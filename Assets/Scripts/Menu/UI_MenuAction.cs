using PeaTFS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class UI_MenuAction : MonoBehaviour
{
    [Header("Input System")]
    [SerializeField] private InputActionReference inputPause;


    public bool isPauseMenu;

    private void OnEnable()
    {
        inputPause.action.performed += OnPauseMenu;
    }

    private void OnDisable()
    {
        inputPause.action.performed -= OnPauseMenu;
    }


    private void Update()
    {
        PauseMenu();
    }

    private void PauseMenu()
    {
        if (isPauseMenu)
        {
            bool isPause = GameManager.Instance.IsGamePaused();
            if (isPause)
            {
                GameManager.Instance.OnGameStateChange(GameState.Resume);
                Debug.Log("Resume Game");
            }
            else
            {
                GameManager.Instance.OnGameStateChange(GameState.Pause);
                Debug.Log("Pause Game");
            }
            isPauseMenu = false;
        }
    }
    private void OnPauseMenu(InputAction.CallbackContext obj)
    {
        isPauseMenu = isPauseMenu ? isPauseMenu : !isPauseMenu;
        Debug.Log("Disable enable " + isPauseMenu);
    }

}
