using LB.InputControllers;
using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


namespace LB.Weapons.HandGun
{
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
            UseStandartOnHitBehaviour(ref hit);
        }

        void DestroyParticles()
        {
            Destroy(GameObject.Find("Blood(Clone)"), 1);
            Destroy(GameObject.Find("BulletImpact(Clone)"), 1);
        }
        public override void Aim()
        {
            animator.SetBool("IsAiming", InputController.RightMouse || InputController.Xbox_LeftBumber);
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

            if (InputController.Reload || InputController.Xbox_X && MaxAmmo > 0)
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

}
