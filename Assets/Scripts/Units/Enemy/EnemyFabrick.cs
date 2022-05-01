using UnityEngine.AI;

namespace StudyGame
{
    public class EnemyFabrick
    {
        private int smallEnemyHealth = 20;
        private int smallEnemyEnergy = 50;
        private int smallEnemySpeed = 2;
        private int smallEnemyJumpSpeed = 2;

        private int mediumEnemyHealth = 240;
        private int mediumEnemyEnergy = 225;
        private int mediumEnemySpeed = 9;
        private int mediumEnemyJumpSpeed = 9;

        private int largeEnemyHealth = 500;
        private int largeEnemyEnergy = 500;
        private int largeEnemySpeed = 16;
        private int largeEnemyJumpSpeed = 16;


        public EnemyStruct CreateEnemy(EnemyPool typeOfEnemy, EnemyPools pools, bool random)
        {
            EnemyStruct temp = new EnemyStruct();
            if (random)
            {
                temp.EnemyStats = RandomEnemyStats();
            }
            else
            {
                temp.EnemyStats = SpecificEnemyStats(typeOfEnemy);
            }
            temp.EnemyObject = pools.GetEnemyFromPool(typeOfEnemy);
            temp.EnemyView = temp.EnemyObject.AddComponent<EnemyView>();
            temp.EnemyView.NavigationAgent = temp.EnemyObject.AddComponent<NavMeshAgent>();
            temp.EnemyView.SetEnemyTransform = temp.EnemyObject.transform;
            return temp;
        }

        private UnitStructure RandomEnemyStats()
        {
            UnitStructure temp = new UnitStructure();
            System.Random random = new System.Random();
            temp.Health = random.Next(smallEnemyHealth, largeEnemyHealth);
            temp.Energy = random.Next(smallEnemyEnergy, largeEnemyEnergy);
            temp.MovementSpeed = random.Next(smallEnemySpeed, largeEnemySpeed);
            temp.JumpSpeed = random.Next(smallEnemyJumpSpeed, largeEnemyJumpSpeed);
            return temp;
        }
        private UnitStructure SpecificEnemyStats(EnemyPool typeOfEnemy)
        {
            UnitStructure temp;
            switch (typeOfEnemy)
            {
                case EnemyPool.red: temp = new UnitStructure(mediumEnemyEnergy,mediumEnemyHealth,mediumEnemySpeed,mediumEnemyJumpSpeed); break;
                case EnemyPool.blue: temp = new UnitStructure(mediumEnemyEnergy, mediumEnemyHealth, mediumEnemySpeed, mediumEnemyJumpSpeed); break;
                case EnemyPool.green: temp = new UnitStructure(smallEnemyEnergy, smallEnemyHealth, largeEnemySpeed, largeEnemyJumpSpeed); break;
                default: temp = new UnitStructure(largeEnemyEnergy, largeEnemyHealth, smallEnemySpeed, smallEnemyJumpSpeed); break;
            }
            return temp;
        }
    }
}