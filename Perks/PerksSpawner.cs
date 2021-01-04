using LB.InputControllers;
using LB.Player;
using LB.UI;
using UnityEngine;

namespace LB.Perks
{
    public class PerksSpawner : MonoBehaviour
    {
        [SerializeField] Perk[] Perks;

        Transform parent;
        GameObject currentPerkToSpawn;
        GameObject currentPerkHologram;
        bool inSpawningMode = false;
        float distanceToGround = 0f;
        Vector3 position;

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
                        Destroy(currentPerkHologram);
                        perk.Enable();
                        currentPerkToSpawn.SetActive(true);
                        currentPerkToSpawn.transform.parent = null;
                        currentPerkToSpawn = null;
                        inSpawningMode = false;
                    }
                    else
                        UIManager.Singleton.ShowMessage("Not enough money!!, Press [E] to cancel", 2f);
                }
            }

            if (!inSpawningMode && Perks.Length > 0) 
            {
                foreach (var perk in Perks)
                {
                    if(Input.GetKeyDown((KeyCode)perk.PerkKey))
                    {
                        inSpawningMode = true;
                        currentPerkHologram = perk.HologramPrefab;
                        currentPerkToSpawn = perk.OriginalPrefab;
                        SpawnPerk(currentPerkToSpawn, false, false);
                        SpawnPerk(currentPerkHologram, true, true);
                    }
                }
            }


            if(inSpawningMode && InputController.CancelAction)
            {
                inSpawningMode = false;
                currentPerkToSpawn.transform.parent = null;
                currentPerkHologram.transform.parent = null;
                Destroy(currentPerkToSpawn);
                Destroy(currentPerkHologram);
            }
        }

        void SpawnPerk(GameObject perk, bool active, bool isHologram)
        {
            position = new Vector3(parent.position.x, parent.position.y, parent.position.z);
            position += parent.forward * 4f;

            var obj = Instantiate(perk, position, parent.rotation);
            obj.transform.parent = parent;
            obj.SetActive(active);

            if (!isHologram)
                currentPerkToSpawn = obj;
            else
                currentPerkHologram = obj;
        }

        private void FixedUpdate()
        {

            if (inSpawningMode)
            {
                Ray ray = new Ray(currentPerkToSpawn.transform.position, -Vector3.up);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 5f))
                {
                    distanceToGround = hit.distance;
                }

                position.y = currentPerkToSpawn.transform.position.y - distanceToGround;

                //just to keep object little above the ground
                position.y += 0.05f;

                currentPerkToSpawn.transform.position = new Vector3(currentPerkToSpawn.transform.position.x, position.y, currentPerkToSpawn.transform.position.z);
                currentPerkHologram.transform.position = new Vector3(currentPerkToSpawn.transform.position.x, position.y, currentPerkToSpawn.transform.position.z);
            }
         
        }
    }
}



