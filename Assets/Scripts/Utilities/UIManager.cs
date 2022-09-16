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

    private void Awake()
    {
        Messenger.AddListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.AddListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);

        //menuUI.SetActive(true);
        gameUI.SetActive(true);
    }

    private void Update()
    {
        float progressValue = (float)GameManager.instance.score / (float)GameManager.instance.targetScore * 100;
        progressValue += 0.2f;
        progressSlider.DOValue(progressValue, 0.2f, true).SetEase(Ease.OutSine);

        progressTxt.text = GameManager.instance.score.ToString();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.OBJ_DESTROYED, OnObjDestroyed);
        Messenger.RemoveListener(GameEvent.AMMI_CAUGHT_UP, OnAmmiCaughtUp);
    }

    private void Start()
    {
        progressSlider.value = 0;
        //string text = (gameManager.initialObjListSize - gameManager.currentObjListSize).ToString();
        //text += " / " + gameManager.initialObjListSize;
        //progressTxt.text = text;
        progressTxt.text = 0.ToString();
    }

    private void OnObjDestroyed()
    {
        ////Debug.Log("Obj Destroyed Event Triggered in UI");
        ////!EventSystem.current.IsPointerOverGameObject()

        //float progressValue = (float)gameManager.currentObjListSize / gameManager.initialObjListSize * 100;
        ////Debug.Log(progressValue);

        ////progressSlider.value = 100 - progressValue;
        //string text = (gameManager.initialObjListSize - gameManager.currentObjListSize).ToString();
        //text += " / " + gameManager.initialObjListSize;
        //progressTxt.text = text;


        //float progressValue = (float)GameManager.instance.score / (float)GameManager.instance.targetScore * 100;
        //progressSlider.DOValue(progressValue, 0.2f, true).SetEase(Ease.OutSine);
        progressSlider.transform.DOShakePosition(1f, 10);
        //progressTxt.text = GameManager.instance.score.ToString();
    }

    private void OnAmmiCaughtUp()
    {
        Debug.Log("Ammi Caught Up Event Triggered in UI");
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
