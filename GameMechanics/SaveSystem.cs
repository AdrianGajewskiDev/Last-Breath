using UnityEngine;
using System.IO;

namespace LB.GameMechanics
{
    public class SaveSystem : MonoBehaviour
    {

        public static void SaveOptions_Controll(float mouseSensitivityX,
        float mouseSensitivityY,
        float controllerSensitivityX,
        float controllerSensitivityY)
        {
            string optionsPath = Application.streamingAssetsPath + "/ControllOptions.json";

            OptionsUtility_Controlls options = new OptionsUtility_Controlls
            {
                MouseSensitivityX = mouseSensitivityX,
                MouseSensitivityY = mouseSensitivityY,
                ControllerSensitivityX = controllerSensitivityX,
                ControllerSensitivityY = controllerSensitivityY
            };

            var jsonData = JsonUtility.ToJson(options);

            File.WriteAllText(optionsPath, jsonData);
        }

        public static OptionsUtility_Controlls LoadOptions_Controlls()
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/ControllOptions.json");
            var result = JsonUtility.FromJson<OptionsUtility_Controlls>(json);
            return result;
        }


        public static void SaveOptions_Graphic(float lightIntensity, float shadowStrength, int qualityLevel, int[] currentResolution)
        {
            string optionsPath = Application.streamingAssetsPath + "/GraphicOptions.json";

            OptionsUtility_Graphic options = new OptionsUtility_Graphic
            {
                currentlightIntensity = lightIntensity,
                currentQualityLevel = qualityLevel,
                currentResolution = currentResolution,
                currentShadowStrength = shadowStrength
            };

            var jsonData = JsonUtility.ToJson(options);

            File.WriteAllText(optionsPath, jsonData);
        }

        public static OptionsUtility_Graphic LoadOptions_Graphic()
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/GraphicOptions.json");
            var result = JsonUtility.FromJson<OptionsUtility_Graphic>(json);
            return result;
        }
    }
}
