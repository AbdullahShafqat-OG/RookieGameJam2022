using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform toFollow;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - toFollow.position;
    }
    void Update()
    {
        this.transform.position = toFollow.position + offset;
    }
}
