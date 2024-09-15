using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    [Header("Panel")]


    [Header("Game Over")]
    public GameObject canvasGameOver;
    public TextMeshProUGUI textScore;
    public Image lineOverImage;
    public Sprite lineVictory;
    public Sprite lineLosse;

    public void GameOver(bool _isWin, int timeRemaining)
    {
        canvasGameOver.SetActive(true);

        if(_isWin)
        {
            lineOverImage.sprite = lineVictory;

            int min = timeRemaining / 60;
            int sec = timeRemaining % 60;
            if (sec >= 10)
            {
                textScore.text = "0" + min + ":" + sec;
            }
            else
            {
                textScore.text = "0" + min + ":0" + sec;
            }
        }
        else
        {
            lineOverImage.sprite = lineLosse;
        }
    }
}
