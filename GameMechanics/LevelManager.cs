using LB.UI;
using UnityEngine;

namespace LB.GameMechanics
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Singleton;

        public int CurrentLevel;
        public int ZombiesToSpawnNumber = 3;

        Level currentLevel = new Level();

        void Start()
        {
            Singleton = this;
            currentLevel.ZombiesToSpawnNumber = ZombiesToSpawnNumber;
            CurrentLevel = currentLevel.LevelNumber;
            ZombiesManager.Singleton.SpawnZombies(currentLevel.ZombiesToSpawnNumber);
        }

        private void Update()
        {
            CurrentLevel = currentLevel.LevelNumber;

            if (currentLevel.LevelFinished == true)
            {
                StartCoroutine(UIManager.Singleton.PlayLevelFinishedAnimation());
                currentLevel.ZombiesToSpawnNumber += 2;
                currentLevel.IncreaseLevel();
                ZombiesManager.Singleton.SpawnZombies(currentLevel.ZombiesToSpawnNumber);
            }
        }
    }

}

