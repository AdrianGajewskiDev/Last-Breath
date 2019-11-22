using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButtonsOnHover : MonoBehaviour
{

    public void OnEnter()
    {

        switch (gameObject.name)
        {
            case "Resume":
                {
                    UIManager.Singleton.PauseMenuText.text = "Resume game";
                }
                break;
            case "Main Menu":
                {

                    UIManager.Singleton.PauseMenuText.text = "Go back to main menu";
                }
                break;
            case "Options":
                {

                    UIManager.Singleton.PauseMenuText.text = "Show options";
                }
                break;
        }

    }

    public void OnExit()
    {
        UIManager.Singleton.PauseMenuText.text = string.Empty;
    }
}
