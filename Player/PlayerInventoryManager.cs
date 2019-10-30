using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    private Weapon m_CurrentWeapon;
    public Weapon CurrentWeapon { get => m_CurrentWeapon; }

    public List<Weapon> Weapons = new List<Weapon>();

    [SerializeField] private Transform hand;

    private void SetCurrentWeapon()
    { 
        m_CurrentWeapon = hand.GetComponentInChildren<Weapon>();

        if (m_CurrentWeapon != null && !Weapons.Contains(m_CurrentWeapon))
            Weapons.Add(m_CurrentWeapon);
    }

    private void Update()
    {
        SetCurrentWeapon();
    }
}
