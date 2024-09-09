using PeaTFS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurifyTimer : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Slider sliderTime;

    private Transform lookTarget;
    private float time = 0f;
    private float timeToComplete;
    private float sliderValue;

    private void Start()
    {
        sliderTime.gameObject.SetActive(false);
        lookTarget = Camera.main.transform;
    }

    public void SetupPurify(float _timeToComplete)
    {
        Debug.Log("Start Purifying");
        timeToComplete = _timeToComplete;
        sliderTime.gameObject.SetActive(true);

        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while(time < timeToComplete)
        {
            time += Time.deltaTime;
            sliderValue = time / timeToComplete;
            sliderTime.value = sliderValue;
            yield return null;
        }

        Debug.Log("Finish Purifying");
        
        GetComponentInParent<Obstacles>().PurifyObstacle();
        FindObjectOfType<SeedingGrowth>().StartSeeding();
        this.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 targetPostition = new Vector3(lookTarget.position.x,
                                       this.transform.position.y,
                                       lookTarget.position.z);

        transform.LookAt(targetPostition);
    }

    
}
