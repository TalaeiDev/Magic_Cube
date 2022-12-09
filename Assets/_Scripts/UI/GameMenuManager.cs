using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public GameObject menuButtons;
    public GameObject timerText;
   public void Retry()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void MenuButton()
    {
        if (GameManager.Instance.Scrambling) return;

        menuButtons.SetActive(!menuButtons.activeSelf);
        GameManager.Instance.stop = !GameManager.Instance.stop;
    }

    public void TimerText()
    {
        timerText.SetActive(!timerText.activeSelf);
    }
}
