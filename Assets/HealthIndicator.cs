using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthIndicator : MonoBehaviour
{
    public DestructibleObj dObj;
    [SerializeField]
    private Image damageFillImage, fillImage;
    public float decreaseStep;

    private int totalHealth;

    private void Start()
    {
        totalHealth = dObj.health;
    }
    private void Update()
    {
        //if ((float)dObj.health / (float)totalHealth != fillImage.fillAmount)
        //{
        //    Debug.Log("YOS");
        //    this.transform.DOShakeScale(0.3f);
        //}

        updateHealthBar();
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }

    void updateHealthBar()
    {
        if (this.transform.GetComponent<FollowScript>())
        {
            if(this.GetComponent<FollowScript>().toFollow == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        fillImage.fillAmount = (float)dObj.health / (float)totalHealth;

        if (dObj.health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (damageFillImage.fillAmount > fillImage.fillAmount)
        {
            damageFillImage.fillAmount -= decreaseStep * Time.deltaTime;
        }
    }
}
