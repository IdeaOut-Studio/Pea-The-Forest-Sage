using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    [Header("Menu Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelBtn(string levelToLoad)
    {

        if (levelToLoad != "")
        {
            if(mainMenu!= null)
                mainMenu.SetActive(false);
            loadingScreen.SetActive(true);
            StartCoroutine(LoadLevelAsync(levelToLoad));

        }
        else
        {
            Debug.Log("locked");
        }
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressive = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value= progressive;
            yield return null;
        }
    }
}
