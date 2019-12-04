using UnityEngine;

namespace LB.GameMechanics
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;

        public GameMode GameMode;

        private void Awake()
        {
            Singleton = this;
        }
    }
}
