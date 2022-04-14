using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public struct LevelStructure
    {
        private GameObject level;
        private LevelView levelView;
        private int levelVariant;
        private bool activated;
        
        public LevelStructure(GameObject addedLevel, LevelView addedLevelView, int setVariant, bool setActive)
        {
            level = addedLevel;
            levelView = addedLevelView;
            levelVariant = setVariant;
            activated = setActive;
        }

        public GameObject Level { get { return level; } }
        public LevelView LevelView { get { return levelView; } }
        public bool Activated { get { return activated; } }
        public int LevelVariant { get { return levelVariant; } }
    }
}
