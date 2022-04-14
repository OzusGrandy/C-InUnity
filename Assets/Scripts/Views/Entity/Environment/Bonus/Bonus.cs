using UnityEngine;

namespace StudyGame
{
    public abstract class Bonus : Environment, IInteract
    {
        public bool IsInteracted { get; } = true;

        protected Color _color;

        private void Start()
        {
            Action();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteracted || other.CompareTag("Player"))
            {
                return;
            }
            Interaction();
            Destroy(gameObject);
        }

        protected abstract void Interaction();

        public override void Action()
        {
            _color = Random.ColorHSV();

            if (TryGetComponent(out Renderer renderer))
            {
                renderer.sharedMaterial.color = _color;
            }
        }
    }

}

