using Cinemachine;
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

        [Header("Action Effect")]
        [SerializeField] private AudioSource audSfx;
        [SerializeField] private AudioClip audClip;
        [SerializeField] private Animator anim;
        [SerializeField] private Transform cameraRoot;
        [SerializeField] private CinemachineVirtualCamera CN_Cam;
        [SerializeField] private Particles particleFX;

        public bool isOndemand;

        private void Start()
        {
            arableMesh.enabled = false;
        }

        public void ActivateSeeding()
        {
            arableMesh.enabled = true;
            /* Do Some Animation of Activating, and Cinemachine Here*/
            audSfx.PlayOneShot(audClip);
            anim.SetTrigger("Seeding");

            particleFX.ActivateAllParticle();
            StartCoroutine(Rotate());

            Destroy(this.GetComponent<SphereCollider>());
            notify.DestroyNotif();
        }

        IEnumerator Rotate()
        {
            CN_Cam.enabled= true;
            Quaternion startRotation = cameraRoot.rotation;
            float endYRot = 360f;
            float duration = 5f;
            float t = 0;

            StartCoroutine(DeactiveCinema(duration));

            while (t < duration)
            {
                t = Mathf.Min(1f, t + Time.deltaTime / duration);
                Vector3 newEulerOffset = Vector3.up * (endYRot * t);
                // global y rotation
                cameraRoot.rotation = Quaternion.Euler(newEulerOffset) * startRotation;
                // local y rotation
                // transform.rotation = startRotation * Quaternion.Euler(newEulerOffset);
                
                yield return null;
                
            }
        }

        IEnumerator DeactiveCinema(float _duration)
        {
            yield return new WaitForSeconds(_duration);
            particleFX.DisableParticle();
            CN_Cam.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") )
            {
                //arableMesh.material.EnableKeyword("_EMISSION");
                
                notify.direction = Direction.up;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.actionType = actionType;
                if(isOndemand)
                {
                    if(OnDemandTraining.Instance.ondemandState == OnDemandStates.third)
                    {
                        OnDemandTraining.Instance.ondemandState = OnDemandStates.forth;
                        OnDemandTraining.Instance.CompleteQuest();
                        isOndemand = false;

                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") )
            {
                //arableMesh.material.DisableKeyword("_EMISSION");
                
                notify.direction = Direction.down;
                notify.Activate();

                PeaAction action = other.GetComponent<PeaAction>();
                action.actionType = ActionType.None;
            }
        }

    }
}
