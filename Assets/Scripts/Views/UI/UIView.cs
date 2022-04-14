using UnityEngine;
using UnityEngine.UI;

namespace StudyGame
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Text healthValueText;
        [SerializeField] private Text energyValueText;
        [SerializeField] private Text movementSpeedValueText;
        [SerializeField] private Text jumpSpeedValueText;
        [SerializeField] private GameObject miniMapCamera;
        [SerializeField] private GameObject playerIcon;
        [SerializeField] private GameObject keyIcon;
        [SerializeField] private GameObject enterPortalIcon;
        [SerializeField] private GameObject exitPortalIcon;

        private bool save, load, restart, exit = false;

        public Text HealthValueText { get { return healthValueText; } }
        public Text EnergyValueText { get { return energyValueText; } }
        public Text MovementSpeedValueText { get { return movementSpeedValueText; } }
        public Text JumpSpeedValueText { get { return jumpSpeedValueText; } }
        public GameObject MiniMapCamera { get { return miniMapCamera; } }
        public GameObject PlayerIcon { get { return playerIcon; } }
        public GameObject KeyIcon { get { return keyIcon; } }
        public GameObject EnterPortalIcon { get { return enterPortalIcon; } }
        public GameObject ExitPortalIcon { get { return exitPortalIcon; } }
        public bool IsRestart { get { return restart; } }
        public bool IsExit { get { return exit; } }
        public bool IsSave { get { return save; } set { save = false; } }
        public bool IsLoad { get { return load; } set { load = false; } }

        public delegate void UIAction();
        public event UIAction GetAction;

        public void Restart()
        {
            restart = true;
            GetAction?.Invoke();
        }

        public void Exit()
        {
            exit = true;
            GetAction?.Invoke();
        }

        public void Save()
        {
            save = true;
            GetAction?.Invoke();
        }

        public void Load()
        {
            load = true;
            GetAction?.Invoke();
        }
    }
}

