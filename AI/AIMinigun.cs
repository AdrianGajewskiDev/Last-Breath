using LB.GameMechanics;
using LB.Health;
using LB.InputControllers;
using LB.Perks;
using LB.Player.Inventory;
using LB.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace LB.AI
{
    [RequireComponent(typeof(AudioSource))]
    public class AIMinigun : AI, IPerk, IControllable
    {
        [SerializeField] private Transform muzzle;

        private Transform[] allTargets;
        private Transform currentTarget;
        private AudioSource audioSource;
        private Camera mainCamera;

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
        float mouseSensitivity = 1f;

        public int Cost { get => cost; set => cost = value; }

        private void Start()
        {
            mainCamera = Camera.main;
            minigunControlCam.enabled = false;
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Active == false)
                return;

            if(controlledByPlayer == true)
            {
                if(InputController.CancelAction)
                {
                    controlledByPlayer = false;
                    SwitchCameras(false);
                    EnablePlayer();
                }
                else
                {
                    Control();
                    return;
                }
            }

            allTargets = ScanForTargets<ZombieAI>(muzzle, layerMask, Radius, Angle);

            if(allTargets.Length > 0)
                currentTarget = CalculateCurrentTarget();

            if(currentTarget != null && !currentTarget.GetComponent<IHealth>().IsDead())
            {
                RotateToTarget(muzzle, currentTarget, 60f);
                ShootAI();
            }
        }

        private void FixedUpdate()
        {
            if(controlledByPlayer)
            {
                if(InputController.LeftMouse)
                {
                    ShootPlayer();
                }
            }
        }

        void ShootAI()
        {
            if(Time.time >= deltaTime)
            {
                deltaTime = Time.time + 1 / rateOfFire;
                audioSource.PlayOneShot(shootSound);
                muzzleVFX.Play();
                currentTarget.GetComponent<IHealth>().GiveDamage(2);
            }
        }

        void ShootPlayer()
        {
            if (Time.time >= deltaTime)
            {
                deltaTime = Time.time + 1 / rateOfFire;
                audioSource.PlayOneShot(shootSound);
                muzzleVFX.Play();

                if (Physics.Raycast(muzzle.transform.position, muzzle.forward, out var hitInfo, 200f))
                {
                    if(hitInfo.transform.CompareTag("Zombie"))
                    {
                        var health = hitInfo.transform.GetComponent<IHealth>();

                        if(health == null)
                        {
                            health = hitInfo.transform.GetComponentInParent<IHealth>();
                        }

                        if(health != null)
                            health.GiveDamage(5);
                    }
                }
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

            muzzle.localRotation *= Quaternion.AngleAxis(-yRot, Vector3.right)
                * Quaternion.AngleAxis(xRot, Vector3.up) * Quaternion.AngleAxis(0, Vector3.forward);
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
                    SwitchCameras(true);
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

        void SwitchCameras(bool mainCamEnabled)
        {
            if (mainCamEnabled)
            {
                minigunControlCam.enabled = true;
                mainCamera.enabled = false;
            }
            else 
            {
                mainCamera.enabled = true;
                minigunControlCam.enabled = false;
            }

        }

        void DisablePlayer()
        {
            GameManager.Singleton.localPlayer.GetComponent<FirstPersonController>().enabled = false;
            mainCamera.gameObject.SetActive(false);
            UIManager.Singleton.HideCrosshair();
        }


        void EnablePlayer() 
        {
            GameManager.Singleton.localPlayer.GetComponent<FirstPersonController>().enabled = true;
            mainCamera.gameObject.SetActive(true);
            UIManager.Singleton.ShowCrosshair();

        }

    }
}


