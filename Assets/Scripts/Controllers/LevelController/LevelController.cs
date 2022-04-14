using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public sealed class LevelController : Controller
    {
        private GameObject[] levelPrefabs;
        private Material[] materialPrefabs;

        private List<LevelStructure> levels;
        private Key key;

        private int activeLevel;

        private Transform currentTeleportationPoint;
        private Transform currentEnterPortal, currentExitPortal;

        public delegate void Teleportation();
        public event Teleportation GetTeleportation;

        public delegate void ShowKey(bool show);
        public event ShowKey GetShowKey;

        public delegate void ShowPortalIcons(bool showEnter, bool showExit, Transform enter, Transform exit);
        public event ShowPortalIcons GetShowPortalIcons;

        public Transform CurrentTeleportationPoint { get { return currentTeleportationPoint; } }
        public Transform CurrentEnterPortal { get { return currentEnterPortal; } }
        public Transform CurrentExitPortal { get { return currentExitPortal; } }

        public int ActiveLevel { get { return activeLevel; } }

        public override void StartController()
        {
            levels = new List<LevelStructure>();
            levelPrefabs = Resources.LoadAll<GameObject>("Prefabs/Levels/HexagonalPartsLevels");
            materialPrefabs = Resources.LoadAll<Material>("Materials/Portal");
            AddLevel();
            activeLevel = 0;
        }

        private void AddLevel()
        {
            System.Random random = new System.Random();
            var newLevel = Object.Instantiate(levelPrefabs[random.Next(levelPrefabs.Length)]);
            switch (newLevel.name)
            {
                case "LevelVariant1(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 1, false));
                    break;                
                case "LevelVariant2(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 2, false));
                    break;                
                case "LevelVariant3(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 3, false));
                    break;                
                case "LevelVariant4(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 4, false));
                    break;                
                case "LevelVariant5(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 5, false));
                    break;                
                case "LevelVariant6(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 6, false));
                    break;                
                case "LevelVariant7(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 7, false));
                    break;                
                case "LevelVariant8(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 8, false));
                    break;
            }
            levels[levels.Count - 1].LevelView.EnterPortalView.GetActivate += LoadLevel;
            levels[levels.Count - 1].LevelView.ExitPortalView.GetActivate += LoadLevel;
            GetShowKey.Invoke(true);
            if (levels.Count - 1 != 0)
            {
                currentEnterPortal = levels[levels.Count - 1].LevelView.ExitPortal.transform;
                currentExitPortal = levels[levels.Count - 1].LevelView.EnterPortal.transform;

                GetShowPortalIcons.Invoke(false, false, currentEnterPortal, currentExitPortal);
            }

        }

        private void AddLoadedLevel(int levelVariant, bool activated)
        {
            var newLevel = Object.Instantiate(levelPrefabs[levelVariant - 1]);
            switch (newLevel.name)
            {
                case "LevelVariant1(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 1, activated));
                    break;
                case "LevelVariant2(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 2, activated));
                    break;
                case "LevelVariant3(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 3, activated));
                    break;
                case "LevelVariant4(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 4, activated));
                    break;
                case "LevelVariant5(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 5, activated));
                    break;
                case "LevelVariant6(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 6, activated));
                    break;
                case "LevelVariant7(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 7, activated));
                    break;
                case "LevelVariant8(Clone)":
                    levels.Add(new LevelStructure(newLevel, newLevel.GetComponent<LevelView>(), 8, activated));
                    break;
            }
            levels[levels.Count - 1].LevelView.EnterPortalView.GetActivate += LoadLevel;
            levels[levels.Count - 1].LevelView.ExitPortalView.GetActivate += LoadLevel;
            if (!activated)
            {
                GetShowKey.Invoke(true);
                GetShowPortalIcons.Invoke(false, false, currentEnterPortal, currentExitPortal);
            }
            else
            {
                GetShowKey.Invoke(false);
                SetPortals(levels.Count - 1);
            }
        }

        private void LoadLevel()
        {
            if (levels[activeLevel].LevelView.ExitPortalView.Activated)
            {
                if(activeLevel == levels.Count - 1)
                {
                    AddLevel();
                    activeLevel++;
                    levels[activeLevel - 1].Level.SetActive(false);
                    levels[activeLevel - 1].LevelView.ExitPortalView.Action();
                    currentTeleportationPoint = levels[activeLevel].LevelView.EnterPortalExitPoint;
                    GetTeleportation.Invoke();
                }
                else
                {
                    activeLevel++;
                    levels[activeLevel - 1].Level.SetActive(false);
                    levels[activeLevel - 1].LevelView.ExitPortalView.Action();
                    levels[activeLevel].Level.SetActive(true);
                    currentTeleportationPoint = levels[activeLevel].LevelView.EnterPortalExitPoint;

                    GetTeleportation.Invoke();

                    currentEnterPortal = levels[activeLevel].LevelView.ExitPortal.transform;
                    currentExitPortal = levels[activeLevel].LevelView.EnterPortal.transform;

                    GetShowPortalIcons.Invoke(true, activeLevel != 0, currentEnterPortal, currentExitPortal);
                }
            } else if (levels[activeLevel].LevelView.EnterPortalView.Activated)
            {
                if (activeLevel != 0)
                {
                    activeLevel--;
                    levels[activeLevel + 1].Level.SetActive(false);
                    levels[activeLevel + 1].LevelView.EnterPortalView.Action();
                    levels[activeLevel].Level.SetActive(true);
                    currentTeleportationPoint = levels[activeLevel].LevelView.ExitPortalExitPoint;
                    GetTeleportation.Invoke();
                    currentEnterPortal = levels[activeLevel].LevelView.ExitPortal.transform;
                    currentExitPortal = levels[activeLevel].LevelView.EnterPortal.transform;

                    GetShowPortalIcons.Invoke(true, activeLevel != 0, currentEnterPortal, currentExitPortal);
                }
            }
        }

        public void SetActivated()
        {
            LevelStructure temp = new LevelStructure(levels[activeLevel].Level, levels[activeLevel].LevelView, levels[activeLevel].LevelVariant, true);
            levels[activeLevel] = temp;
            SetPortals(activeLevel);
        }        
        
        private void SetPortals(int numberOfLevel)
        {
            if (activeLevel != 0)
            {
                levels[numberOfLevel].LevelView.EnterPortal.GetComponent<Renderer>().material = materialPrefabs[0];
                levels[numberOfLevel].LevelView.EnterPortalView.State = PortalState.Active;
            }
            levels[numberOfLevel].LevelView.ExitPortal.GetComponent<Renderer>().material = materialPrefabs[1];
            levels[numberOfLevel].LevelView.ExitPortalView.State = PortalState.Active;

            currentEnterPortal = levels[numberOfLevel].LevelView.ExitPortal.transform;
            currentExitPortal = levels[numberOfLevel].LevelView.EnterPortal.transform;

            GetShowPortalIcons.Invoke(true, activeLevel != 0, currentEnterPortal, currentExitPortal);
        }

        public LevelInfo[] GetInfo()
        {
            LevelInfo[] temp = new LevelInfo[levels.Count];
            for (int i = 0; i < levels.Count; i++)
            {
                temp[i].Activated = levels[i].Activated;
                temp[i].LevelVariant = levels[i].LevelVariant;
            }
            return temp;
        }

        public void SetLoadedData(LevelInfo[] data, int activeLevel)
        {
            this.activeLevel = activeLevel;
            for (int i = 0; i < levels.Count; i++)
            {
                Object.Destroy(levels[i].Level);
            }
            levels.Clear();
            for (int i = 0; i < data.Length; i++)
            {
                AddLoadedLevel(data[i].LevelVariant, data[i].Activated);
                levels[levels.Count - 1].Level.SetActive(false);
            }
            levels[activeLevel].Level.SetActive(true);

        }
    }
}
