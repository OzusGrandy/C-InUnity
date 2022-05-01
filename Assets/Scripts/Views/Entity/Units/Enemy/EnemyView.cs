using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private NavMeshAgent navigationAgent;

    public Transform GetEnemyTransform { get { return enemy; } }
    public Transform SetEnemyTransform { set { enemy = gameObject.transform; } }
    public NavMeshAgent NavigationAgent { get { return navigationAgent; } set { navigationAgent = value; } }

}
