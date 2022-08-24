using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int listSize;

    [SerializeField]
    private float timeScale = 1;
    [SerializeField]
    private GameObject destructibleObjsParent;

    private List<Transform> destructibleObjsList = new List<Transform>();

    private void OnEnable()
    {
        DestructibleObj.onDestroyedObj += DestroyDestructibleObj;
    }

    private void OnDisable()
    {
        DestructibleObj.onDestroyedObj -= DestroyDestructibleObj;
    }

    private void Start()
    {
        Time.timeScale = timeScale;

        DestructibleObj[] objs = destructibleObjsParent.GetComponentsInChildren<DestructibleObj>();

        for (int i = 0; i < objs.Length; i++)
        {
            destructibleObjsList.Add(objs[i].transform);
        }

        listSize = destructibleObjsList.Count;
    }

    private void DestroyDestructibleObj(GameObject obj)
    {
        //Debug.Log("Destroying obj " + obj.name);

        destructibleObjsList.Remove(obj.transform);
        Destroy(obj);

        listSize = destructibleObjsList.Count;
    }
}
