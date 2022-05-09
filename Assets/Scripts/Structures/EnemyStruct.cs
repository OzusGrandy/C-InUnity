using UnityEngine;

namespace StudyGame
{
    public struct EnemyStruct
    {
        private UnitStructure enemyStats;
        private GameObject enemyObject;
        private EnemyView enemyView;
        private EnemyPool enemyPool;
        private Enemy enemyScript;

        public UnitStructure EnemyStats { get { return enemyStats; } set { enemyStats = value; } }
        public GameObject EnemyObject { get { return enemyObject; } set { enemyObject = value; } }
        public EnemyView EnemyView { get { return enemyView; } set { enemyView = value; } }
        public EnemyPool EnemyPool { get { return enemyPool; } set { enemyPool = value;} }
        public Enemy EnemyScript { get { return enemyScript; } set { enemyScript = value; } }

        public EnemyStruct(UnitStructure enemyStats, GameObject enemyObject, EnemyView enemyView, Enemy enemyScript) : this()
        {
            EnemyStats = enemyStats;
            EnemyObject = enemyObject;
            EnemyView = enemyView;
            EnemyScript = enemyScript;
        }
    }
}