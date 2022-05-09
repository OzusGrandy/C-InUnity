using UnityEngine;
using UnityEngine.AI;

namespace StudyGame
{
    public sealed class RangeEnemy : Enemy
    {
        private float rotateSpeed;
        private Transform playerTransform;
        private Transform enemyTransform;
        private NavMeshAgent enemyAgent;

        public override void StartEnemy(float stoppingDistance, Transform player, Transform enemy, NavMeshAgent agent, float speed)
        {
            playerTransform = player;
            enemyTransform = enemy;
            enemyAgent = agent;
            rotateSpeed = 25f;
            enemyAgent.speed = speed;
            enemyAgent.stoppingDistance = stoppingDistance;
        }

        public override void Attack()
        {

        }

        public override void Move()
        {
            enemyAgent.SetDestination(playerTransform.position);

            if (Vector3.Distance(playerTransform.position, enemyTransform.position) <= enemyAgent.stoppingDistance)
            {
                enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, Quaternion.LookRotation(playerTransform.position - enemyTransform.position, Vector3.up), rotateSpeed * Time.deltaTime);
                Attack();
            }
        }
    }
}