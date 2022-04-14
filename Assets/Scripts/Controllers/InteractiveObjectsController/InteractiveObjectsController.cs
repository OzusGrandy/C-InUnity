using System.Collections.Generic;
using UnityEngine;

namespace StudyGame
{
    public sealed class InteractiveObjectsController : Controller
    {
        private List<Bonus> interactiveObjects = new List<Bonus>();

        private GameObject key;
        private Key keyScript;
        private Vector3 keyPosition;

        public Vector3 KeyPosition { get { return key.transform.position; } }

        public delegate void GetKey();
        public event GetKey Get;

        public override void StartController()
        {
            keyPosition = new Vector3(0f, 1.5f, 5f);
            key = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Bonuses/Key"),keyPosition,Quaternion.identity);
            keyScript = key.GetComponent<Key>();
            keyScript.InteractionWithPlayer += HideKeyAfterPicking;
        }

        public void UpdateObjects()
        {
            for (int i = 0; i < interactiveObjects.Count; i++)
            {
                if (interactiveObjects[i].gameObject.activeSelf == false)
                {
                    continue;
                }

                if (interactiveObjects[i] is IFly fly)
                {
                    fly.Fly();
                }
                if (interactiveObjects[i] is IRotation rotation)
                {
                    rotation.Rotate();
                }
                if (interactiveObjects[i] is IFlicker flicker)
                {
                    flicker.Flick();
                }
            }
        }
        public void AddObject(GameObject addedObject)
        {

            interactiveObjects.Add(addedObject.GetComponent<Bonus>());
        }

        public void AddObjects(GameObject[] addedObjects)
        {
            AddKey();
            for (int i = 0;i < addedObjects.Length; i++)
            {
                AddObject(addedObjects[i]);
            }
        }

        private void AddKey()
        {
            interactiveObjects.Add(key.GetComponent<Bonus>());
        }

        public void ClearList()
        {
            interactiveObjects.Clear();
        }

        public void ReloadObjects()
        {
            ClearList();
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Bonus");
            if (objects != null)
                AddObjects(objects);
        }

        public void HideKeyAfterPicking()
        {
            key.SetActive(false);
            Get.Invoke();
        }

        public void HideOrShowKey(bool isShowing)
        {
            key.SetActive(isShowing);
        }
    }
}