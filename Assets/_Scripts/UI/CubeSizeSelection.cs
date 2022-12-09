using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CubeSizeSelection : MonoBehaviour
{
    public Slider slider;
    public Text sliderText;
    public Text buttonText;

    // Update is called once per frame
    void Update()
    {
        sliderText.text = slider.value.ToString();
        buttonText.text = (slider.value.ToString() + " X " + slider.value.ToString() + " X " + slider.value.ToString());
    }

    public void LoadGame()
    {
        GameManager.Instance.playerSettings.rubicSize = (int)slider.value;
        GameManager.Instance.playerSettings.timer = GameManager.Instance.playerSettings.rubicSize * 40;
        SceneManager.LoadSceneAsync(1);
    }
}
