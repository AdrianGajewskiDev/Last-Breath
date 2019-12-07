using LB.InputControllers;
using LB.UI;
using UnityEngine;
using UnityEngine.UI;

namespace LB.Player.Inventory
{
    public class Flashlight : MonoBehaviour, IInventoryItem
    {
        public const float MAX_BATTERY_CHARGE = 3f;

        public float batteryCharge = 3;

        public AudioClip switchOnOffSound;
        public AudioClip lowBattery;

        AudioSource m_AudioSource;

        public static bool EmptyBattery;

        bool isOn = false;

        /// <summary>
        /// 0 - empty, 1-low, 2-half, 3-full
        /// </summary>
        public Sprite[] batteryStatuses;

        BatteryStatus batteryStatus;

        void DecreaseBattery()
        {
            if (!isOn)
                return;

            batteryCharge -= .0001f;
        }

        enum BatteryStatus
        {
            full,
            half,
            low,
            empty
        }

        private void Awake()
        {
            m_AudioSource = GetComponentInParent<AudioSource>();
        }

        public void LoadBattery()
        {
            EmptyBattery = false;
            batteryCharge = MAX_BATTERY_CHARGE;
        }

        void SetBatteryStatus()
        {
            if (batteryCharge >= 2.5)
                batteryStatus = BatteryStatus.full;
            else if (batteryCharge < 2.5 && batteryCharge >= 1)
                batteryStatus = BatteryStatus.half;
            else if (batteryCharge < 1 && batteryCharge >= .5f)
                batteryStatus = BatteryStatus.low;
            else if (batteryCharge <= 0)
                batteryStatus = BatteryStatus.empty;
        }


        void ShowBatteryStatus()
        {
            switch (batteryStatus)
            {
                case BatteryStatus.full:
                    UIManager.Singleton.BatteryStatus.sprite = batteryStatuses[3];
                    break;
                case BatteryStatus.half:
                    UIManager.Singleton.BatteryStatus.sprite = batteryStatuses[2];
                    break;
                case BatteryStatus.low:
                    UIManager.Singleton.BatteryStatus.sprite = batteryStatuses[1];
                    break;
                case BatteryStatus.empty:
                    UIManager.Singleton.BatteryStatus.sprite = batteryStatuses[0];
                    break;
                default:
                    break;
            }
        }

        void DisableFlashlight()
        {
            isOn = false;
            this.GetComponent<Light>().enabled = false;
        }

        public void ExecuteAction()
        {
            this.GetComponent<Light>().enabled = true;
        }

        private void Update()
        {

            if (InputController.UseItem && isOn == false && batteryStatus != BatteryStatus.empty)
            {
                isOn = true;
                m_AudioSource.PlayOneShot(switchOnOffSound);
                ExecuteAction();
            }
            else if(InputController.UseItem && isOn == true)
            {
                isOn = false;
                m_AudioSource.PlayOneShot(switchOnOffSound);
                DisableFlashlight();
            }

            if (batteryStatus == BatteryStatus.low)
                m_AudioSource.PlayOneShot(lowBattery);

            if (batteryCharge <= 0)
            {
                EmptyBattery = true;
                DisableFlashlight();
            }


            SetBatteryStatus();
            ShowBatteryStatus();
            DecreaseBattery();

        }
    }

}


