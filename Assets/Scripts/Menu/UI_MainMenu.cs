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
    public GameObject[] panelModal;

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
