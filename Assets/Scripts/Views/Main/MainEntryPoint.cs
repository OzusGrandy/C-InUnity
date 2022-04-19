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
    }
}


