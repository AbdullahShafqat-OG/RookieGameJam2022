using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        transform.position = newPos;

        if (Input.GetKeyDown(KeyCode.Space))
            AudioManager.instance.PlaySound("YouThere");
    }
}
