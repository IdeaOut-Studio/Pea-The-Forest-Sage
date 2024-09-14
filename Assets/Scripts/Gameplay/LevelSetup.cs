using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeaTFS
{
    public class MenuLevelSetup : MonoBehaviour
    {

        private void Awake()
        {
            GetGameCanvas();
        }

        public void NextScene(string _nextScene)
        {
            SceneManager.LoadScene(_nextScene);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void GetGameCanvas()
        {

        }
    }    
}