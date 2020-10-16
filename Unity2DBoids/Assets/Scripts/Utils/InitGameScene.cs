using UnityEngine;
using UnityEngine.SceneManagement;

namespace nl.FutureWhiz.Unity2DBoids.Utils
{
    public class InitGameScene : MonoBehaviour
    {
        /// <summary>
        /// Loads Up GameScene
        /// </summary>
        private void Awake()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}