using UnityEngine;

namespace StudyGame
{
    public sealed class MainController : Controller
    {
        private LevelController levelController;
        private InteractiveObjectsController interactiveObjectsController;
        private GameObject player;
        private PlayerController playerController;
        private UnitController unitController;
        private DataController dataController;
        private MainEntryPoint mainView;
        private UIController UI;

        public override void StartController()
        {
            mainView = GameObject.Find("MainEntryPoint").GetComponent<MainEntryPoint>();
            unitController = new UnitController();
            levelController = new LevelController();
            interactiveObjectsController = new InteractiveObjectsController();
            playerController = new PlayerController();
            dataController = new DataController();
            UI = new UIController();
            interactiveObjectsController.StartController();
            interactiveObjectsController.Get += LevelActivator;
            levelController.GetShowKey += ShowKey;
            levelController.GetTeleportation += Teleportation;
            levelController.GetShowPortalIcons += ShowPortalIcons;
            UI.GetSave += Save;
            UI.GetLoad += Load;
            dataController.CreateAutoSave += Save;
            UI.StartController();
            levelController.StartController();
            unitController.StartController();
            playerController.StartController();
            UI.SetHealth = playerController.GetHealth.ToString();
            UI.SetEnergy = playerController.GetEnergy.ToString();
            UI.SetMovementSpeed = playerController.GetMovementSpeed.ToString();
            UI.SetJumpSpeed = playerController.GetJumpSpeed.ToString();
            interactiveObjectsController.ReloadObjects();
            player = GameObject.Find("Player(Clone)");
            dataController.StartController();

        }

        private void Teleportation()
        {
            playerController.Teleportation(levelController.CurrentTeleportationPoint);
        }

        public void Updates()
        {
            playerController.UpdateViev();
            interactiveObjectsController.UpdateObjects();
            UI.UpdateMiniMap(player.transform.position);
            if (Input.GetKeyDown(KeyCode.Escape))
                UI.Pause();
        }

        private void Save()
        {
            dataController.Save(levelController.GetInfo(), playerController.GetInfo(), levelController.ActiveLevel);
        }

        private void Load()
        {
            SaveStructure temp = dataController.Load();
            playerController.SwitchPlayerActive();
            levelController.SetLoadedData(temp.Levels, temp.ActiveLevel);
            playerController.SetLoadedData(temp.Player);
            playerController.SwitchPlayerActive();
        }

        private void LevelActivator()
        {
            levelController.SetActivated();
            UI.UpdateKeyIcon(false, interactiveObjectsController.KeyPosition);
        }
        
        private void ShowKey(bool show)
        {
            interactiveObjectsController.HideOrShowKey(show);
            UI.UpdateKeyIcon(show, interactiveObjectsController.KeyPosition);
        }

        private void ShowPortalIcons(bool showEnter, bool showExit, Transform exit, Transform enter)
        {
            UI.UpdatePortalIcons(showEnter, showExit, new Vector3(enter.position.x, enter.position.y, enter.position.z), new Vector3(exit.position.x, exit.position.y, exit.position.z));
        }
    }
}

