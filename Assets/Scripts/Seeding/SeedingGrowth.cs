using AmazingAssets.DynamicRadialMasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PeaTFS
{
    public class SeedingGrowth : MonoBehaviour
    {
        [SerializeField] private SphereCollider radialCollider;
        [SerializeField] private DRMGameObject radialObject;
        [SerializeField] private float growthSpeed = 0.5f;
        [SerializeField] private GameObject arableSphereEffect;

        private bool isGrowth = false;
        private float radius = 6f;

        private bool firstStart = true;

        public void StartSeeding()
        {
            if(firstStart)
            {
                radialObject.smooth = 2f;
            }

            if(!isGrowth)
            {
                isGrowth= true;
                StartCoroutine(Seeding());

            }
        }

        public void StopSeeding()
        {
            isGrowth = false;
            StopCoroutine(Seeding());
        }

        IEnumerator Seeding()
        {
            if (firstStart)
            {
                yield return new WaitForSeconds(5f);
            }


            while(isGrowth)
            {
                radius += (Time.deltaTime*growthSpeed);

                radialCollider.radius = radius;
                radialObject.radius = radius;
                arableSphereEffect.transform.localScale = Vector3.one*radius;
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("obstacle"))
            {
                Debug.Log("There is Obstacle");
                StopSeeding();
            }

            if (other.CompareTag("FinishBorder"))
            {
                StopSeeding();

                GameManager.Instance.OnGameStateChange(GameState.Victory);
            }
        }
    }
}
