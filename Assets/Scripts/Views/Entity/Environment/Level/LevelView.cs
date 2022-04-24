using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StudyGame
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private GameObject enterPortal;
        [SerializeField] private GameObject exitPortal;
        [SerializeField] private Transform enterPortalExitPoint;
        [SerializeField] private Transform exitPortalExitPoint;
        [SerializeField] private PortalView enterPortalView;
        [SerializeField] private PortalView exitPortalView;
        [SerializeField] private NavMeshSurface navMeshSurface;

        public GameObject EnterPortal { get { return enterPortal; } }
        public GameObject ExitPortal { get { return exitPortal; } }
        public Transform EnterPortalExitPoint { get { return enterPortalExitPoint; } }
        public Transform ExitPortalExitPoint { get { return exitPortalExitPoint; } }
        public PortalView EnterPortalView { get { return enterPortalView; } }
        public PortalView ExitPortalView { get { return exitPortalView; } }
        public NavMeshSurface NavMeshSurface { get { return navMeshSurface; } }
    }
}

