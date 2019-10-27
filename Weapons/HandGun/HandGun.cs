using System.Threading.Tasks;
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


        if (zombie != null)
        {
            Instantiate(vfx[0], hit.point, Quaternion.identity);
            zombie.GiveDamage(Damage);
        }
    }

    void DestroyParticles()
    {
        Destroy(GameObject.Find("Blood(Clone)"),1);
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
