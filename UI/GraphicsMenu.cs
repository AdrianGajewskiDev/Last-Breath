using LB.GameMechanics;
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

        int currentQuality;
        int[] currentResulution = new int[2];

        public void SaveOptions()
        {
            SaveSystem.SaveOptions_Graphic(lightSlider.value, lightShadowStrengthSlider.value, currentQuality, currentResulution);
        }

        private void Awake()
        {
            var options = SaveSystem.LoadOptions<OptionsUtility_Graphic>(OptionsType.Graphic);


            lightSlider.value = options.currentlightIntensity;
            lightShadowStrengthSlider.value = options.currentShadowStrength;
            QualitySettings.SetQualityLevel(options.currentQualityLevel);
            Screen.SetResolution(options.currentResolution[0], options.currentResolution[1], fullScreen.isOn);
        }

        #region Quality Levels
        public void SetQuality_Low()
        {
            QualitySettings.SetQualityLevel(1);
            currentQuality = 1;
        }

        public void SetQuality_Medium()
        {
            QualitySettings.SetQualityLevel(2);
            currentQuality = 2;
        }

        public void SetQuality_High()
        {
            QualitySettings.SetQualityLevel(3);
            currentQuality = 3;
        }
        public void SetQuality_Ultra()
        {
            QualitySettings.SetQualityLevel(5, true);
            currentQuality = 5;

        }

        #endregion

        #region Resolution Levels
        public void SetResolution_1280_720()
        {
            Screen.SetResolution(1280, 720, fullScreen.isOn);
            currentResulution[0] = 1280;
            currentResulution[1] = 720;
        }
        public void SetResolution_1440_900()
        {
            Screen.SetResolution(1440, 900, fullScreen.isOn);
            currentResulution[0] = 1440;
            currentResulution[1] = 900;
        }

        public void SetResolution_1366_768()
        {
            Screen.SetResolution(1366, 768, fullScreen.isOn);
            currentResulution[0] = 1366;
            currentResulution[1] = 768;
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
