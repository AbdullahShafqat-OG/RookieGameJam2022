using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector2 bounds = new Vector2(-5, 5);
    [Range(15.0f, 85.0f)]
    [SerializeField]
    private float maxRotation = 25.0f;

    [Header("Player Movement Controls")]
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float slideSpeed = 500.0f;

    [SerializeField]
    private float rotateSpeed = 10.0f;

    [Range(1, 10)]
    [SerializeField]
    private int rotateBackSpeed = 5;

    private Transform modelTransform;
    private Touch touch;

    // for rotating smoothly
    private float rotateFactor = 0.0f;

    // 1 is right -1 is left 0 is straight
    private int direction = 0;
    private int lastDirection = 0;

    private float xMovement = 5.0f;

    void Start()
    {
        modelTransform = GetComponentInChildren<ModelScript>().transform;
    }

    void Update()
    {
        HandleTouchInput();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void HandleTouchInput()
    {
        //Debug.Log("Mobile Input");

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                direction = (int)Mathf.Sign(touch.deltaPosition.x);
                xMovement = touch.deltaPosition.magnitude / Time.deltaTime / slideSpeed;
            }
            else
                direction = 0;
        }
    }

    void MoveCharacter()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (direction == 0)
        {
            rotateFactor = 0.0f;
            Quaternion newRot = Quaternion.Euler(new Vector3(0, 0, 0));

            modelTransform.rotation = Quaternion.Lerp(modelTransform.rotation, newRot, rotateBackSpeed * Time.deltaTime);
            return;
        }

        if (lastDirection != direction)
        {
            rotateFactor = 0.0f;
            modelTransform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        }

        rotateFactor += rotateSpeed;
        rotateFactor = Mathf.Clamp(rotateFactor, 0.0f, maxRotation);

        modelTransform.eulerAngles = new Vector3(0.0f, rotateFactor * direction, 0.0f);

        transform.Translate(Vector3.right * direction * xMovement * Time.deltaTime);

        if (transform.position.x > bounds.y)
            transform.position = new Vector3(bounds.y, transform.position.y, transform.position.z);

        if (transform.position.x < bounds.x)
            transform.position = new Vector3(bounds.x, transform.position.y, transform.position.z);

        lastDirection = direction;
    }
}
