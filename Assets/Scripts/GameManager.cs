using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    [SerializeField] private GameState gameState;


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
    }

    public void OnGameStateChange(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Loading:

                break;
            case GameState.Start:

                break;
            case GameState.Pause:

                break;
            case GameState.UnPause:

                break;
            case GameState.GameOver:

                break;
            case GameState.Victory:

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