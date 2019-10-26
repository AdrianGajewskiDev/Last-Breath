using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour
{
    [SerializeField] Texture2D crosshair;

    [SerializeField] float Width;
    [SerializeField] float Height;

    private void OnGUI()
    {
        var x = Screen.width / 2;
        var y = Screen.height / 2;

        GUI.DrawTexture(new Rect(x,y, Width, Height),crosshair);
    }
}
