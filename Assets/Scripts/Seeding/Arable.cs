using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeaTFS
{
    public class Arable : MonoBehaviour
    {

        [SerializeField] private MeshRenderer arableMesh;
        [SerializeField] private NotificationObject notify;
        [SerializeField] private SeedingGrowth seedingScript;

        [Header("Action Type")]
        [SerializeField] private ActionType actionType = ActionType.ActivateMagicSeed;

        public void ActivateSeeding()
        {
            /* Do Some Animation of Activating, and Cinemachine Here*/
            Destroy(this.GetComponent<SphereCollider>());
            notify.DestroyNotif();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") )
            {
                arableMesh.material.EnableKeyword("_EMISSION");
                notify.direction = Direction.up;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.actionType = actionType;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") )
            {
                arableMesh.material.DisableKeyword("_EMISSION");
                notify.direction = Direction.down;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.actionType = ActionType.None;
            }
        }

    }
}
