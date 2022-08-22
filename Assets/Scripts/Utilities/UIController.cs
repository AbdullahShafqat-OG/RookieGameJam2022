using UnityEngine;

public class UIController : MonoBehaviour
{
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GENERIC_EVENT, OnGenericEvent);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GENERIC_EVENT, OnGenericEvent);
    }

    private void OnGenericEvent()
    {
        Debug.Log("UI Generic Event Triggered");
        //!EventSystem.current.IsPointerOverGameObject()
    }
}
