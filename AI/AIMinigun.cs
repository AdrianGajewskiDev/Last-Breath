using LB.GameMechanics;
using LB.Health;
using LB.InputControllers;
using LB.Perks;
using LB.UI;
using System.Collections.Generic;
using UnityEngine;

namespace LB.AI
{
    [RequireComponent(typeof(AudioSource))]
    public class AIMinigun : AI, IPerk, IControllable
    {
        [SerializeField] private Transform muzzle;

        private Transform[] allTargets;
        private Transform currentTarget;
        private AudioSource audioSource;

        [SerializeField] private float rateOfFire = 0.5f;

        [Header("FOV")]
        public float Angle;
        public float Radius;

        [SerializeField] int cost = 100;

        [Header("Effects")]
        [SerializeField] private Camera minigunControlCam;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private ParticleSystem muzzleVFX;

        LayerMask layerMask;

        bool Active = false;
        bool controlledByPlayer = false;
        float deltaTime;
        float mouseSensitivity = 2f;

        public int Cost { get => cost; set => cost = value; }

        private void Start()
        {
            minigunControlCam.enabled = false;
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Active == false)
                return;

            if(controlledByPlayer == true)
            {
                Control();
                return;
            }

            allTargets = ScanForTargets<ZombieAI>(muzzle, layerMask, Radius, Angle);

            if(allTargets.Length > 0)
                currentTarget = CalculateCurrentTarget();

            if(currentTarget != null && !currentTarget.GetComponent<IHealth>().IsDead())
            {
                RotateToTarget(muzzle, currentTarget, 60f);
                Shoot();
            }
        }

        void Shoot()
        {
            if(Time.time >= deltaTime)
            {
                deltaTime = Time.time + 1 / rateOfFire;
                audioSource.PlayOneShot(shootSound);
                muzzleVFX.Play();
                currentTarget.GetComponent<IHealth>().GiveDamage(2);
            }
        }

        public Transform CalculateCurrentTarget()
        {
            Dictionary<float, Transform> distances = new Dictionary<float, Transform>();

            foreach (var target in allTargets)
            {
                var distance = Vector3.Distance(transform.position, target.position);
                if (!distances.ContainsKey(distance) && distance > 2f)
                    distances.Add(distance, target);
                else
                    continue;
            }

            var sortedDistances = distances.GetSortedAscending();

            return distances[sortedDistances[0]];
        }

        private void OnDrawGizmos()
        {
            DrawGizmos(muzzle, Radius, Angle);
        }

        public void Enable()
        {
            Active = true;
        }

        public void Control()
        {
            float xRot = Input.GetAxis("Mouse X") * mouseSensitivity;
            float yRot = Input.GetAxis("Mouse Y") * mouseSensitivity;

            muzzle.localRotation *= Quaternion.AngleAxis(yRot, Vector3.right)
                * Quaternion.AngleAxis(xRot, Vector3.up);
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                UIManager.Singleton.ShowMessage("Control Minigun [F]");
                if(InputController.ExecuteAction)
                {
                    UIManager.Singleton.ShowMessage("To exit press [E]", 2f);
                    controlledByPlayer = true;
                    SwitchCameras(true, false);
                    DisablePlayer();
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                controlledByPlayer = false;
                UIManager.Singleton.ClearMessage();
            }
        }

        void SwitchCameras(bool miniGunCamEnabled, bool mainCamEnabled)
        {
            minigunControlCam.enabled = miniGunCamEnabled;
            Camera.main.enabled = mainCamEnabled;
        }

        void DisablePlayer()
        {
            GameManager.Singleton.localPlayer.SetActive(false);
        }
    }
}


