using UnityEngine;
using UnityEngine.UI;

namespace LB.UI
{
    public class GraphicsMenu : MonoBehaviour
    {
        public new Light light;

        public Slider lightSlider;
        public Slider lightShadowStrengthSlider;
        public Toggle fullScreen;

        #region Quality Levels
        public void SetQuality_Low()
        {
            QualitySettings.SetQualityLevel(1);
        }

        public void SetQuality_Medium()
        {
            QualitySettings.SetQualityLevel(2);
        }

        public void SetQuality_High()
        {
            QualitySettings.SetQualityLevel(3);
        }
        public void SetQuality_Ultra()
        {
            QualitySettings.SetQualityLevel(5, true);
        }

        #endregion

        #region Resolution Levels
        public void SetResolution_1280_720()
        {
            Screen.SetResolution(1280, 720, fullScreen.isOn);
        }
        public void SetResolution_1440_900()
        {
            Screen.SetResolution(1440, 900, fullScreen.isOn);
        }

        public void SetResolution_1366_768()
        {
            Screen.SetResolution(1366, 768, fullScreen.isOn);
        }
        #endregion

        #region Light Intensity
        void SetLightIntensity()
        {
            light.intensity = lightSlider.value;
        }

        void SetLightShadowStrength()
        {
            light.shadowStrength = lightShadowStrengthSlider.value;
        }

        void FullScreen()
        {
            Screen.fullScreen = fullScreen.isOn;
        }
        #endregion

        private void Update()
        {
            SetLightIntensity();
            SetLightShadowStrength();
            FullScreen();
        }
    }

}
