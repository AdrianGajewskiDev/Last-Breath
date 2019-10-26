using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandGun : Weapon
{
    float timeToFireAllowed;
    
    private void Awake()
    {
        camera = Camera.main;
        AudioSource = GetComponent<AudioSource>();

        OnShot += HandleShooting;
    }

    private void HandleShooting(RaycastHit hit)
    {
        var zombie = hit.transform.GetComponent<ZombieHealth>();


        if(zombie != null)
        {
            Debug.Log(zombie);

            zombie.OnHit += (animator) =>
            {
                animator.SetBool("GetHit", true);
            };

            zombie.GiveDamage(Damage);
        }
    }

    void Update()
    {
        canFIre = InputController.LeftMouse;

        if (canFIre && Time.time >= timeToFireAllowed)
        {
            timeToFireAllowed = Time.time + 1 / rateOfFire;
            Shot();        
        }
    }
}
