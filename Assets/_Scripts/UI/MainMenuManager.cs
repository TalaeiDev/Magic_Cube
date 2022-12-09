using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    
    public void StartGame()
    {
        Instantiate(GameManager.Instance.playerSettings.rubicSizeSelectionMenu, Vector3.zero, Quaternion.identity);
        Destroy(gameObject);
    }

    public void Continue()
    {

    }

    public void Quite()
    {
        Application.Quit();
       
    }
}
