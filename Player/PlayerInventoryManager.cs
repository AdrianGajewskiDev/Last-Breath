using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    Weapon m_CurrentWeapon;
    public Weapon CurrentWeapon { get => m_CurrentWeapon; }

    public List<Weapon> Weapons = new List<Weapon>();

    [SerializeField] private Transform hand;

    int currentWeaponIndex = 0;

    private void SetCurrentWeapon(int index)
    {
        if(Weapons.Any())
        {
            Weapons[index].gameObject.SetActive(true);
            m_CurrentWeapon = Weapons[index];
        }
    }

    bool HasSecondaryWeapon() => Weapons.Count > 1;

    private void ChangeWeapon()
    {
        if (InputController.PrimaryWeapon)
        {
            currentWeaponIndex = 0;
            Weapons[currentWeaponIndex].gameObject.SetActive(true);
            Weapons[1].gameObject.SetActive(false);
        }

        if (InputController.SecondaryWeapon)
        {
            currentWeaponIndex = 1;
            Weapons[currentWeaponIndex].gameObject.SetActive(true);
            Weapons[0].gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        Weapons = hand.GetComponentsInChildren<Weapon>().ToList();    

        if(Weapons.Any())
        {
            foreach (Weapon weapon in Weapons)
            {
                weapon.gameObject.SetActive(false);
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
