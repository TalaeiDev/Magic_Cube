using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.stop) return;

        if(GameManager.Instance.gameOver== false && GameManager.Instance.Scrambling == false)
           TimerCountDown();
    }

    public void TimerCountDown()
    {
        float minutes = Mathf.FloorToInt(GameManager.Instance.timer / 60);
        float Seconds = Mathf.FloorToInt(GameManager.Instance.timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, Seconds);

        if (GameManager.Instance.timer > 0)
            GameManager.Instance.timer -= Time.deltaTime;

        if (GameManager.Instance.timer < 0)
        {
            GameManager.Instance.timer = 0;
            Instantiate(GameManager.Instance.playerSettings.gameOverMenu);
            Destroy(GameManager.Instance.currentMenu);
            GameManager.Instance.gameOver = true;
        }
            
    }
}
