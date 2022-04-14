using UnityEngine;

namespace StudyGame
{
    public sealed class UnitController : Controller
    {
        public Vector3 spawnPlayer;

        public override void StartController()
        {
            spawnPlayer = new Vector3(0f, 2f, 0f);
            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player/Player"), spawnPlayer, Quaternion.identity);
        }
    }
}

