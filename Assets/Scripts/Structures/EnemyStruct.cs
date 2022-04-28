using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public struct EnemyStruct
    {
        private UnitStructure enemyStats;
        private GameObject enemyObject;
        private EnemyView enemyView;

        public UnitStructure EnemyStats { get { return enemyStats; } set { enemyStats = value; } }
        public GameObject EnemyObject { get { return enemyObject; } set { enemyObject = value; } }
        public EnemyView EnemyView { get { return enemyView; } set { enemyView = value; } }
    }
}