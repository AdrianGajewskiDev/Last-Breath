using UnityEngine;
using System.IO;

namespace LB.GameMechanics
{
    public class SaveSystem 
    {
        #region Options

        public static void SaveOptions_Controll(float mouseSensitivityX,
        float mouseSensitivityY,
        float controllerSensitivityX,
        float controllerSensitivityY,
        string inputType)
        {
            string optionsPath = Application.streamingAssetsPath + "/ControllOptions.json";

            ControllsOptionsSaveModel options = new ControllsOptionsSaveModel
            {
                MouseSensitivityX = mouseSensitivityX,
                MouseSensitivityY = mouseSensitivityY,
                ControllerSensitivityX = controllerSensitivityX,
                ControllerSensitivityY = controllerSensitivityY,
                InputType = inputType
            };

            var jsonData = JsonUtility.ToJson(options);

            File.WriteAllText(optionsPath, jsonData);
        }

        public static void SaveOptions_Graphic(float lightIntensity, float shadowStrength, int qualityLevel, int[] currentResolution)
        {
            string optionsPath = Application.streamingAssetsPath + "/GraphicOptions.json";

            GraphicOptionsSaveModel options = new GraphicOptionsSaveModel
            {
                currentlightIntensity = lightIntensity,
                currentQualityLevel = qualityLevel,
                currentResolution = currentResolution,
                currentShadowStrength = shadowStrength
            };

            var jsonData = JsonUtility.ToJson(options);

            File.WriteAllText(optionsPath, jsonData);
        }

        public static void SaveOptions_Sound(float VolumeSlider, float PitchSlider, float WeaponVolumeSlider, bool BypassEffects)
        {


            string optionsPath = Application.streamingAssetsPath + "/SoundsOptions.json";

            var options = new VolumeOptionsSaveModel
            {
                BypassEffects = BypassEffects,
                PitchSlider = PitchSlider,
                WeaponVolumeSlider = WeaponVolumeSlider,
                VolumeSlider = VolumeSlider
            };

            var jsonData = JsonUtility.ToJson(options);

            File.WriteAllText(optionsPath, jsonData);
        }

        public static T LoadOptions<T>(OptionsType optionsType) where T : struct
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
                        json = File.ReadAllText(Application.streamingAssetsPath + "/GraphicOptions.json");
                    }
                    break;

                case OptionsType.Sounds:
                    {
                        json = File.ReadAllText(Application.streamingAssetsPath + "/SoundsOptions.json");

                    }
                    break;
            }

            var result = JsonUtility.FromJson<T>(json);


            return (T)result;
        }
        #endregion

        #region Player 

        public static void SavePlayerStats(int health, int level, float exp)
        {
            var path = Application.streamingAssetsPath + "/PlayerStats.json";
            var stats = new PlayerStatsSaveModel 
            {
             CurrentHealth = health,
             CurrentPlayerExperience = exp,
             CurrentPlayerLevel = level
            };

            var json = JsonUtility.ToJson(stats);

            File.WriteAllText(path, json);

        }

        public static PlayerStatsSaveModel LoadPlayerStats()
        {
            var json = File.ReadAllText(Application.streamingAssetsPath + "/PlayerStats.json");

            var playerStats = JsonUtility.FromJson<PlayerStatsSaveModel>(json);

            return playerStats;

        }

        #endregion
    }
}
