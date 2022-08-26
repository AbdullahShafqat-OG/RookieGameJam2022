using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Slider progressSlider;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
    }

    private void OnObjDestroyed()
    {
        //Debug.Log("UI Obj Destroyed Event Triggered");
        //!EventSystem.current.IsPointerOverGameObject()

        float progressValue = (float)gameManager.currentObjListSize / gameManager.initialObjListSize * 100;
        //Debug.Log(progressValue);
        progressSlider.value = 100 - progressValue;
    }
}
