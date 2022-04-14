using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGame 
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _camLayer;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Rigidbody rigidbody;

        public Camera Camera { get { return _camera; } }
        public LayerMask layerMask { get { return _camLayer; } }
        public CharacterController CharacterController { get { return characterController; } }
        public Rigidbody Rigidbody { get { return rigidbody; } }
    }
}


