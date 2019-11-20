using UnityEngine;
public class Crosshair : MonoBehaviour
{
    public static Crosshair Singleton;

    [SerializeField] Texture2D crosshairTexture;

    [SerializeField] float width = 0;
    [SerializeField] float height = 0;

    public bool HideCrosshair;

    private void Awake()
    {
        Singleton = this;
    }

    private void OnGUI()
    {
        if(!InputController.RightMouse && !HideCrosshair)
        {
            var x = (Screen.width / 2) - (width / 2);
            var y = (Screen.height / 2) - (height / 2);

            GUI.DrawTexture(new Rect(x, y, width, height), crosshairTexture);
        }
        
    }
}
