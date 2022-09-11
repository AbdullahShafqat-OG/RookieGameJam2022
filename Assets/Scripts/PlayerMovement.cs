using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    internal PlayerManager playerManager;

    public Rigidbody rb;
    public float forwardSpeed;
    public float turnSpeed, maxTurn;

    private float turn;

    private bool alive = true;

    [SerializeField]
    private float scoreMultiIncStep, scoreMultiDecStep, driftAddScore;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void Update()
    {

    }
    void FixedUpdate()
    {
        if (!alive)
            return;
        Vector3 driftValue = transform.InverseTransformVector(rb.velocity); 
        float driftAngle = (Mathf.Atan2(driftValue.x, driftValue.z) * Mathf.Rad2Deg);
        if (rb.velocity.magnitude > 5 && Mathf.Abs(driftAngle) > 15)
        {
            //Debug.Log(driftAngle);
            GameManager.instance.scoreMultiplier += (scoreMultiIncStep * Mathf.Abs((driftAngle / 90)));
            //GameManager.instance.score += (int)(driftAddScore);
        }
        else
        {
            if(GameManager.instance.scoreMultiplier >= 1 + scoreMultiDecStep)
            {
                float oldValue = GameManager.instance.scoreMultiplier;
                if(GameManager.instance.scoreMultiplier - ((int)GameManager.instance.scoreMultiplier) > 0.15f){
                    GameManager.instance.scoreMultiplier -= scoreMultiDecStep;
                }
                else
                {
                    GameManager.instance.scoreMultiplier -= (scoreMultiDecStep * .4f);
                }
                if (GameManager.instance.scoreMultiplier < (int)oldValue)
                {
                    GameManager.instance.scoreMultiplier = 1;
                }
            }
        }

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
