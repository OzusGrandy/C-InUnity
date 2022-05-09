using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StudyGame
{
    public sealed class UnitController : Controller
    {
        public Vector3 spawnPlayer;
        private Player player;
        private GameObject playerObject;
        private PlayerView playerView;
        private Camera camera;
        private CharacterController characterController;
        private Quaternion cameraTargetRotation;
        private Quaternion characterTargetRotation;

        private float mouseX;
        private float mouseY;
        private float maxYAngle;
        private float minYAngle;
        private float sensitivityMouse;
        private float enemyMeeleStoppingDistance;
        private float enemyRangeStoppingDistance;

        private EnemyPools enemyPools;
        private EnemyFabrick enemyFabrick;
        private List<EnemyStruct> enemies;
        private List<Unit> unitScripts;

        public Player Player { get { return player; } }

        public override void StartController()
        {
            unitScripts = new List<Unit>();
            player = new Player();
            unitScripts.Add(player);
            spawnPlayer = new Vector3(0f, 2f, 0f);
            playerObject = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player/Player"), spawnPlayer, Quaternion.identity);
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/EnemyStorage"));
            enemyPools = new EnemyPools();
            enemyFabrick = new EnemyFabrick();
            enemies = new List<EnemyStruct>();
            enemyPools.CreateEnemyPools();
            playerView = playerObject.GetComponent<PlayerView>();
            characterController = playerView.CharacterController;
            camera = playerView.Camera;
            SetStartParameters();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            characterTargetRotation = playerView.PlayerTransform.localRotation;
            cameraTargetRotation = camera.transform.localRotation;
            if (playerView.Rigidbody)
            {
                playerView.Rigidbody.freezeRotation = true;
            }
            player.CharacterController = playerView.CharacterController;
            player.PlayerView = playerView;
            enemyMeeleStoppingDistance = 2.5f;
            enemyRangeStoppingDistance = 50.0f;
        }

        public void UpdateUnits()
        {
            UpdatePlayerRotation();
            for (int i = 0; i < unitScripts.Count; i++)
            {
                if (i == 0)
                {
                    if (characterController.enabled)
                        unitScripts[i].Move();
                }
                else
                {
                    unitScripts[i].Move();
                }
            }

        }

        public void SwitchPlayerActive()
        {
            playerObject.SetActive(!playerObject.activeSelf);
        }
        public void SetLoadedData(PlayerInfo data)
        {
            characterController.enabled = false;
            playerView.PlayerTransform.position = new Vector3(data.XPos, data.YPos, data.ZRot);
            characterTargetRotation = Quaternion.Euler(data.XRot, data.YRot, data.ZRot);
            player.PlayerStats = data.PlayerStats;
            characterController.enabled = true;
        }

        public void PlayerTeleportation(Transform transform)
        {
            characterController.enabled = false;
            characterController.transform.position = transform.position;
            characterController.enabled = true;
        }

        private void UpdatePlayerRotation()
        {
            mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;

            characterTargetRotation *= Quaternion.Euler(0.0f, mouseX, 0.0f);
            cameraTargetRotation *= Quaternion.Euler(-mouseY, 0.0f, 0.0f);

            cameraTargetRotation = Math.ClampRotation(cameraTargetRotation, minYAngle, maxYAngle);

            playerView.PlayerTransform.localRotation = characterTargetRotation;

            camera.transform.localRotation = cameraTargetRotation;
        }

        private void SetStartParameters()
        {
            player.MovementSpeed = 8.0f;
            player.JumpSpeed = 8.0f;
            player.Health = 100.0f;
            player.Energy = 50.0f;

            maxYAngle = 90.0f;
            minYAngle = -90.0f;
            sensitivityMouse = 200.0f;
        }

        public void AddEnemies(Transform[] spawnPoints)
        {
            System.Random random = new System.Random();
            int tempTypeNumber = 0;
            int tempAttackType = 0;
            EnemyPool tempEnemyType = EnemyPool.red;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                tempTypeNumber = random.Next(0,4);
                switch (tempTypeNumber)
                {
                    case 0: tempEnemyType = EnemyPool.red; break;
                    case 1: tempEnemyType = EnemyPool.blue; break;
                    case 2: tempEnemyType = EnemyPool.green; break;
                    case 3: tempEnemyType = EnemyPool.yellow; break;
                    default: tempEnemyType = EnemyPool.red; break;
                }
                tempAttackType = random.Next(0, 2);
                if (tempAttackType == 0)
                {
                    enemies.Add(enemyFabrick.CreateEnemy(tempEnemyType, enemyPools, false, true));
                    enemies[enemies.Count - 1].EnemyScript.StartEnemy(enemyMeeleStoppingDistance, playerObject.transform, enemies[enemies.Count - 1].EnemyObject.transform, enemies[enemies.Count - 1].EnemyView.NavigationAgent, enemies[enemies.Count - 1].EnemyStats.MovementSpeed);
                }
                else
                {
                    enemies.Add(enemyFabrick.CreateEnemy(tempEnemyType, enemyPools, false, false));
                    enemies[enemies.Count - 1].EnemyScript.StartEnemy(enemyRangeStoppingDistance, playerObject.transform, enemies[enemies.Count - 1].EnemyObject.transform, enemies[enemies.Count - 1].EnemyView.NavigationAgent, enemies[enemies.Count - 1].EnemyStats.MovementSpeed);
                }
                enemies[enemies.Count - 1].EnemyObject.transform.position = spawnPoints[i].position;
                unitScripts.Add(enemies[enemies.Count - 1].EnemyScript);
                enemies[enemies.Count - 1].EnemyObject.SetActive(true);
            }
        }
        
        public void RemoveEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemyPools.ReturnEnemyAtPool(enemies[i].EnemyPool, enemies[i].EnemyObject);
                enemies[i].EnemyObject.SetActive(false);
                Object.Destroy(enemies[i].EnemyObject.GetComponent<NavMeshAgent>());
                Object.Destroy(enemies[i].EnemyObject.GetComponent<EnemyView>());
            }
            unitScripts.RemoveRange(1, unitScripts.Count - 1);
            enemies.Clear();
        }
    }
}

