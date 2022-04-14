using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StudyGame
{
    public sealed class UIController : Controller
    {
        private Text health;
        private Text energy;
        private Text movementSpeed;
        private Text jumpSpeed;
        private GameObject miniMapCamera;
        private GameObject playerIcon, keyIcon, enterPortalIcon, exitPortalIcon;
  

        private UIView view;

        private GameObject gameInterface;
        private GameObject pauseMenu;

        private bool pause;
        
        public string SetHealth { set { health.text = value; } }
        public string SetEnergy { set { energy.text = value; } }
        public string SetMovementSpeed { set { movementSpeed.text = value; } }
        public string SetJumpSpeed { set { jumpSpeed.text = value; } }

        public delegate void SaveOrLoad();
        public event SaveOrLoad GetSave;
        public event SaveOrLoad GetLoad;

        public override void StartController()
        {
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/UI"));
            view = GameObject.Find("UI(Clone)").GetComponent<UIView>();
            gameInterface = GameObject.Find("GameInterface");
            pauseMenu = GameObject.Find("PauseMenu");
            pauseMenu.SetActive(false);
            view.GetAction += Action;
            health = view.HealthValueText;
            energy = view.EnergyValueText;
            movementSpeed = view.MovementSpeedValueText;
            jumpSpeed = view.JumpSpeedValueText;
            miniMapCamera = view.MiniMapCamera;
            playerIcon = view.PlayerIcon;
            keyIcon = view.KeyIcon;
            enterPortalIcon = view.EnterPortalIcon;
            exitPortalIcon = view.ExitPortalIcon;
            pause = false;
            playerIcon.SetActive(true);
        }

        public void UpdateMiniMap(Vector3 playerPosition)
        {
            miniMapCamera.transform.position = new Vector3(playerPosition.x, miniMapCamera.transform.position.y, playerPosition.z);
            playerIcon.transform.position = new Vector3(playerPosition.x, playerIcon.transform.position.y, playerPosition.z);
        }

        public void UpdateKeyIcon(bool showKey, Vector3 keyPosition)
        {
            keyIcon.SetActive(showKey);
            keyIcon.transform.position = new Vector3(keyPosition.x, keyIcon.transform.position.y, keyPosition.z);
        }

        public void UpdatePortalIcons(bool showEnter, bool showExit, Vector3 exitPosition, Vector3 enterPosition)
        {
            enterPortalIcon.SetActive(showEnter);
            exitPortalIcon.transform.position = new Vector3(exitPosition.x, enterPortalIcon.transform.position.y, exitPosition.z);
            exitPortalIcon.SetActive(showExit);
            enterPortalIcon.transform.position = new Vector3(enterPosition.x, exitPortalIcon.transform.position.y, enterPosition.z);
        }

        public void Pause()
        {
            pause = !pause;
            if (pause)
            {
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            gameInterface.SetActive(!pause);
            pauseMenu.SetActive(pause);
        }

        private void Action()
        {
            if (view.IsSave)
            {
                view.IsSave = false;
                GetSave?.Invoke();
            }
            if (view.IsLoad)
            {
                view.IsLoad = false;
                GetLoad?.Invoke();
            }
            if (view.IsRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            if (view.IsExit)
            {
                Application.Quit();
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
            }
        }
    }
}

