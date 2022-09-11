using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    internal float timeScale = 1;
    [SerializeField]
    private float dObjDestroyDelay = 1.0f;
    [SerializeField]
    private GameObject destructibleObjsParent;
    [SerializeField]
    private GameObject destroyParticleEffect;
    [SerializeField]
    private float destroyPEScaleDown = 6.0f;
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    internal int hitScore, negScore, score, targetScore, targetScoreFactor;

    internal float scoreMultiplier;

    [SerializeField]
    CinematicCamera cinematicCamera;

    private List<Transform> destructibleObjsList = new List<Transform>();

    public int initialObjListSize { get; private set; }
    public int currentObjListSize { get; private set; }

    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
        Messenger.RemoveListener(GameEvent.HitDestructibleObject, OnHitDestructibleObject);
    }

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
        scoreMultiplier = 1f;

        Messenger.AddListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
        Messenger.AddListener(GameEvent.HitDestructibleObject, OnHitDestructibleObject);

        Time.timeScale = timeScale;

        DestructibleObj[] objs = destructibleObjsParent.GetComponentsInChildren<DestructibleObj>();

        for (int i = 0; i < objs.Length; i++)
        {
            destructibleObjsList.Add(objs[i].transform);
            Debug.Log(objs[i].transform.name);
        }

        initialObjListSize = destructibleObjsList.Count;
        currentObjListSize = initialObjListSize;

        targetScore = initialObjListSize * hitScore * targetScoreFactor;

        Invoke("InstantiateHealthBars", 0.5f);
        //InstantiateHealthBars();
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

    private void InstantiateHealthBars()
    {
        foreach (var obj in destructibleObjsList)
        {
            for (int i = 0; i < obj.childCount; i++)
            {
                Transform child = obj.GetChild(i);

                if(child.tag == "Destructible")
                {
                    Instantiate(healthBar, child.transform.position, healthBar.transform.rotation);
                    healthBar.GetComponent<FollowScript>().toFollow = child.transform;
                    healthBar.GetComponent<HealthIndicator>().dObj = child.GetComponent<DestructibleObj>();
                }
            }

            Instantiate(healthBar, obj.position, healthBar.transform.rotation);
            healthBar.GetComponent<FollowScript>().toFollow = obj.transform;
            healthBar.GetComponent<HealthIndicator>().dObj = obj.GetComponent<DestructibleObj>();

            Debug.Log(healthBar.GetComponent<FollowScript>().toFollow.transform.name);
        }
    }

    private void OnObjDestroyed()
    {
        if(score >= targetScore && targetScore > 0)
        {
            cinematicCamera.gameObject.SetActive(true);
            Invoke("handleWin", 1.5f);
        }
        //float progressValue = (float)currentObjListSize / initialObjListSize * 100;

        //if (progressValue <= 30)
        //{
        //    cinematicCamera.gameObject.SetActive(true);
        //    Invoke("handleWin", 1.5f);
        //}
    }

    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in UI");
        // here handle the losing screen
        cinematicCamera.slowMotionScale = GameManager.instance.timeScale;
        cinematicCamera.gameObject.SetActive(true);
        Invoke("handleLoss", 1.5f);
    }

    void OnHitDestructibleObject()
    {

    }

    void handleLoss()
    {
        cinematicCamera.slowMotionScale = 1f;
        SceneManager.LoadScene("Lose Screen");
    }
    void handleWin()
    {
        Time.timeScale = GameManager.instance.timeScale;
        SceneManager.LoadScene("Win Screen");
    }
}
