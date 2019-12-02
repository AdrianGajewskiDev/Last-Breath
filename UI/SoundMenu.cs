using LB.GameMechanics;
using LB.Player;
using LB.Player.Inventory;
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

        private void Awake()
        {
            var options = SaveSystem.LoadOptions<OptionsUtility_Sounds>(OptionsType.Sounds);

            volumeSlider.value = options.VolumeSlider;
            pitchSlider.value = options.PitchSlider;
            WeaponVolumeSlider.value = options.WeaponVolumeSlider;
            bypassEffects.isOn = options.BypassEffects;
        }

        public void SaveOptions()
        {
            SaveSystem.SaveOptions_Sound(volumeSlider.value, pitchSlider.value, WeaponVolumeSlider.value, bypassEffects.isOn);
        }

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

