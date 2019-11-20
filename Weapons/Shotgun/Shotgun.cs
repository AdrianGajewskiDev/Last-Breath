using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Shotgun : Weapon
{
    float timeToFireAllowed;

    public AudioClip reloadSoundAfterShot;

    private void Awake()
    {
        camera = Camera.main;
        AudioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        OnShot += HandleShooting;
    }

    private void HandleShooting(RaycastHit hit)
    {
        UseStandartOnHitBehaviour(ref hit);
    }

    IEnumerator ReloadAfterShot()
    {
        animator.SetBool("ReloadAfterShot", true);
        yield return new WaitForSeconds(.2f);
        AudioSource.PlayOneShot(reloadSoundAfterShot);
        animator.SetBool("ReloadAfterShot", false); 
    }
    public override void Shot()
    {
        base.Shot();
        StartCoroutine(ReloadAfterShot());
    }

    private void Update()
    {
        canFIre = CheckIfCanFire(ref timeToFireAllowed, rateOfFire, CurrentAmmoInClip);

        if (canFIre && isReloading == false)
        {
            HandleRecoil();
            vfx[1].Play();
            vfx[2].Play();
            Shot();
        }

        if (InputController.Reload || InputController.Xbox_X && MaxAmmo > 0 && CurrentAmmoInClip != ClipSize)
            StartCoroutine(Reload());

        animator.SetBool("IsShooting", canFIre);

        Aim();

        DestroyParticles();

        animator.SetBool("IsRunning", !FirstPersonController.IsWalking);
    }

    void DestroyParticles()
    {
        Destroy(GameObject.Find("Blood(Clone)"), .2f);
        Destroy(GameObject.Find("BulletImpact(Clone)"), .2f);
    }

    public override IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("IsReloading", true);
        AudioSource.Play();
        AudioSource.loop = true;
        yield return new WaitForSeconds(2f);
        AudioSource.loop = false;
        AudioSource.Stop();
        HandleReload(ref CurrentAmmoInClip, ref ClipSize, ref MaxAmmo);
        animator.SetBool("IsReloading", false);
        isReloading = false;
    }

    public override void Aim()
    {
        animator.SetBool("IsAiming", InputController.RightMouse || InputController.Xbox_LeftBumber);
    }
}
