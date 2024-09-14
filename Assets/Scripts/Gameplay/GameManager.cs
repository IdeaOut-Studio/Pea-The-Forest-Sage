using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PeaTFS
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        //[Header("Game State")]
        private GameState gameState;

        /* Player Input */
        private PlayerInput playerInput;

        [Header("Timer Setup")]
        [SerializeField]private int timer = 300;
        [SerializeField]private TextMeshProUGUI textTimer;

        private void Awake()
        {
            if (Instance == null)
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

        private void SetTextTimer()
        {
            int min = timer / 60;
            int sec = timer % 60;
            if(sec >= 10)
            {
                textTimer.text = "0" + min + ":" + sec;
            }
            else
            {
                textTimer.text = "0" + min + ":0" + sec;
            }

            
        }

        IEnumerator Countdown()
        {
            while(timer > 0)
            {
                yield return new WaitForSeconds(1);
                timer -= 1;
                SetTextTimer();
            }
        }

        public void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
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
            StopCoroutine(Countdown());
            playerInput.enabled = false;

            if (_isWin)
            {
                Debug.Log("Victory");
            }
            else
            {
                Debug.Log("Lose");
            }
        }

        private void UnPauseGame()
        {
            playerInput.enabled = true;
            StartCoroutine(Countdown());
        }

        private void PauseGame()
        {
            playerInput.enabled = false;
            StopCoroutine(Countdown());
        }

        private void StartGame()
        {
            playerInput.enabled = true;
            StartCoroutine(Countdown());
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