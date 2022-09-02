using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Rigidbody rb;
    private void Update()
    {
        transform.LookAt(transform.position + rb.velocity);
    }
}
