using System.Collections.Generic;
using UnityEngine;

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

        private EnemyPools enemyPools;
        private EnemyFabrick enemyFabrick;
        private List<EnemyStruct> enemies;

        public Player Player { get { return player; } }

        public override void StartController()
        {
            player = new Player();
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
        }

        public void UpdatePlayer()
        {
            UpdatePlayerRotation();
            if (characterController.enabled)
                player.Move();
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
    }
}

