using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ZombiesManager : MonoBehaviour
{
    public List<GameObject> Zombies = new List<GameObject>();
    public List<GameObject> ZombiesPrefabs = new List<GameObject>();

    public Transform[] spawnPoints;

    public static ZombiesManager Singleton;

    [HideInInspector] public bool gameOver;

    void Start()
    {
        Singleton = this;
    }

    private void Awake()
    {
        Zombies = GameObject.FindGameObjectsWithTag("Zombie").ToList();
    }

    private void Update()
    {
        Zombies = GameObject.FindGameObjectsWithTag("Zombie").ToList();
    }

    public void DestroyAllZombies()
    {
        foreach (var zombie in Zombies)
        {
            Destroy(zombie);
        }
    }

    public void SpawnZombies(int amount)
    {
        for (int i = 0; i <= amount; i++)
        {
            var randomZombie = Random.Range(0, ZombiesPrefabs.Count);
            var randomSpawnPoint = Random.Range(0, spawnPoints.Length );

            Instantiate(ZombiesPrefabs[randomZombie] ,spawnPoints[randomSpawnPoint].position, spawnPoints[randomSpawnPoint].rotation);
        }
    }
}
