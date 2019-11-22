using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsMenu : MonoBehaviour
{
    public Light light;

    public Slider lightSlider;
    public Slider lightShadowStrengthSlider;
    public Toggle fullScreen;

    public void SetResolution_1280_720()
    {
        Screen.SetResolution(1280, 720, fullScreen.isOn);
    }
    public void SetResolution_1440_900()
    {
        Screen.SetResolution(1440, 900, fullScreen.isOn);
    }

    public void SetResolution_1366_768()
    {
        Screen.SetResolution(1366, 768, fullScreen.isOn);
    }

    private void FixedUpdate()
    {
        Debug.Log(Screen.currentResolution);
    }

    void SetLightIntensity()
    {
        light.intensity = lightSlider.value;
    }

    void SetLightShadowStrength()
    {
        light.shadowStrength = lightShadowStrengthSlider.value;
    }

    void FullScreen()
    {
        Screen.fullScreen = fullScreen.isOn;
    }

    private void Update()
    {
        SetLightIntensity();
        SetLightShadowStrength();
        FullScreen();
    }
}
