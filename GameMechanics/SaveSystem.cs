using UnityEngine;
using System.IO;

namespace LB.GameMechanics
{
    public class SaveSystem 
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

        public static T LoadOptions<T>(OptionsType optionsType)
        {
            string json = string.Empty;

            switch (optionsType)
            {
                case OptionsType.Controll:
                    {
                        json = File.ReadAllText(Application.streamingAssetsPath + "/ControllOptions.json");
                    }
                    break;

                case OptionsType.Graphic:
                    {
                        json = Application.streamingAssetsPath + "/GraphicOptions.json";
                    }
                    break;

                case OptionsType.Sounds:
                    {
                        json = Application.streamingAssetsPath + "/SoundsOptions.json";

                    }
                    break;
            }

            var result = JsonUtility.FromJson<T>(json);


            return (T)result;
        }
    }
}
