using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace PeaTFS
{
	public class PeaAction : MonoBehaviour
	{
        #if ENABLE_INPUT_SYSTEM
                private PlayerInput _playerInput;
#endif

		private PeaMovementInput _input;
        

        [Header("Radial Seeding Growth")]
        [SerializeField] private SeedingGrowth seedingGrowth;

        public LayerMask layerPlantMedia;
        public ActionType actionType = ActionType.None;

        private bool isRunning = false;
        public bool IsRunning {set => isRunning = value; }

        private bool IsCurrentDeviceMouse
        {
            get
            {
                #if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
                #else
				return false;
                #endif
            }
        }


        private void Start()
        {
            actionType = ActionType.None;
            _input = GetComponent<PeaMovementInput>();
#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else 
            Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif
            if(seedingGrowth== null )
            {
                seedingGrowth = FindObjectOfType<SeedingGrowth>();
            }

            
        }

        private void Update()
        {
            if(isRunning)
                ExecuteAction();
            PauseMenu();
        }
        /*
        private void FixedUpdate()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, layerPlantMedia)){
                Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                Debug.Log("Hit it");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1f, Color.white);
            }
        }
        /*/
        private void ExecuteAction()
        {
            if (actionType != ActionType.None)
            {
                if (_input.interaction)
                {
                    switch (actionType)
                    {
                        case ActionType.ActivateMagicSeed:
                            StartCoroutine(ActionSpell());
                            break;

                        case ActionType.MagicPlant:
                            StartCoroutine(ActionPlant());
                            break;
                        
                    }
                }
            }
            else
            {
                _input.interaction = false;
            }

        }

        private void PauseMenu()
        {
            if (_input.pauseMenu)
            {
                bool isPause = GameManager.Instance.IsGamePaused();
                if (isPause)
                {
                    GameManager.Instance.OnGameStateChange(GameState.Resume);
                }
                else    
                {
                    GameManager.Instance.OnGameStateChange(GameState.Pause);
                }
                _input.pauseMenu = false;
            }
        }

        #region MagicSeed

        private IEnumerator ActionSpell()
        {
            yield return null;
            FindObjectOfType<Arable>().ActivateSeeding();
            seedingGrowth.StartSeeding();
            ResetActionType();
            _input.interaction = false;
        }

        #endregion
        #region PLANT MEDIA

        private IEnumerator ActionPlant()
        {
            yield return null;
            ResetActionType();
            _input.interaction = false;

            plantMedia.ActivateSeeding();
        }

        private PlantMedia plantMedia;
        public void GetPlantMedia(PlantMedia _plantMedia)
        {
            plantMedia= _plantMedia;
        }

        #endregion

        private void ResetActionType()
        {
            actionType = ActionType.None;
        }

    }
}

[System.Serializable]
public enum ActionType
{
    None,
    ActivateMagicSeed,
    MagicPlant
    
}