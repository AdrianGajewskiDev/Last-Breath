using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static bool LeftMouse;
    public static bool RightMouse;

    void Update()
    {
        LeftMouse = Input.GetMouseButtonDown(0) || Input.GetMouseButton(0);
        RightMouse = Input.GetMouseButton(1);
    }
}
