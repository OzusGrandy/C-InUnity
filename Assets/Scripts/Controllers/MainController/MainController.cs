using UnityEngine;

namespace StudyGame
{
    public sealed class MainController : Controller
    {
        private LevelController levelController;
        private InteractiveObjectsController interactiveObjectsController;
        private GameObject player;
        private Player playerScript;
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
            dataController = new DataController();
            UI = new UIController();
            interactiveObjectsController.StartController();
            interactiveObjectsController.Get += LevelActivator;
            levelController.GetShowKey += ShowKey;
            levelController.GetTeleportation += Teleportation;
            levelController.GetShowPortalIcons += ShowPortalIcons;
            levelController.GetAddEnemies += AddEnemies;
            UI.GetSave += Save;
            UI.GetLoad += Load;
            dataController.CreateAutoSave += Save;
            UI.StartController();
            unitController.StartController();
            levelController.StartController();
            playerScript = unitController.Player;
            interactiveObjectsController.ReloadObjects();
            player = GameObject.Find("Player(Clone)");
            dataController.StartController();
            UI.SetStartTextParameters(playerScript.Health, playerScript.Energy, playerScript.MovementSpeed, playerScript.JumpSpeed);
        }



        public void Updates()
        {
            unitController.UpdatePlayer();
            interactiveObjectsController.UpdateObjects();
            UI.UpdateMiniMap(player.transform.position);
            if (Input.GetKeyDown(KeyCode.Escape))
                UI.Pause();
        }

        private void Teleportation()
        {
            unitController.PlayerTeleportation(levelController.CurrentTeleportationPoint);
        }

        private void Save()
        {
            dataController.Save(levelController.GetInfo(), playerScript.GetInfo(), levelController.ActiveLevel);
        }

        private void Load()
        {
            SaveStructure temp = dataController.Load();
            unitController.SwitchPlayerActive();
            levelController.SetLoadedData(temp.Levels, temp.ActiveLevel);
            unitController.SetLoadedData(temp.Player);
            unitController.SwitchPlayerActive();
        }

        private void LevelActivator()
        {
            levelController.SetActivated();
            UI.UpdateKeyIcon(false, interactiveObjectsController.KeyPosition);
            unitController.RemoveEnemies();
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

        private void AddEnemies(Transform[] spawnPoints)
        {
            unitController.AddEnemies(spawnPoints);
        }
    }
}

