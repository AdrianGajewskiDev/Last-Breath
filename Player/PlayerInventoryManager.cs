using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    Weapon m_CurrentWeapon;

    public static PlayerInventoryManager Singleton;

    public Weapon CurrentWeapon { get => m_CurrentWeapon; }

    public List<Weapon> Weapons = new List<Weapon>();

    [SerializeField] private GameObject[] hands;

    int currentWeaponIndex = 0;

    private void SetCurrentWeapon(int index)
    {
        if(Weapons.Any())
        {
            Weapons[index].gameObject.transform.parent.gameObject.SetActive(true);
            m_CurrentWeapon = Weapons[index];
        }
    }

    bool HasSecondaryWeapon() => Weapons.Count > 1;

    private void ChangeWeapon()
    {
        if (InputController.PrimaryWeapon)
        {
            currentWeaponIndex = 0;
            Weapons[currentWeaponIndex].gameObject.transform.parent.gameObject.SetActive(true);
            Weapons[1].gameObject.transform.parent.gameObject.SetActive(false);
        }

        if (InputController.SecondaryWeapon)
        {
            currentWeaponIndex = 1;
            Weapons[currentWeaponIndex].gameObject.transform.parent.gameObject.SetActive(true);
            Weapons[0].gameObject.transform.parent.gameObject.SetActive(false); ;
        }
    }

    private void Awake()
    {
        Singleton = this;

        hands = GameObject.FindGameObjectsWithTag("Hand");

        foreach (GameObject hand in hands)
        {
            Weapons.Add(hand.transform.GetComponentInChildren<Weapon>());
        }

        if(Weapons.Any())
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.gameObject.transform.parent.gameObject.SetActive(false);
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
