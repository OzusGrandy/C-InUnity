using UnityEngine;
using UnityEngine.AI;

namespace StudyGame
{
    public abstract class Enemy : Unit
    {
        public abstract void StartEnemy(float stoppingDistance, Transform player, Transform enemy, NavMeshAgent agent, float speed);
    }
}