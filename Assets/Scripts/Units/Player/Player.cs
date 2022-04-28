using UnityEngine;

namespace StudyGame
{
    public sealed class Player : Unit
    {

        private PlayerView playerView;
        private UnitStructure playerStats;

        private Vector3 moveDirection;
        private CharacterController characterController;
        
        private float diagonalDeceleration = 0.75f;
        private float acceleration = 1.7f;

        public float Health { get { return playerStats.Health; } set { playerStats.Health = value; } }
        public float Energy { get { return playerStats.Energy; } set { playerStats.Energy = value; } }
        public float MovementSpeed { get { return playerStats.MovementSpeed; } set { playerStats.MovementSpeed = value; } }
        public float JumpSpeed { get { return playerStats.JumpSpeed; } set { playerStats.JumpSpeed = value; } }

        public UnitStructure PlayerStats { set { playerStats = value; } }

        public CharacterController CharacterController { set { characterController = value; } }
        public PlayerView PlayerView { set { playerView = value; } }

        public override void Move()
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
                currentSpeed = playerStats.MovementSpeed * diagonalDeceleration;
            }

            if (characterController.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection = playerView.PlayerTransform.TransformDirection(moveDirection);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveDirection.x *= currentSpeed * acceleration;
                    moveDirection.z *= currentSpeed * acceleration;
                }
                else
                {
                    moveDirection.x *= currentSpeed;
                    moveDirection.z *= currentSpeed;
                }

            }

            if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
            {
                moveDirection.y = playerStats.JumpSpeed;
            }

            moveDirection.y -= Physics.Gravity * Time.deltaTime;

            characterController.Move(moveDirection * Time.deltaTime);
        }

        public override void Attack()
        {
            
        }

        public PlayerInfo GetInfo()
        {
            PlayerInfo temp = new PlayerInfo();
            temp.XPos = playerView.PlayerTransform.position.x;
            temp.YPos = playerView.PlayerTransform.position.y;
            temp.ZPos = playerView.PlayerTransform.position.z;
            temp.XRot = playerView.PlayerTransform.rotation.x;
            temp.YRot = playerView.PlayerTransform.rotation.y;
            temp.ZRot = playerView.PlayerTransform.rotation.z;
            temp.WRot = playerView.PlayerTransform.rotation.w;
            temp.PlayerStats = playerStats;
            return temp;
        }
    }
}

