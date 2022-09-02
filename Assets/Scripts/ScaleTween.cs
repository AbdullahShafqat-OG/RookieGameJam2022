using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleTween : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetScale = new Vector3(2.0f, 2.0f, 1.0f);
    [SerializeField]
    private float cycleLength = 1.0f;
    [SerializeField]
    private Ease ease = Ease.InOutSine;

    private void Start()
    {
        transform.DOScale(targetScale, cycleLength).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
    }
}
