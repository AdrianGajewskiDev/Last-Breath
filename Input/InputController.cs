using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class InputController : MonoBehaviour
{
    public static bool LeftMouse;
    public static bool RightMouse;

    public static bool PrimaryWeapon;
    public static bool SecondaryWeapon;

    public static bool Reload;
    public static bool ShowStats;
    public static bool PickUpItem;

    void Update()
    {
        LeftMouse = Input.GetMouseButtonDown(0) || Input.GetMouseButton(0);
        RightMouse = Input.GetMouseButton(1);

        PrimaryWeapon = Input.GetKeyDown(KeyCode.Alpha1);
        SecondaryWeapon = Input.GetKeyDown(KeyCode.Alpha2);

        Reload = Input.GetKeyDown(KeyCode.R);
        ShowStats = Input.GetKeyDown(KeyCode.Tab);
        PickUpItem = Input.GetKeyDown(KeyCode.F);
    }
}
