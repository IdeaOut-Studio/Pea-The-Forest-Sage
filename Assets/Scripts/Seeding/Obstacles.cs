using AmazingAssets.DynamicRadialMasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeaTFS
{
    public class Obstacles : MonoBehaviour
    {
        [Header("Obstacle Properties")]
        [SerializeField] private float timeToPurify = 10f;
        [SerializeField] private GameObject obstacleObject;
        [SerializeField] private ObstacleType type = ObstacleType.Defatult;

        [SerializeField] private MeshRenderer toxicMeshMaterial;
        [SerializeField] private Material freshMaterial;

        public void PurifyObstacle()
        {
            if(obstacleObject != null)
            {
                Destroy(obstacleObject);
            }

            if(toxicMeshMaterial != null)
            {
                toxicMeshMaterial.material= freshMaterial;  
            }
        }
    }
}