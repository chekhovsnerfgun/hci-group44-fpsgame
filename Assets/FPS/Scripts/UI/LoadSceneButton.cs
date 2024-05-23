using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Unity.FPS.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        string SceneName = "";
        bool Silly = false;

        public void ToggleSilly()
        {
            if (Silly)
                Silly = false;
            else
                Silly = true;
        }

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
                && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            switch (Silly)
            {
                case false:
                    SceneName = "MainScene";
                    break;
                case true:
                    SceneName = "SecondaryScene";
                    break;
            }

            SceneManager.LoadScene(SceneName);
        }
    }
}