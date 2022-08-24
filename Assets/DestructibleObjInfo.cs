using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjInfo : MonoBehaviour
{
    public int health;

    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            DestroyThisObject();
        }
    }

    void DestroyThisObject()
    {
        Destroy(this.gameObject);
    }
}
