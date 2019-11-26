using UnityEngine;

namespace LB.UI
{
    public class OptionsMenuPanelSwitcher : MonoBehaviour
    {
        public GameObject graphicPanel;
        public GameObject soundPanel;
        public GameObject controllPanel;

        public void ShowGraphicPanel()
        {
            graphicPanel.SetActive(true);
            soundPanel.SetActive(false);
            controllPanel.SetActive(false);
        }
        public void ShowSoundPanel()
        {
            graphicPanel.SetActive(false);
            soundPanel.SetActive(true);
            controllPanel.SetActive(false);
        }
        public void ShowControllPanel()
        {
            graphicPanel.SetActive(false);
            soundPanel.SetActive(false);
            controllPanel.SetActive(true);
        }
    }

}
