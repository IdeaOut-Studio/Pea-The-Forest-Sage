using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPlantStep : MonoBehaviour
{
    public GameObject mesh;
    public AnimationCurve animationCurve;
    public Vector3 origin = Vector3.one;
    public Vector3 target = new Vector3(1.25f, 1.25f, 1.25f);
    public bool isAdult = false;

    public void Growth(float _durationGrow)
    {
        StartCoroutine(Growing(origin, target, _durationGrow));
        mesh.SetActive(true);
    }

    private IEnumerator Growing(Vector3 _origin, Vector3 _target, float _duration)
    {
        //Debug.Log("Start Ggrowing Plant with Step");

        float journey = 0f;
        while (journey <= _duration)
        {
            journey = journey+ Time.deltaTime;

            float percent = Mathf.Clamp01(journey / _duration);
            float curvePercent = animationCurve.Evaluate(percent);

            transform.localScale = Vector3.Lerp(_origin, _target, curvePercent);

            if (isAdult)
            {
                mesh.GetComponent<Animator>().SetTrigger("Purifying");
            }

            yield return null;
        }

        GetComponentInParent<MagicPlant>().StartGrowth();
        //yield return new WaitForSeconds(0.5f);
        //Destroy(this.gameObject);
    }

}
