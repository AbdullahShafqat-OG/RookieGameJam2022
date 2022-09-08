using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private GameObject gameUI;

    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private TextMeshProUGUI progressTxt;

    [SerializeField]
    CinematicCamera cinematicCamera;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);

        //menuUI.SetActive(true);
        gameUI.SetActive(true);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void Start()
    {
        progressSlider.value = 0;
        string text = (gameManager.initialObjListSize - gameManager.currentObjListSize).ToString();
        text += " / " + gameManager.initialObjListSize;
        progressTxt.text = text;
    }

    private void OnObjDestroyed()
    {
        //Debug.Log("Obj Destroyed Event Triggered in UI");
        //!EventSystem.current.IsPointerOverGameObject()

        float progressValue = (float)gameManager.currentObjListSize / gameManager.initialObjListSize * 100;
        //Debug.Log(progressValue);
        progressSlider.DOValue(100 - progressValue, 0.2f, true).SetEase(Ease.OutSine);

        //progressSlider.value = 100 - progressValue;
        string text = (gameManager.initialObjListSize - gameManager.currentObjListSize).ToString();
        text += " / " + gameManager.initialObjListSize;
        progressTxt.text = text;

        if (progressValue <= 30)
        {
            cinematicCamera.gameObject.SetActive(true);
            Invoke("handleWin", 1.5f);
        }
    }

    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in UI");
        // here handle the losing screen
        cinematicCamera.slowMotionScale = GameManager.instance.timeScale;
        cinematicCamera.gameObject.SetActive(true);
        Invoke("handleLoss", 1.5f);
    }

    void handleLoss()
    {
        cinematicCamera.slowMotionScale = 1f;
        SceneManager.LoadScene("Lose Screen");
    }
    void handleWin()
    {
        Time.timeScale= GameManager.instance.timeScale;
        SceneManager.LoadScene("Win Screen");
    }

    public void StartLevel()
    {
        Messenger.Broadcast(GameEvent.START_LEVEL);
        EnableGameUI();
    }

    private void EnableGameUI()
    {
        Debug.Log("Enabling Game UI");
        menuUI.SetActive(false);
        gameUI.SetActive(true);
    }
}
