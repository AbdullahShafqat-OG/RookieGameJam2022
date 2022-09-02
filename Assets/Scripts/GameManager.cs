using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeScale = 1;
    [SerializeField]
    private float dObjDestroyDelay = 1.0f;
    [SerializeField]
    private GameObject destructibleObjsParent;
    [SerializeField]
    private GameObject destroyParticleEffect;
    [SerializeField]
    private float destroyPEScaleDown = 6.0f;

    private List<Transform> destructibleObjsList = new List<Transform>();

    public int initialObjListSize { get; private set; }
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

    private void Update()
    {
        // CODE FOR TESTING
        if (Input.GetKeyDown(KeyCode.R))
            SceneLoader.instance.ReloadScene();
    }

    private void DestroyDestructibleObj(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();

        GameObject particle = Instantiate(destroyParticleEffect, obj.transform.position, Quaternion.identity);
        particle.transform.localScale *= objRenderer.bounds.size.magnitude / destroyPEScaleDown;
        particle.GetComponent<ParticleSystemRenderer>().material.color = objRenderer.material.color;

        destructibleObjsList.Remove(obj.transform);
        Destroy(obj, dObjDestroyDelay);

        currentObjListSize = destructibleObjsList.Count;

        Messenger.Broadcast(GameEvent.OBJ_DESTROYED);
    }
}
