using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPlant : MonoBehaviour
{
    //public SO_Plant magicPlant;
    public PurifyTimer purifyTimer;
    public List<MagicPlantStep> plantStep;

    private string name;
    private PlantType plantType;

    private MagicPlantStep curentPlantStep;

    [Tooltip("time to complete the action (Purify/Destroy)")]
    private float timeToComplete;
    [Tooltip("Gworth Time Per Step")]
    public float timeToGrowth;

    [Range(1,3)]
    private int growthStep = 0;

    public void SetupPlant(SO_Plant _plant)
    {
        plantStep.Add(_plant.sproutStep.GetComponent<MagicPlantStep>());
        plantStep.Add(_plant.jevenileStep.GetComponent<MagicPlantStep>());
        plantStep.Add(_plant.adultStep.GetComponent<MagicPlantStep>());

        plantType= _plant.plantType;
        timeToComplete= _plant.timeToComplete;
        timeToGrowth= _plant.timeToGrowth;

        StartGrowth();
    }

    public void StartGrowth()
    {
        if(growthStep <= plantStep.Count-1)
        {
            //Start Growing
            StartCoroutine(Growing());
        }
        else
        {
            //Start Purify
            purifyTimer.SetupPurify(timeToComplete);
        }
    }
    
    private IEnumerator Growing()
    {

        //Debug.Log("On Growing");

        if (curentPlantStep != null)
        {
            Destroy(curentPlantStep.gameObject); 
            curentPlantStep = null;
        }

        MagicPlantStep _step = Instantiate(plantStep[growthStep], this.transform); 
        curentPlantStep = _step;
        _step.Growth(1f);

        growthStep += 1;
        yield return null;
    }
    
}
