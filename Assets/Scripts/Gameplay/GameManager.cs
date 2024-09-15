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
        private bool isGamePaused;
        /* Player Input */
        //private PlayerInput playerInput;
        private PeaMovement peaMovement;
        private PeaAction peaAction;

        [Header("Menu Setting")]
        [SerializeField] private UI_MainMenu panelPauseMenu;
        [SerializeField] private UI_GameOver panelGameOver;

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

            //playerInput = FindAnyObjectByType<PlayerInput>();
        }

        private void Start()
        {
            if (peaMovement == null)
            {
                peaMovement = FindObjectOfType<PeaMovement>();
            }
            if (peaAction == null)
                peaAction = FindObjectOfType<PeaAction>();

            isGamePaused = true;
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
                if (!isGamePaused)
                {
                    timer -= 1;
                    SetTextTimer();

                }
            }

            OnGameStateChange(GameState.GameOver);
        }

        public bool IsGamePaused()
        {
            return isGamePaused;
        }

        public void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Loading:
                    isGamePaused = true;
                    break;
                case GameState.Start:
                    isGamePaused = false;
                    StartGame();

                    break;
                case GameState.Pause:
                    isGamePaused = true;
                    PauseGame();

                    break;
                case GameState.Resume:
                    isGamePaused = false;
                    Resume();

                    break;
                case GameState.GameOver:
                    isGamePaused = true;
                    GameOver(false);

                    break;
                case GameState.Victory:
                    GameOver(true);
                    isGamePaused = true;

                    break;

            }
        }

        private void GameOver(bool _isWin)
        {
            peaMovement.IsGameRunning = false;
            peaAction.IsRunning= false;
            CursorVisible(true);
            panelGameOver.GameOver(_isWin, timer);

            if (_isWin)
            {
                Debug.Log("Victory");
            }
            else
            {
                Debug.Log("Lose");
            }
        }

        private void Resume()
        {
            peaMovement.IsGameRunning = true;
            peaAction.IsRunning = true;
            CursorVisible(false);
            panelPauseMenu.OpenMenu(false);
            StartCoroutine(Countdown());
        }


        private void PauseGame()
        {
            peaMovement.IsGameRunning = false;
            peaAction.IsRunning = false;
            CursorVisible(true);
            panelPauseMenu.OpenMenu(true);
            StopCoroutine(Countdown());
        }

        private void StartGame()
        {
            peaMovement.IsGameRunning = true;
            peaAction.IsRunning = true;
            CursorVisible(false);
            panelPauseMenu.OpenMenu(false);
            StartCoroutine(Countdown());
        }
        private void CursorVisible(bool _isVisible)
        {
            if(_isVisible)
            {
                //playerInput.enabled = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible= true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
        }
    }
}
    

[System.Serializable]
public enum GameState
{
    Loading,
    Start,
    Pause,
    Resume,
    GameOver,
    Victory
}