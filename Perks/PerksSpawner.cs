using LB.InputControllers;
using LB.UI;
using UnityEngine;

namespace LB.Player
{
    public class PerksSpawner : MonoBehaviour
    {
        [SerializeField] GameObject minigunPerkPrefab;

        Transform parent;
        GameObject currentPerkToSpawn;
        bool inSpawningMode = false;

        private void Start()
        {
            parent = transform;
        }


        private void Update()
        {
            if(InputController.ConfirmAction)
            {
                if (inSpawningMode && currentPerkToSpawn != null)
                {
                    IPerk perk = currentPerkToSpawn.GetComponent<IPerk>();
                    if (perk.Cost <= PlayerStats.Singleton.PlayerMoney)
                    {
                        perk.Enable();
                        currentPerkToSpawn.transform.parent = null;
                        currentPerkToSpawn = null;
                        inSpawningMode = false;

                    }
                    else
                        UIManager.Singleton.ShowMessage("Not enough money!!, Press [E] to cancel", 2f);
                }
            }


            if (InputController.Perk_Minigun && !inSpawningMode)
            {
                inSpawningMode = true;
                SpawnPerk(minigunPerkPrefab);
            }

            if(inSpawningMode && InputController.CancelAction)
            {
                inSpawningMode = false;
                currentPerkToSpawn.transform.parent = null;
                Destroy(currentPerkToSpawn);
            }
        }

        void SpawnPerk(GameObject perk)
        {
            Vector3 position = new Vector3(parent.position.x, .5f, parent.position.z);
            position += parent.forward * 4f;

            var obj = Instantiate(perk, position, parent.rotation);
            obj.transform.parent = parent;
            obj.SetActive(true);
            currentPerkToSpawn = obj;
        }
    }
}



