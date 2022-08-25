using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeScale = 1;
    [SerializeField]
    private GameObject destructibleObjsParent;

    private List<Transform> destructibleObjsList = new List<Transform>();

    // serialized for debugging only
    [field: SerializeField]
    public int initialObjListSize { get; private set; }
    [field: SerializeField]
    public int currentObjListSize { get; private set; }

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

        initialObjListSize = destructibleObjsList.Count;
        currentObjListSize = initialObjListSize;
    }

    private void DestroyDestructibleObj(GameObject obj)
    {
        destructibleObjsList.Remove(obj.transform);
        Destroy(obj);

        currentObjListSize = destructibleObjsList.Count;

        Messenger.Broadcast(GameEvent.OBJ_DESTROYED);
    }
}
