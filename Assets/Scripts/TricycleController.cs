using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricycleController : MonoBehaviour
{
    public float handleRotation;
    public float paddleRotationSpeed;

    [SerializeField]
    private Transform handleParent;
    [SerializeField]
    private Transform paddlesParent;

    [SerializeField, Range(-90f, 90f)]
    private float maxHandleRotation;

    float paddlesParentYRot, paddlesParentZRot;

    // Start is called before the first frame update
    void Start()
    {
        handleRotation = handleParent.rotation.y;

        paddlesParentYRot = paddlesParent.rotation.eulerAngles.y;
        paddlesParentZRot = paddlesParent.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(handleRotation) > Mathf.Abs(maxHandleRotation))
            handleRotation = Mathf.Sign(handleRotation) * maxHandleRotation;

        handleParent.rotation = 
            Quaternion.Euler(handleParent.rotation.eulerAngles.x, handleRotation, handleParent.rotation.eulerAngles.z);

        paddlesParent.Rotate(new Vector3(paddleRotationSpeed, paddlesParentYRot, paddlesParentZRot) * Time.deltaTime);
    }
}
