using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    [SerializeField]
    internal Transform toFollow;

    private void Update()
    {
        if(toFollow != null)
        {
            this.transform.position = toFollow.position;
        }
    }
}
