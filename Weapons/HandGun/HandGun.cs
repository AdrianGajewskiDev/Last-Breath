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
        CurrentWeaponPosition = this.gameObject.transform.parent.position;
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

            Instantiate(vfx[0], hit.point, Quaternion.identity);

        }
    }

    void DestroyParticles()
    {
        Destroy(GameObject.Find("Blood(Clone)"),1);
    }

    public override void Aim()
    {
        animator.SetBool("IsAiming", InputController.RightMouse);
    }

    void Update()
    {
        canFIre = InputController.LeftMouse;

        if (canFIre && Time.time >= timeToFireAllowed)
        {
            vfx[1].Play();
            vfx[2].Play();
            timeToFireAllowed = Time.time + 1 / rateOfFire;
            Shot();     
        }

        animator.SetBool("IsShooting", canFIre);
        animator.SetBool("IsRunning", !FirstPersonController.IsWalking);
        Aim();
        DestroyParticles();
    }
}
