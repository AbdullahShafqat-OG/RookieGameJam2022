using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField]
    private int health = 2;

    public delegate void DestroyObjAction(GameObject obj);
    public static event DestroyObjAction onDestroyedObj;

    public int DamageObj(int value)
    {
        health -= value;
        if (health <= 0)
        {
            onDestroyedObj(gameObject);
        }

        HandleChildren();

        return health;
    }

    private void HandleChildren()
    {
        if (transform.childCount == 0)
            return;

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child == this.transform)
                continue;

            if (child.CompareTag("Destructible"))
            {
                child.parent = null;
                child.GetComponent<Collider>().enabled = true;

                DestructibleObj destructibleObj = child.gameObject.AddComponent<DestructibleObj>();
                child.gameObject.AddComponent<Rigidbody>();
                destructibleObj.health = 1;
            }
        }
    }
}
