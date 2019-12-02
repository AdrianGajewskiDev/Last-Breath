
namespace LB.GameMechanics
{
    public class Level
    {
        public int LevelNumber = 1;
        public int ZombiesToSpawnNumber;

        public bool LevelFinished => LevelManager.Singleton.ZombiesCount == 0;

        public void IncreaseLevel()
        {
            LevelNumber += 1;
            ZombiesToSpawnNumber += 2;
        }
    }

}
