using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public Animator momAnimator, boiAnimator;
    string gameplaySceneName;
    public void PlayGame(string gameplayScene)
    {
        gameplaySceneName = gameplayScene;

        Invoke("PlayBoiDestruction", 0f);
        Invoke("PlayAmmiSurprise", 2f);
        Invoke("LoadGameplay", 3.5f);
    }
    public void PlayGameInstantly(string gameplayScene)
    {
        gameplaySceneName = gameplayScene;
        LoadGameplay();
    }

    void PlayBoiDestruction()
    {
        boiAnimator.SetTrigger("drop object");
    }
    void PlayAmmiSurprise()
    {
        momAnimator.SetTrigger("surprise");
    }

    void LoadGameplay()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}
