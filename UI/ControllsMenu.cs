using UnityEngine;

namespace LB.UI
{

    public class ControllsMenu : MonoBehaviour
    {
        public static InputType inputType;

        private void Update()
        {
            Debug.Log(inputType);
        }
    }

}
