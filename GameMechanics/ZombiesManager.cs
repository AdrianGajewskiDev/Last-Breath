using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZombiesManager : MonoBehaviour
{
    public List<GameObject> Zombies = new List<GameObject>();

    public static ZombiesManager Singleton;

    void Start()
    {
        Singleton = this;
    }

    private void LateUpdate()
    {
        Zombies = GameObject.FindGameObjectsWithTag("Zombie").ToList();
    }

    public void DisableZombies()
    {
        foreach (var zombie in Zombies)
        {
            zombie.SetActive(false);
        }
    }
}
