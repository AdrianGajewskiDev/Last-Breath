using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (canFIre)
        {
            HandleRecoil();
            vfx[1].Play();
            vfx[2].Play();
            Shot();
        }

        if (InputController.Reload && MaxAmmo > 0)
            StartCoroutine(Reload());

        Aim();
        
    }

    public override IEnumerator Reload()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(2f);
        HandleReload(ref CurrentAmmoInClip, ref ClipSize, ref MaxAmmo);
        Debug.Log("Reloaded");
    }

    public override void HandleRecoil()
    {
        Debug.Log("Recoil Works");
    }

    public override void Aim()
    {
        animator.SetBool("IsAiming", InputController.RightMouse);
    }
}
