using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricycleVisual : MonoBehaviour
{
    public Rigidbody rb;

    private void Update()
    {
        this.transform.LookAt(this.transform.position + rb.velocity);
    }
}
