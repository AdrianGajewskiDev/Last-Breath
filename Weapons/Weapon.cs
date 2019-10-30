using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;
    public int ClipSize;
    public int CurrentAmmoInClip;
    public int MaxAmmo;

    public float rateOfFire;
    public float range;

    protected Camera camera;
    protected bool canFIre;

    [HideInInspector]public AudioSource AudioSource;
    [SerializeField] AudioClip gunShotSound;
    public  ParticleSystem[] vfx;

    public event System.Action<RaycastHit> OnShot;

    [HideInInspector]public Animator animator;

    public Vector3 AimPosition;
    public Vector3 CurrentWeaponPosition;

    public virtual void Shot() 
    {
        RaycastHit hit;

        var direction = camera.transform.forward;

        AudioSource.PlayOneShot(gunShotSound);
        CurrentAmmoInClip -= 1;
        if (Physics.Raycast(camera.transform.position, direction, out hit, range))
        {
            OnShot?.Invoke(hit);
        }
    }

    public virtual bool CheckIfCanFire(ref float nextFireAllowed, float rateOfFire, float currentAmmoInClip)
    {
        canFIre = InputController.LeftMouse;

        if (canFIre && Time.time >= nextFireAllowed && currentAmmoInClip > 0)
        {
            nextFireAllowed = Time.time + 1 / rateOfFire;
            return true;
        }
        else
            return false;
    }
    public virtual void Aim() { }
}
