using System.Collections;
using UnityEngine;

namespace StudyGame
{
    public class MainEntryPoint: MonoBehaviour
    {
        private MainController mainController;

        void Start()
        {
            mainController = new MainController();
            mainController.StartController();
        }

        void Update()
        {
            mainController.Updates();
        }

        public void StartSomeCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
        public void StopSomeCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }


    }
}


