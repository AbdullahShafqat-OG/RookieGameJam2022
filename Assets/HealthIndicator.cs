using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthIndicator : MonoBehaviour
{
    public DestructibleObj dObj;
    [SerializeField]
    private Image fillImage;
    private int totalHealth;

    private void Start()
    {
        totalHealth = dObj.health;
    }
    private void Update()
    {
        if ((float)dObj.health / (float)totalHealth != fillImage.fillAmount)
        {
            Debug.Log("YOS");
            this.transform.DOShakeScale(0.3f);
        }

        fillImage.fillAmount = (float)dObj.health / (float)totalHealth;

        if (dObj.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void LateUpdate()
    {
        
    }
}
