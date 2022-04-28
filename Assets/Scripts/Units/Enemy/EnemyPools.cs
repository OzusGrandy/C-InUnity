using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public class EnemyPools
    {
        private List<GameObject> redEnemyPool;
        private List<GameObject> blueEnemyPool;
        private List<GameObject> greenEnemyPool;
        private List<GameObject> yellowEnemyPool;

        public void CreateEnemyPools()
        {
            redEnemyPool = new List<GameObject>(); 
            blueEnemyPool = new List<GameObject>(); 
            greenEnemyPool = new List<GameObject>(); 
            yellowEnemyPool = new List<GameObject>();
            GameObject[] redEnemies = new GameObject[4];
            GameObject[] blueEnemies = new GameObject[4];
            GameObject[] greenEnemies = new GameObject[4];
            GameObject[] yellowEnemies = new GameObject[4];
            redEnemies = GameObject.FindGameObjectsWithTag("RedEnemy");
            blueEnemies = GameObject.FindGameObjectsWithTag("BlueEnemy");
            greenEnemies = GameObject.FindGameObjectsWithTag("GreenEnemy");
            yellowEnemies = GameObject.FindGameObjectsWithTag("YellowEnemy");

            for (int i =0; i < redEnemies.Length; i++)
            {
                redEnemyPool.Add(redEnemies[i]);
                blueEnemyPool.Add(blueEnemies[i]);
                greenEnemyPool.Add(greenEnemies[i]);
                yellowEnemyPool.Add(yellowEnemies[i]);
            }
        }

        public GameObject GetEnemyFromPool(EnemyPool pool)
        {
            GameObject temp;
            switch (pool)
            {
                case EnemyPool.red:
                    temp = redEnemyPool[redEnemyPool.Count - 1];
                    redEnemyPool.RemoveAt(redEnemyPool.Count - 1);
                    break;                
                case EnemyPool.blue:
                    temp = blueEnemyPool[blueEnemyPool.Count - 1];
                    blueEnemyPool.RemoveAt(blueEnemyPool.Count - 1);
                    break;                
                case EnemyPool.green:
                    temp = greenEnemyPool[greenEnemyPool.Count - 1];
                    greenEnemyPool.RemoveAt(greenEnemyPool.Count - 1);
                    break;                
                default:
                    temp = yellowEnemyPool[yellowEnemyPool.Count - 1];
                    yellowEnemyPool.RemoveAt(yellowEnemyPool.Count - 1);
                    break;
            }
            return temp;
        }
        
        public void ReturnEnemyAtPool(EnemyPool pool, GameObject enemy)
        {
            switch (pool)
            {
                case EnemyPool.red:
                    redEnemyPool.Add(enemy);
                    break;
                case EnemyPool.blue:
                    blueEnemyPool.Add(enemy);
                    break;
                case EnemyPool.green:
                    greenEnemyPool.Add(enemy);
                    break;
                default:
                    yellowEnemyPool.Add(enemy);
                    break;
            }
        }
    }
}