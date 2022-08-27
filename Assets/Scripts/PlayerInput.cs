using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerManager playerManager;
    public FixedJoystick joystick;

    internal float dir;

    void Start()
    {
        //vroom vrrom madafaka
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxis("Horizontal");
        dir = joystick.Direction.x;

        setAnimBlendValue(dir);
    }

    void setAnimBlendValue(float xDir)
    {
        float value = 0.5f + (xDir / 2);
        playerManager.animator.SetFloat("Blend", value);
    }
}
