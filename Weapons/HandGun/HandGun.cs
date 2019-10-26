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
