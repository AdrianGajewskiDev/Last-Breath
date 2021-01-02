using LB.Health;
using System.Collections.Generic;
using UnityEngine;

namespace LB.AI
{
    [RequireComponent(typeof(AudioSource))]
    public class AIMinigun : AI, IPerk
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
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private ParticleSystem muzzleVFX;

        LayerMask layerMask;

        bool Active = false;
        float deltaTime = 5f;

        public int Cost { get => cost; set => cost = value; }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Active == false)
                return;

            allTargets = ScanForTargets<ZombieAI>(muzzle, layerMask, Radius, Angle);

            if(allTargets.Length > 0)
                currentTarget = CalculateCurrentTarget();

            if(currentTarget != null && !currentTarget.GetComponent<IHealth>().IsDead())
            {
                RotateToTarget(muzzle.parent, currentTarget, 60f);
                Shoot();
            }
        }

        void Shoot()
        {
            if(deltaTime >= rateOfFire)
            {
                audioSource.PlayOneShot(shootSound);
                muzzleVFX.Play();
                currentTarget.GetComponent<IHealth>().GiveDamage(2);
                deltaTime = 0;
            }

            deltaTime += Time.deltaTime;
        }

        public Transform CalculateCurrentTarget()
        {
            Dictionary<float, Transform> distances = new Dictionary<float, Transform>();

            foreach (var target in allTargets)
            {
                var distance = Vector3.Distance(transform.position, target.position);
                if (!distances.ContainsKey(distance))
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
    }
}


