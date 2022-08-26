using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    internal PlayerManager playerManager;

    public Rigidbody rb;
    public float forwardSpeed;
    public float turnSpeed;
    private float dir;


    private void Start()
    {

    }
    private void Update()
    {
        dir = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        moveForward();
        rotatePlayer(dir);
    }
    void moveForward()
    {
        if (rb.velocity.magnitude < forwardSpeed)
        {
            rb.AddForce(transform.forward * forwardSpeed);
        }
        //rb.velocity = transform.forward * forwardSpeed;
    }
    void rotatePlayer(float dir)
    {
        transform.Rotate(Vector3.up.normalized * dir * turnSpeed);
    }
}
