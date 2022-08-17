using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSphere : MonoBehaviour
{
	[SerializeField, Range(0f, 100f)]
	float maxSpeed = 10f;

	[SerializeField, Range(0f, 100f)]
	float maxAcceleration = 10f, maxAirAcceleration = 1f;

	[SerializeField, Range(1f, 5f)]
	float airGravityModifier = 2f;

	[SerializeField, Range(0f, 10f)]
	float jumpHeight = 2f;

	[SerializeField, Range(0f, 90f)]
	float maxGroundAngle = 25f;
	
	Rigidbody body;
	Vector3 velocity, desiredVelocity;
	bool desiredJump;

	[SerializeField]
	bool onGround;

	float minGroundDotProduct;

	Vector3 defaultGravity;

    private void OnValidate()
    {
		minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
    }

    private void Awake()
	{
		body = GetComponent<Rigidbody>();
		OnValidate();

		defaultGravity = Physics.gravity;
	}

	private void Update()
	{
		Vector2 playerInput;
		playerInput.x = Input.GetAxis("Horizontal");
		playerInput.y = Input.GetAxis("Vertical");
		playerInput = Vector2.ClampMagnitude(playerInput, 1f);

		desiredVelocity =
			new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;

		desiredJump |= Input.GetButtonDown("Jump");
	}

    private void FixedUpdate()
    {
		if (onGround)
			Physics.gravity = defaultGravity;
		else
			Physics.gravity = 
				new Vector3(defaultGravity.x, defaultGravity.y * airGravityModifier, defaultGravity.z);

		velocity = body.velocity;
		float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
		float maxSpeedChange = acceleration * Time.deltaTime;
		velocity.x =
			Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
		velocity.z =
			Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
		
		if (desiredJump)
		{
			desiredJump = false;
			Jump();
		}
		body.velocity = velocity;

		onGround = false;
	}

	void Jump()
    {
		if (onGround)
        {
			velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void OnCollisionStay(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void EvaluateCollision(Collision collision)
	{
		for (int i = 0; i < collision.contactCount; i++)
		{
			Vector3 normal = collision.GetContact(i).normal;
			onGround |= normal.y >= minGroundDotProduct;
		}
	}
}
