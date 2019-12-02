using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LB.GameMechanics
{
    public class ZombiesManager : MonoBehaviour
    {
        public List<GameObject> ZombiesPrefabs = new List<GameObject>();

        public Transform[] spawnPoints;

        public static ZombiesManager Singleton;

        [HideInInspector] public bool gameOver;

        void Start()
        {
            Singleton = this;
        }


        public void DestroyAllZombies()
        {
            var zombies = GameObject.FindGameObjectsWithTag("Zombie");

            foreach (var zombie in zombies)
            {
                Destroy(zombie);
            }
        }

        public void SpawnZombies(int amount)
        {
            for (int i = 0; i <= amount; i++)
            {
                var randomZombie = Random.Range(0, ZombiesPrefabs.Count);
                var randomSpawnPoint = Random.Range(0, spawnPoints.Length);

                Instantiate(ZombiesPrefabs[randomZombie], spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
            }
        }
    }

}

