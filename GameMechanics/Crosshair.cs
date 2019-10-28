using UnityEngine;
public class Crosshair : MonoBehaviour
{
    [SerializeField] Texture2D crosshairTexture;

    [SerializeField] float width = 0;
    [SerializeField] float height = 0;

   
    private void OnGUI()
    {
        var x = (Screen.width / 2) - (width / 2);
        var y = (Screen.height / 2) - (height / 2);

       GUI.DrawTexture(new Rect(x,y, width,height),crosshairTexture);
    }
}
