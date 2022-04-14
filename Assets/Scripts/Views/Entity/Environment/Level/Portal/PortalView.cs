using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StudyGame
{
    public class PortalView : Environment
    {
        private bool activated;

        public delegate void Activate();
        public event Activate GetActivate;

        private PortalState state;

        public PortalState State { get { return state; } set { state = value; } }

        public bool Activated
        {
            get { return activated; }
        }

        public override void Action()
        {
            activated = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (state != PortalState.Inactive)
                if (other.gameObject.name == "Player(Clone)")
                {
                    activated = true;
                    GetActivate.Invoke();
                }
        }
    }
}
