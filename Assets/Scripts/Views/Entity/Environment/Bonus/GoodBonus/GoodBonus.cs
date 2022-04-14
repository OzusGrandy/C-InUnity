using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public class GoodBonus : Bonus, IFlicker, IFly
    {
        [SerializeField] private Material _material;
        private float heightFly = 1.5f;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
        }
        public void Flick()
        {
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, Mathf.PingPong(Time.time, 1f));
        }

        public void Fly()
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, heightFly) + 0.5f, transform.position.z);
        }

        protected override void Interaction()
        {
            Debug.Log("No interaction");
        }
    }
}
