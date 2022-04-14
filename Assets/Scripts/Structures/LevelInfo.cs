using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public struct LevelInfo
    {
        private int levelVariant;
        private bool activated;


        public bool Activated { get { return activated; } set { activated = value; } }
        public int LevelVariant { get { return levelVariant; } set { levelVariant = value; } }
    }
}

