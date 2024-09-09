using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //[Header("Game State")]
    private GameState gameState;

    /* Player Input */
    private PlayerInput playerInput;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerInput = FindAnyObjectByType<PlayerInput>();
    }

    private void Start()
    {
        playerInput.enabled = false;
        OnGameStateChange(GameState.Start);
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Loading:

                break;
            case GameState.Start:
                StartGame();

                break;
            case GameState.Pause:
                PauseGame();

                break;
            case GameState.UnPause:
                UnPauseGame();

                break;
            case GameState.GameOver:
                GameOver(false);

                break;
            case GameState.Victory:
                GameOver(true);

                break;

        }
    }

    private void GameOver(bool _isWin)
    {
        playerInput.enabled = false;
    }

    private void UnPauseGame()
    {
        playerInput.enabled = true;
    }

    private void PauseGame()
    {
        playerInput.enabled = false;
    }

    private void StartGame()
    {
        playerInput.enabled = true;
    }
}

[System.Serializable]
public enum GameState
{
    Loading,
    Start,
    Pause,
    UnPause,
    GameOver,
    Victory
}