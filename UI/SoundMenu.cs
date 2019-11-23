using UnityEngine;
using UnityEngine.UI;

public class SoundMenu : MonoBehaviour
{
    public AudioSource mainAudioSource;

    public Slider volumeSlider;
    public Slider pitchSlider;
    public Slider WeaponVolumeSlider;
    public Toggle bypassEffects;

    void SetVolume()
    {
        mainAudioSource.volume = volumeSlider.value;
    }

    void SetPitch()
    {
        mainAudioSource.pitch = pitchSlider.value;
    }

    void SetWeaponVolume()
    {
        PlayerInventoryManager.Singleton.CurrentWeapon.GetComponent<AudioSource>().volume = WeaponVolumeSlider.value;
    }

    void SetBypassEffects()
    {
        mainAudioSource.bypassEffects = bypassEffects.isOn;
    }

    private void Update()
    {

        SetBypassEffects();
        SetVolume();
        SetWeaponVolume();
        SetPitch();
    }
}
