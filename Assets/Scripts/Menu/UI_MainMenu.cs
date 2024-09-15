using PeaTFS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip clipClick;
    public GameObject mainMenu;
    public GameObject uiSelectLevel;
    public GameObject[] panelModal;

    private bool ondemandComplete;
    private LoadingGame loadGame;
    private void Start()
    {
        loadGame = GetComponent<LoadingGame>();

        int od = PlayerPrefs.GetInt("OndemandComplete", 0);
        //int od = 0;
        if (od == 0)
        {
            ondemandComplete = false;
        }
        else if (od == 1)
        {
            ondemandComplete = true;
        }

    }

    public void PLayGameMenu()
    {
        if(ondemandComplete)
        {
            OpenPanelModal(uiSelectLevel);
        }
        else
        {
            loadGame.LoadLevelBtn("L_Ondemand");

        }
    }

    public void ResumeGame()
    {
        GameManager.Instance.OnGameStateChange(GameState.Resume);
    }

    public void OpenMenu(bool isOpen)
    {
        mainMenu.SetActive(isOpen);
    }

    public void OpenPanelModal(GameObject _panel)
    {
        OnClickSfx();
        foreach (GameObject p in panelModal)
        {
            if(p != _panel)
            {
                p.SetActive(false);
            }
            else
            {
                p.SetActive(true);
            }
        }
    }

    public void ClosePanelModal()
    {
        OnClickSfx();
        foreach(GameObject p in panelModal)
        {
            p.SetActive(false);
        }
    }

    private void OnClickSfx()
    {
        audio.PlayOneShot(clipClick);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
