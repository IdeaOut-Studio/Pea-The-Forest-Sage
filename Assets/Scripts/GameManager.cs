using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    [SerializeField] private GameState gameState;

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
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Loading:

                break;
            case GameState.Start:
                playerInput.enabled = true;

                break;
            case GameState.Pause:
                playerInput.enabled = false;

                break;
            case GameState.UnPause:
                playerInput.enabled = true;

                break;
            case GameState.GameOver:
                playerInput.enabled = false;

                break;
            case GameState.Victory:
                playerInput.enabled = false;

                break;

        }
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