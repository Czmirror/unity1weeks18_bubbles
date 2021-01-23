using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bubbles.Scripts.UI
{
    public class ButtonSceneMove : MonoBehaviour
    {
        public string loadScene;

        public void pushButton () {
            SceneManager.LoadScene ( loadScene );
        }
    }
}