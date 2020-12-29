using LB.Health;
using UnityEngine;

namespace LB.AI
{
    [RequireComponent(typeof(AudioSource))]
    public class AIMinigun : AI
    {
        [SerializeField] private Transform muzzle;

        private Transform currentTarget;
        private AudioSource audioSource;

        [SerializeField] private float rateOfFire = 0.5f;

        [Header("FOV")]
        public float Angle;
        public float Radius;

        [Header("Effects")]
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private ParticleSystem muzzleVFX;

        LayerMask layerMask;

        float deltaTime = 5f;
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            currentTarget = ScanForTarget<ZombieAI>(muzzle, layerMask, Radius, Angle);

            if(currentTarget != null)
            {
                if(!currentTarget.GetComponent<IHealth>().IsDead())
                {
                    RotateToTarget(muzzle.parent, currentTarget, 60f);
                    Shoot();
                }
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

        private void OnDrawGizmos()
        {
            DrawGizmos(muzzle, Radius, Angle);
        }
    }
}


