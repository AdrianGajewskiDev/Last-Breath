using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(AudioSource))]
public class SMG : Weapon
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
        UseStandartOnHitBehaviour(ref hit);
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

        if (InputController.Reload && MaxAmmo > 0 && CurrentAmmoInClip != ClipSize)
            StartCoroutine(Reload());

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
        AudioSource.PlayOneShot(gunReloadSound);
        yield return new WaitForSeconds(3f);
        HandleReload(ref CurrentAmmoInClip, ref ClipSize, ref MaxAmmo);
        animator.SetBool("IsReloading", false);
        isReloading = false;
    }

    public override void HandleRecoil()
    {
        Debug.Log("Recoil works !!!");
    }

    public override void Aim()
    {
        animator.SetBool("IsAiming", InputController.RightMouse);
    }
}
