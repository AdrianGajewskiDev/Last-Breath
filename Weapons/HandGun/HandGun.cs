using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]
public class HandGun : Weapon
{
    float timeToFireAllowed;
    
    private void Awake()
    {
        camera = Camera.main;
        AudioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        OnShot += HandleShooting;
    }

    private void HandleShooting(RaycastHit hit)
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
            if(hit.point != null)
                Instantiate(vfx[3], hit.point, Quaternion.identity);

        }
    }

    void DestroyParticles()
    {
        Destroy(GameObject.Find("Blood(Clone)"),1);
        Destroy(GameObject.Find("BulletImpact(Clone)"),1);
    }
    public override void Aim()
    {
        animator.SetBool("IsAiming", InputController.RightMouse);
    }

    void Update()
    {
        canFIre = CheckIfCanFire(ref timeToFireAllowed, rateOfFire, CurrentAmmoInClip);

        if (canFIre)
        {
            vfx[1].Play();
            vfx[2].Play();
            Shot();
        }

        animator.SetBool("IsShooting", canFIre);

        animator.SetBool("IsRunning", !FirstPersonController.IsWalking);
        Aim();
        DestroyParticles();

        if (InputController.Reload && MaxAmmo > 0)
            StartCoroutine(Reload());
    }

    public override IEnumerator Reload()
    {
        animator.SetBool("IsReloading", true);
        AudioSource.PlayOneShot(gunReloadSound);
        yield return new WaitForSeconds(2f);
        HandleReload(ref CurrentAmmoInClip, ref ClipSize, ref MaxAmmo);
        animator.SetBool("IsReloading", false);
    }
}
