using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGame 
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [SerializeField] private LayerMask camLayer;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Transform playerTransform;

        public Camera Camera { get { return camera; } }
        public LayerMask layerMask { get { return camLayer; } }
        public CharacterController CharacterController { get { return characterController; } }
        public Rigidbody Rigidbody { get { return rigidbody; } }
        public Transform PlayerTransform { get { return playerTransform; } }
    }
}


