using UnityEngine.UI;

namespace LB.UI
{
    public class ControllsButtonOnClick : PauseMenuButtonsOnHover
    {

        public Text inputTypeText;

        InputType inputType = InputType.keyboard;

        private void Awake()
        {
            inputTypeText.text = inputType.ToString();
        }

        public override void OnClick()
        {

            base.OnClick();

            if (inputType == InputType.keyboard)
            {
                inputTypeText.text = "Controller";
                inputType = InputType.controller;
            }
            else if (inputType == InputType.controller)
            {
                inputTypeText.text = "Keyboard";
                inputType = InputType.keyboard;
            }

            ControllsMenu.inputType = inputType;
        }
    }

}

