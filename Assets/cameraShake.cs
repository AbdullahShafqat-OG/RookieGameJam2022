using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraShake : MonoBehaviour
{
    [SerializeField]
    private float strength, duration;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
    }

    void OnObjDestroyed()
    {
        this.transform.DOShakePosition(duration, strength);
    }
}
