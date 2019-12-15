using UnityEngine;

namespace LB.UI
{
    public class PauseMenuButtonsOnHover : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip hoverSound;
        public AudioClip clickSound;

        public virtual void OnClick()
        {
            PlayClip(clickSound);
        }

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
                case "Save":
                    {
                        UIManager.Singleton.PauseMenuText.text = "Save a game progress";
                    }
                    break;
            }
            PlayClip(hoverSound);

        }

        void PlayClip(AudioClip clip)
        {
            audioSource.PlayOneShot(clip);
        }

        public void OnExit()
        {
            UIManager.Singleton.PauseMenuText.text = string.Empty;
        }
    }

}
