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
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void OnObjDestroyed()
    {
        //Debug.Log("Obj Destroyed Event Triggered in UI");
        //!EventSystem.current.IsPointerOverGameObject()

        float progressValue = (float)gameManager.currentObjListSize / gameManager.initialObjListSize * 100;
        //Debug.Log(progressValue);
        progressSlider.value = 100 - progressValue;
    }

    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in UI");
        // here handle the losing screen
    }
}
