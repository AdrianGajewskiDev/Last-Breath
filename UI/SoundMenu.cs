using LB.Player;
using UnityEngine;
using UnityEngine.UI;

namespace LB.UI
{
    public class SoundMenu : MonoBehaviour
    {
        public AudioSource mainAudioSource;

        public Slider volumeSlider;
        public Slider pitchSlider;
        public Slider WeaponVolumeSlider;
        public Toggle bypassEffects;


        float currentVolume;
        float currentPitch;
        float currentWeaponVolume;

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

}

