using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private TextMeshProUGUI progressTxt;

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
        string text = (gameManager.initialObjListSize - gameManager.currentObjListSize).ToString();
        text += " / " + gameManager.initialObjListSize;
        progressTxt.text = text;
    }

    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in UI");
        // here handle the losing screen
    }
}
