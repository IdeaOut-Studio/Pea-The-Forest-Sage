using UnityEngine;

[CreateAssetMenu(fileName = "Magic Plants", menuName = "Create Magic Plant", order = 1)]
public class SO_Plant : ScriptableObject
{
    public string name = "";
    public GameObject sproutStep;
    public GameObject jevenileStep;
    public GameObject adultStep;

    public PlantType plantType;

    [Tooltip("time to complete the action (Purify/Destroy)")]
    public float timeToComplete = 5f;
    [Tooltip("Gworth Time Per Step")]
    public float timeToGrowth = 1f;
}

[System.Serializable]
public enum PlantType
{
    Purify,
    Destroyer
}
