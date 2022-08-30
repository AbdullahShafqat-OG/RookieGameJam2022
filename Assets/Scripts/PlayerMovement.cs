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

    private bool alive = true;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    void FixedUpdate()
    {
        if (!alive)
            return;

        MoveForward();
        RotatePlayer(playerManager.playerInput.dir);
    }

    void MoveForward()
    {
        if (rb.velocity.magnitude < forwardSpeed)
        {
            rb.AddForce(transform.forward * forwardSpeed);
        }
    }

    void RotatePlayer(float dir)
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

    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in Player Movement");
        alive = false;
    }
}
