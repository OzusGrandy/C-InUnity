using UnityEngine;

namespace StudyGame
{
    public sealed class PlayerController : Controller
    {

        private float _gravity;

        private Camera _camera;

        private GameObject _playerObject;

        private PlayerView playerView;
        private UnitStructure playerStats;

        private Vector3 _moveDirection;
        private CharacterController _characterController;

        private float _mouseX;
        private float _mouseY;

        private Transform _player;

        private Quaternion _cameraTargetRotation;
        private Quaternion _characterTargetRotation;
        
        private float _diagonalDeceleration = 0.75f;
        private float _acceleration = 1.7f;
        private float _maxYAngle = 90.0f;
        private float _minYAngle = -90.0f;
        private float _sensitivityMouse = 200.0f;


        public override void StartController()
        {
            _playerObject = GameObject.Find("Player(Clone)");
            playerView = _playerObject.GetComponent<PlayerView>();
            _camera = playerView.Camera;
            _player = _playerObject.transform;
            _gravity = 9.81f;
            playerStats.MovementSpeed = 8.0f;
            playerStats.JumpSpeed = 8.0f;
            playerStats.Health = 100.0f;


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _characterTargetRotation = _player.localRotation;
            _cameraTargetRotation = _camera.transform.localRotation;
            _characterController = playerView.CharacterController;
            if (playerView.Rigidbody)
            {
                playerView.Rigidbody.freezeRotation = true;
            }
        }

        public float GetHealth { get { return playerStats.Health; } }
        public float GetEnergy { get { return playerStats.Energy; } }
        public float GetMovementSpeed { get { return playerStats.MovementSpeed; } }
        public float GetJumpSpeed { get { return playerStats.JumpSpeed; } }

        public void MoveAndJump()
        {
            if (_characterController.enabled)
            {
                float currentSpeed = playerStats.MovementSpeed;
                bool forvardOrBack;
                bool leftOrRight;
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    forvardOrBack = true;
                }
                else
                {
                    forvardOrBack = false;
                }

                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    leftOrRight = true;
                }
                else
                {
                    leftOrRight = false;
                }

                if (forvardOrBack && leftOrRight)
                {
                    currentSpeed = playerStats.MovementSpeed * _diagonalDeceleration;
                }

                if (_characterController.isGrounded)
                {
                    _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                    _moveDirection = _player.TransformDirection(_moveDirection);

                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        _moveDirection.x *= currentSpeed * _acceleration;
                        _moveDirection.z *= currentSpeed * _acceleration;
                    }
                    else
                    {
                        _moveDirection.x *= currentSpeed;
                        _moveDirection.z *= currentSpeed;
                    }

                }

                if (Input.GetKey(KeyCode.Space) && _characterController.isGrounded)
                {
                    _moveDirection.y = playerStats.JumpSpeed;
                }

                _moveDirection.y -= _gravity * Time.deltaTime;

                _characterController.Move(_moveDirection * Time.deltaTime);
            }
        }

        public void Teleportation(Transform transform)
        {
            _characterController.enabled = false;
            _characterController.transform.position = transform.position;
            _characterController.enabled = true;
        }



        public void UpdateViev()
        {
            _mouseX = Input.GetAxis("Mouse X") * _sensitivityMouse * Time.deltaTime;
            _mouseY = Input.GetAxis("Mouse Y") * _sensitivityMouse * Time.deltaTime;

            _characterTargetRotation *= Quaternion.Euler(0.0f, _mouseX, 0.0f);
            _cameraTargetRotation *= Quaternion.Euler(-_mouseY, 0.0f, 0.0f);

            _cameraTargetRotation = ClampYRotation(_cameraTargetRotation);

            _player.localRotation = _characterTargetRotation;

            _camera.transform.localRotation = _cameraTargetRotation;

            MoveAndJump();
        }

        Quaternion ClampYRotation(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, _minYAngle, _maxYAngle);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        public PlayerInfo GetInfo()
        {
            PlayerInfo temp = new PlayerInfo();
            temp.XPos = _player.position.x;
            temp.YPos = _player.position.y;
            temp.ZPos = _player.position.z;
            temp.XRot = _player.rotation.x;
            temp.YRot = _player.rotation.y;
            temp.ZRot = _player.rotation.z;
            temp.WRot = _player.rotation.w;
            temp.PlayerStats = playerStats;
            return temp;
        }

        public void SetLoadedData(PlayerInfo data)
        {
            _characterController.enabled = false;
            _playerObject.transform.position = new Vector3(data.XPos,data.YPos,data.ZRot);
            _characterTargetRotation = Quaternion.Euler(data.XRot,data.YRot,data.ZRot);
            playerStats = data.PlayerStats;
            _characterController.enabled = true;
        }

        public void SwitchPlayerActive()
        {
            _playerObject.SetActive(!_playerObject.activeSelf);
        }
    }
}

