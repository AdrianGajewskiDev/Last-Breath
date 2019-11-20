public class Level 
{
    public int LevelNumber = 1;
    public int ZombiesToSpawnNumber;
    public int ZombiesLeft => ZombiesManager.Singleton.Zombies.Count;

    public bool LevelFinished => ZombiesLeft <= 0;

    public void IncreaseLevel()
    {
        LevelNumber += 1;
        ZombiesToSpawnNumber += 2;
    }
}
