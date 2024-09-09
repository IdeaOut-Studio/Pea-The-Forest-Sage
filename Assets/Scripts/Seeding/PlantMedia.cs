using System.Collections;
using UnityEngine;

namespace PeaTFS
{
    public class PlantMedia : MonoBehaviour
    {
        [SerializeField] private NotificationObject notify;
        [SerializeField] private MagicPlant prefabMagicPlant;
        [SerializeField] private SO_Plant magicPlantObject;
        [SerializeField] private Obstacles obstacleObject;

        [Header("Action Type")]
        [SerializeField] private ActionType actionType = ActionType.MagicPlant;

        private MagicPlant magicPlant;

        private bool isOnce = true;

        public void ActivateSeeding()
        {
            if (isOnce)
            {
                /* Do Some Animation of Activating, and Cinemachine Here*/
                this.GetComponent<SphereCollider>().enabled = false;

                magicPlant = Instantiate(prefabMagicPlant, this.transform);
                Debug.Log("Activating Seeding / Spawn Magic Plant "+magicPlant.name);

                magicPlant.SetupPlant(magicPlantObject);

                notify.DestroyNotif();

                isOnce = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
                notify.direction = Direction.up;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.GetPlantMedia(this);
                action.actionType = actionType;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                
                notify.direction = Direction.down;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.actionType = ActionType.None;
            }
        }

    }
}
