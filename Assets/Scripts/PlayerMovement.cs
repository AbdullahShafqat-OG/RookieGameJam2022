using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    internal PlayerManager playerManager;

    public Rigidbody rb;
    public float forwardSpeed;
    public float turnSpeed, maxTurn;

    private float turn;


    private void Start()
    {

    }
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        moveForward();
        rotatePlayer(playerManager.playerInput.dir);
    }
    void moveForward()
    {
        if (rb.velocity.magnitude < forwardSpeed)
        {
            rb.AddForce(transform.forward * forwardSpeed);
        }
    }
    void rotatePlayer(float dir)
    {
        if (turn <= maxTurn)
        {
            turn += turnSpeed;
        }
        transform.Rotate(Vector3.up.normalized * dir * turn);

        if (dir == 0)
        {
            turn = 0;
        }
    }
}
