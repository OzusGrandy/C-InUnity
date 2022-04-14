using System;
using UnityEngine;
using Random = UnityEngine.Random;
namespace StudyGame
{
    public class BadBonus : Bonus, IFly, IRotation
    {
        private float heightFly;
        private float rotationSpeed;
        

        private void Awake()
        {
            heightFly = Random.Range(1f, 5f);
            rotationSpeed = Random.Range(13f, 40f);
        }
        public void Fly()
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, heightFly), transform.position.z);
        }

        public void Rotate()
        {
            transform.Rotate(Vector3.up*rotationSpeed*Time.deltaTime, Space.World);
        }

        protected override void Interaction()
        {
            Debug.Log("No interaction");
        }
    }
}

