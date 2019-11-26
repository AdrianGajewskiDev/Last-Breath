using UnityEngine;


namespace LB.InputControllers
{
    public class InputController : MonoBehaviour
    {
        public static bool LeftMouse;
        public static bool RightMouse;

        public static bool PrimaryWeapon;
        public static bool SecondaryWeapon;

        public static bool Reload;
        public static bool ShowStats;
        public static bool ShowPauseMenu;
        public static bool PickUpItem;

        #region Xbox_One_Input
        public static float Xbox_Vertical_Right_Thumbstick;
        public static float Xbox_Horizontal_Right_Thumbstick;

        public static bool Xbox_Run;
        public static bool Xbox_X;

        public static bool Xbox_LeftBumber;
        public static bool Xbox_RightBumber;
        #endregion

        void Update()
        {
            LeftMouse = Input.GetMouseButtonDown(0) || Input.GetMouseButton(0);
            RightMouse = Input.GetMouseButton(1);

            PrimaryWeapon = Input.GetKeyDown(KeyCode.Alpha1);
            SecondaryWeapon = Input.GetKeyDown(KeyCode.Alpha2);

            Reload = Input.GetKeyDown(KeyCode.R);
            ShowStats = Input.GetKeyDown(KeyCode.Tab);
            ShowPauseMenu = Input.GetKeyDown(KeyCode.Escape);
            PickUpItem = Input.GetKeyDown(KeyCode.F);

            //Xbox one
            Xbox_Vertical_Right_Thumbstick = Input.GetAxis("Vertical_Right_Thumbstick");
            Xbox_Horizontal_Right_Thumbstick = Input.GetAxis("Horizontal_Right_Thumbstick");

            Xbox_Run = Input.GetButton("Xbox_Run");
            Xbox_X = Input.GetButtonDown("Xbox_Reload");

            Xbox_RightBumber = Input.GetAxis("Xbox_Right_Bumber") > .5f;
            Xbox_LeftBumber = Input.GetAxis("Xbox_Left_Bumber") > .5f;

        }
    }

}
