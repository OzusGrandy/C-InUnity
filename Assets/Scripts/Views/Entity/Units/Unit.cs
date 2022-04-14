using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public abstract class Unit : Entity
    {
        private Rigidbody _rigidbody;
        public float Speed = 5;
        private float horizontal;
        private float vertical;

        private void Start()
        {
            if (GetComponent<Rigidbody>())
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
        }

        protected void Move()
        {
            
        }
    }
}

