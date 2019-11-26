using UnityEngine;

namespace LB.UI
{
    public class PauseMenuOptions : MonoBehaviour
    {

        bool optionsMenuVisible = false;

        public void Resume()
        {
            UIManager.Singleton.PauseMenuSlideOut();
        }

        public void Options()
        {
            if (optionsMenuVisible == false)
            {
                optionsMenuVisible = true;
                UIManager.Singleton.OptionsMenuSlideIn();
            }

            else if (optionsMenuVisible == true)
            {
                optionsMenuVisible = false;
                UIManager.Singleton.OptionsMenuSlideOut();
            }
        }

        public void MainMenu()
        {

        }


    }

}

