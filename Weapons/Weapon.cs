using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage;
    public int ClipSize;
    public int CurrentAmmoInClip;
    public int MaxAmmo;

    public float rateOfFire;
    public float range;
    public float weaponKick;

    protected Camera camera;
    protected bool canFIre;
    protected bool isReloading;

    [HideInInspector]public AudioSource AudioSource;
    [SerializeField] AudioClip gunShotSound;
    public AudioClip gunReloadSound;


    /// <summary>
    /// vfx[0]: Blood 
    /// vfx[1]: Muzzle flash
    /// vfx[2]: Bullet
    /// vfx[3]: Bullet impact if no zombie was hitted
    /// </summary>
    public  ParticleSystem[] vfx;

    public event System.Action<RaycastHit> OnShot;

    [HideInInspector]public Animator animator;

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

    public void HandleReload(ref int currentAmmoInClip, ref int clipSize, ref int maxAmmo)
    {

        if (MaxAmmo >= 1)
        {
            if (currentAmmoInClip > 0)
            {
                var currentAmmo = currentAmmoInClip;
                var ammoToAdd = clipSize - currentAmmo;
                maxAmmo -= ammoToAdd;
                currentAmmoInClip += ammoToAdd;
            }

            if (currentAmmoInClip == 0)
            {
                var ammoToAdd = clipSize;
                maxAmmo -= ammoToAdd;
                currentAmmoInClip += ammoToAdd;
            }
        }
    }

    public void UseStandartOnHitBehaviour(ref RaycastHit hit)
    {
        var zombie = hit.transform.GetComponentInParent<ZombieHealth>();

        if (zombie != null)
        {
            if (hit.transform.CompareTag("ZombieHead"))
                zombie.GiveDamage(Damage * 2);
            else
                zombie.GiveDamage(Damage);

            if (zombie.transform.CompareTag("Zombie"))
                Instantiate(vfx[0], hit.point, Quaternion.identity);
        }
        else
        {
            if (hit.point != null)
                Instantiate(vfx[3], hit.point, Quaternion.identity);

        }
    }

    public virtual void Aim() { }

    public virtual IEnumerator Reload() { yield return null; }

    public virtual void HandleRecoil() { }
}
