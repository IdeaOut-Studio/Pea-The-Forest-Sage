using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public GameObject deathObject;
    public GameObject freshObject;
    public AnimationCurve curveGrowth;
    public AnimationCurve curveDecrease;
    public Vector3 origin = Vector3.one;
    public Vector3 target = new Vector3(1.25f, 1.25f, 1.25f);

    [SerializeField] private float durationAnim = 1f;
    private bool isDeathTree = true;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    public void Growth(float _durationGrow)
    {
        StartCoroutine(Growing(target, origin, _durationGrow));
    }

    private IEnumerator Growing(Vector3 _origin, Vector3 _target, float _duration)
    {
        //Debug.Log("Start Ggrowing Plant with Step");

        float journey = 0f;
        while (journey <= _duration)
        {
            if (isDeathTree)
            {
                freshObject.SetActive(true);
                deathObject.SetActive(false);
                //StartCoroutine(Growing(origin, target, durationAnim));
                Destroy(this.GetComponent<BoxCollider>());
            }
            else
            {
                yield return null;
            }

            journey = journey+ Time.deltaTime;

            float percent = Mathf.Clamp01(journey / _duration);
            float curvePercent = curveGrowth.Evaluate(percent);

            transform.localScale = Vector3.Lerp(_origin, _target, curvePercent);

            yield return null;
        }
        //yield return new WaitForSeconds(0.5f);
        //Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrowingArea"))
        {
            Growth(durationAnim);
        }
    }
}
