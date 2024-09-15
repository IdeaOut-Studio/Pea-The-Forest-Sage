using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

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

        [Header("Effects")]
        [SerializeField] private Particles particleFX;
        [SerializeField] private AudioSource audSFX;
        [SerializeField] private AudioClip audFXClip;

        private MagicPlant magicPlant;

        private bool toxic = true;

        public void ActivateSeeding()
        {
            if (toxic)
            {
                /* Do Some Animation of Activating, and Cinemachine Here*/
                this.GetComponent<SphereCollider>().enabled = false;

                magicPlant = Instantiate(prefabMagicPlant, this.transform);
                Debug.Log("Activating Seeding / Spawn Magic Plant "+magicPlant.name);
                particleFX.ActivateAllParticle();
                audSFX.Stop();
                audSFX.loop= false;
                audSFX.PlayOneShot(audFXClip);
                magicPlant.SetupPlant(magicPlantObject);

                notify.DestroyNotif();

                toxic = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                particleFX.ActivateBaseParticle();
                notify.direction = Direction.up;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.GetPlantMedia(this);
                action.actionType = actionType;
            }

            if (other.CompareTag("GrowingArea"))
            {
                if (toxic)
                {
                    particleFX.ActivateBaseParticle();
                    audSFX.Play();

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                particleFX.DisableParticle();
                notify.direction = Direction.down;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.actionType = ActionType.None;
            }
        }

    }
}
