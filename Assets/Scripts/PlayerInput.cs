using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerManager playerManager;
    public FixedJoystick joystick;

    internal float dir;
    public float lerpStep;

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
        
        if(value == 0.5f)
        {
            float animBlend = playerManager.animator.GetFloat("Blend");
            if (animBlend > 0.52f)
            {
                playerManager.animator.SetFloat("Blend", animBlend - lerpStep * Time.deltaTime);
            }
            else if(animBlend < 0.48f)
            {
                playerManager.animator.SetFloat("Blend", animBlend + lerpStep * Time.deltaTime);
            }
            return;
        }

        playerManager.animator.SetFloat("Blend", value);
    }
}
