using UnityEngine;

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
            currentLevel.ZombiesToSpawnNumber += 2;
            IncreaseLevel();
            ZombiesManager.Singleton.SpawnZombies(currentLevel.ZombiesToSpawnNumber);
        }
    }

    public void IncreaseLevel()
    {
        currentLevel.LevelNumber += 1;
    }
}
