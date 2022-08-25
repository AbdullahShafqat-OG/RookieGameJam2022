using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjSpawner : MonoBehaviour
{
    public Transform destructibleObjsParent;
    public GameObject[] destructibleObj;
    public int numObjs = 50;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnObjs();
    }

    void SpawnObjs()
    {
        Collider col = GetComponent<BoxCollider>();
        for (int i = 0; i < numObjs; i++)
        {
            GameObject obj = Instantiate(
                destructibleObj[Random.Range(0, destructibleObj.Length)], 
                RandomPointInBounds(col.bounds), Quaternion.identity
                );

            obj.transform.SetParent(destructibleObjsParent);
        }
    }

    Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
            );
    }
}