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
    }
}
