using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followScript : MonoBehaviour
{
    [SerializeField]
    private Transform toFollow;

    private void Update()
    {
        this.transform.position = toFollow.position;
    }
}
