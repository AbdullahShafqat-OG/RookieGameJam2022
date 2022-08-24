using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObj : MonoBehaviour
{
    [SerializeField]
    private int health;

    public delegate void DestroyObjAction(GameObject obj);
    public static event DestroyObjAction onDestroyedObj;

    public int DamageObj(int value)
    {
        health -= value;
        if (health <= 0)
        {
            HandleChildren();
            onDestroyedObj(gameObject);
        }

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

            child.parent = null;
            child.GetComponent<Collider>().enabled = true;
            if (child.CompareTag("Destructible"))
            {
                //Debug.Log("Destructible");

                DestructibleObj destructibleObj = child.gameObject.AddComponent<DestructibleObj>();
                destructibleObj.health = 1;
            }
            else if (child.CompareTag("DestroyChild"))
            {
                //Debug.Log("DestroyChild");

                Destroy(child.gameObject);
            }
            else
            {
                //Debug.Log("Retain");

                child.gameObject.AddComponent<Rigidbody>();
            }
        }
    }
}
