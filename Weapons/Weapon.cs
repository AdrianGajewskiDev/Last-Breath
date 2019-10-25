using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] int Damage;
    [SerializeField] int ClipSize;
    [SerializeField] int CurrentAmmoInClip;

    [SerializeField] float rateOfFire;
    [SerializeField] float range;

    protected Camera camera;

    bool canFIre;

    public event System.Action<RaycastHit> OnShot;

    public virtual void Shot() 
    {
        RaycastHit hit;

        var direction = camera.transform.forward;


        if (Physics.Raycast(gameObject.transform.position, direction, out hit, range))
        {
            OnShot?.Invoke(hit);
        }
    }
}
