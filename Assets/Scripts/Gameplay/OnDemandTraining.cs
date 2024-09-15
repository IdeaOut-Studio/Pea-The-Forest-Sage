using JetBrains.Annotations;
using PeaTFS;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OnDemandTraining : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Image imageRef;

    [Header("Image Placement")]
    [SerializeField] private Sprite[] questImage;

    [Header("Audio Effect")]
    [SerializeField] private AudioSource audioFx;
    [SerializeField] private AudioClip audioClip;

    [Header("Setup References")]
    public OnDemandStates ondemandState;

    public static OnDemandTraining Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        ondemandState = OnDemandStates.first;
        ShowQuest();
    }

    public void ShowQuest()
    {
        
        switch (ondemandState)
        {
            case OnDemandStates.first:
                imageRef.sprite = questImage[0];
                break;
            case OnDemandStates.second:
                imageRef.sprite = questImage[1];
                break;
            case OnDemandStates.third:
                imageRef.sprite = questImage[2];
                break;
            case OnDemandStates.forth:
                imageRef.sprite = questImage[3];
                break;
            case OnDemandStates.fiveth:
                imageRef.sprite = questImage[4];
                break;
            case OnDemandStates.sixth:
                imageRef.sprite = questImage[5];
                break;
            case OnDemandStates.sevent:
                imageRef.sprite = questImage[6];
                break;
        }
        StartCoroutine(PanelShowUp(2f, false));
    }

    public void HideQuest()
    {
        StartCoroutine(PanelShowUp(2f, true));

        if(ondemandState != OnDemandStates.fiveth && ondemandState != OnDemandStates.none)
            StartCoroutine(NextQuest());
    }

    IEnumerator NextQuest()
    {
        yield return new WaitForSeconds(2);
        ShowQuest();
    }

    IEnumerator PanelShowUp(float _targetTime, bool isInvert)
    {
        float time = 0;

        if (isInvert)
        {
            time = _targetTime;
            while (time > 0)
            {
                time -= Time.deltaTime;

                imageRef.color = new Color(1, 1, 1, time / _targetTime);

                yield return null;
            }
        }
        else
        {
            time = 0;
            while (time < _targetTime)
            {
                time += Time.deltaTime;

                imageRef.color = new Color(1, 1, 1, time / _targetTime);

                yield return null;
            }
        }
    }

    public void CompleteQuest()
    {
        audioFx.PlayOneShot(audioClip);
        HideQuest();
    }
}

[System.Serializable]
public enum OnDemandStates
{
    first, second, third, forth, fiveth, sixth, sevent, none
}
    