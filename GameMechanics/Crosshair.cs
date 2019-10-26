using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Crosshair : MonoBehaviour
{
    [SerializeField] Texture2D crosshairTexture;

    [SerializeField] float width;
    [SerializeField] float height;

    private void Awake()
    {
    }

    private void OnGUI()
    {
        var x = Screen.width / 2;
        var y = Screen.height / 2;

        GUI.DrawTexture(new Rect(x,y, width,height),crosshairTexture);
    }

   
}
