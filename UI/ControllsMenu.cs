using LB.GameMechanics;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

namespace LB.UI
{

    public class ControllsMenu : MonoBehaviour
    {
        public static InputType inputType;

        public Slider MSXSlider;
        public Slider MSYSlider;
        public Slider ControllerSXSlider;
        public Slider ControllerSYSlider;

        FirstPersonController playerController;


        private void Awake()
        {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

            if (playerController == null)
                Debug.Log("Player not found!!");

            var options = SaveSystem.LoadOptions<ControllsOptionsSaveModel>(OptionsType.Controll);

            MSXSlider.value = options.MouseSensitivityX;
            MSYSlider.value = options.MouseSensitivityY;
            ControllerSXSlider.value = options.ControllerSensitivityX;
            ControllerSYSlider.value = options.ControllerSensitivityY;
            inputType = (InputType)Enum.Parse(typeof(InputType), options.InputType);
        }

        public void SaveOptions()
        {
            SaveSystem.SaveOptions_Controll(MSXSlider.value, MSYSlider.value, ControllerSXSlider.value, ControllerSYSlider.value, inputType.ToString());
        }

        private void Update()
        {
            playerController.MouseLook.XSensitivity = MSXSlider.value;
            playerController.MouseLook.YSensitivity = MSYSlider.value;
            playerController.MouseLook.Xbox_SensitivityX= ControllerSXSlider.value;
            playerController.MouseLook.Xbox_SensitivityY = ControllerSYSlider.value;
        }
    }

}
