using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class comboUIManager : MonoBehaviour
{
    [SerializeField]
    private Image comboFill;
    [SerializeField]
    private TextMeshProUGUI comboText;

    private float oldMultiValue;
    private void Update()
    {
        if ((int)GameManager.instance.scoreMultiplier > ((int)oldMultiValue))
        {
            //Debug.Log((int)GameManager.instance.scoreMultiplier + ", " + (((int)oldMultiValue) - 1));
            comboFill.transform.parent.transform.DOShakePosition(1f, 10);
        }

        comboText.text = "x" + ((int)GameManager.instance.scoreMultiplier).ToString();
        comboFill.DOFillAmount(GameManager.instance.scoreMultiplier - ((int)GameManager.instance.scoreMultiplier), 0.5f);

        oldMultiValue = GameManager.instance.scoreMultiplier;
    }
}
