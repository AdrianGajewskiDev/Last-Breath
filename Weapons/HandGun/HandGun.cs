using UnityEngine;

public class HandGun : Weapon
{

    private void Awake()
    {
        camera = Camera.main;
        OnShot += HandleShooting;
    }

    private void HandleShooting(RaycastHit obj)
    {
        throw new System.NotImplementedException();
    }

    void Update()
    {
        Shot();        
    }
}
