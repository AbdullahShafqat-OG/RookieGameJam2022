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

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Level " + (PlayerPrefs.GetInt("currLevel") + 1).ToString());
        //SceneLoader.instance.ReloadCurrentLevel();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level " + (PlayerPrefs.GetInt("currLevel") + 1).ToString());
        //SceneLoader.instance.LoadNextLevel();
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
        SceneManager.LoadScene("Level " + (PlayerPrefs.GetInt("currLevel") + 1).ToString());
        //SceneManager.LoadScene(gameplaySceneName);
        //SceneLoader.instance.LoadNextLevel();
    }
}
