using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public Weapon m_CurrentWeapon;

    public static PlayerInventoryManager Singleton;

    public Weapon CurrentWeapon { get => m_CurrentWeapon; }

    public List<Weapon> Weapons = new List<Weapon>();

    [SerializeField] private List<GameObject> hands;

    int currentWeaponIndex = 0;

    public void AddWeapon(GameObject prefab)
    {
        if(hands.Count < 2 && Weapons.Count < 2)
        {
            hands.Add(prefab);
            Weapons.Add(prefab.GetComponentInChildren<Weapon>());
        }
        else
        {
            hands.RemoveAt(1);
            Weapons.RemoveAt(1);

            hands.Add(prefab);
            Weapons.Add(prefab.GetComponentInChildren<Weapon>());
        }
    }

    private void SetCurrentWeapon(int index)
    {
        if(Weapons.Any())
        {
            hands[index].gameObject.SetActive(true);
            m_CurrentWeapon = hands[index].GetComponentInChildren<Weapon>();
        }
    }

    bool HasSecondaryWeapon() => Weapons.Count > 1;

    private void ChangeWeapon()
    {
        if (InputController.PrimaryWeapon)
        {
            currentWeaponIndex = 0;
            hands[currentWeaponIndex].gameObject.SetActive(true);
            hands[1].gameObject.SetActive(false);
        }

        if (InputController.SecondaryWeapon)
        {
            currentWeaponIndex = 1;
            hands[currentWeaponIndex].SetActive(true);
            hands[0].gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        Singleton = this;

        hands = GameObject.FindGameObjectsWithTag("Hand").ToList();

        foreach (GameObject hand in hands)
        {
            Weapons.Add(hand.transform.GetComponentInChildren<Weapon>());
        }

        if(Weapons.Any())
        {
            foreach (GameObject hand in hands)
            {
                hand.gameObject.SetActive(false);
            }
        }

        
    }

    private void Update()
    {
        if(HasSecondaryWeapon())
            ChangeWeapon();

        SetCurrentWeapon(currentWeaponIndex);
    }
}
