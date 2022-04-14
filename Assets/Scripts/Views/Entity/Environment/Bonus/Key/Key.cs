using UnityEngine;

namespace StudyGame
{
    public class Key : Bonus, IFlicker, IRotation
    {
        [SerializeField] private Material _material;
        private float rotationSpeed;

        public delegate void GetKey();
        public event GetKey InteractionWithPlayer;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            rotationSpeed = Random.Range(13f, 40f);
        }

        void OnTriggerEnter()
        {
            Interaction();
        }

        public void Flick()
        {
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b, Mathf.PingPong(Time.time, 1f));
        }

        public void Rotate()
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
        }

        protected override void Interaction()
        {
            InteractionWithPlayer.Invoke();
        }
    }
}

