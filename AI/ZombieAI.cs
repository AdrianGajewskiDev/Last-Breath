using LB.GameMechanics;
using LB.Health;
using LB.InputControllers;
using LB.Player;
using LB.Player.Inventory;
using LB.UI;
using LB.Weapons.Knife;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;


namespace LB.AI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AudioSource))]
    public class ZombieAI : AI
    {
        [Header("Locomotion")]
        public ZombieAIConfiguration configuration;

        [HideInInspector] public float Speed;
        public IList<Transform> _waypoints = new List<Transform>();

        [Header("FOV")]
        public float Angle;
        public float Radius;

        public bool SetSpecialAttack;
        AudioSource playerWeaponSounds;
        public LayerMask layerMask;

        Rigidbody[] ragdolls;

        [Header("Stats")]
        public int Damage;

        Animator animator;
        NavMeshAgent agent;
        AudioSource audioSource;

        enum ZombieState
        {
            Idle,
            Walking,
            Running,
            Attacking,
            Patrolling
        }

        [System.Serializable]
        class SpecialActions
        {
            public bool MakeZombieIdle;
            public bool MakeZombieBitting;

        }

        bool addScoreToPlayer = false;
        [SerializeField] SpecialActions ZombieSpecialActions;
        [SerializeField] ZombieState state;

        [HideInInspector] public Transform player = null;

        void SetWaypoints()
        {
            var waypoints = GameObject.FindGameObjectsWithTag("Waypoint");

            foreach (var w in waypoints)
            {
                _waypoints.Add(w.transform);
            }
        }

        private void Start()
        {

            if (configuration == null)
                Debug.LogError($"{nameof(configuration)} is null or not assigned!!!!");

            SetWaypoints();
            animator = GetComponent<Animator>();
            ragdolls = GetComponentsInChildren<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
            audioSource = GetComponent<AudioSource>();
            DisableRagdoll();

            GetComponent<ZombieHealth>().OnHit += (aa) =>
            {
                GameManager.Singleton.localPlayer.GetComponent<PlayerStats>().AddScore(configuration.zombieScoreAmountOnHit);
            };
            GetComponent<ZombieHealth>().OnDie += () =>
            {
                SetUpNavMeshAgent(false);
                var playerStats = GameManager.Singleton.localPlayer.GetComponent<PlayerStats>();
                playerStats.AddScore(configuration.zombieScoreAmountOnDie);
                playerStats.AddKilledZombies(1);
                playerStats.AddPlayerMoney(50);
                LevelManager.Singleton.ZombiesCount -= 1;
            };

            if (GameManager.Singleton.GameMode == GameMode.Survival)
                LevelManager.Singleton.ZombiesCount += 1;
        }

        //private void OnDrawGizmos()
        //{
        //    DrawGizmos(this.gameObject.transform, Radius, Angle);
        //}

        public void Update()
        {

            if (GameManager.Singleton.GameMode == GameMode.Story)
                player = ScanForTarget<FirstPersonController>(this.gameObject.transform, layerMask, Radius, Angle);

            else
                player = GameManager.Singleton.localPlayer.transform;

            SetState();
            CheckForPotentialTarget(GameManager.Singleton.localPlayer.transform);

            if (player != null)
            {
                RotateToTarget(this.gameObject.transform, player.transform.Find("LookAtTarget"), 3f);
                Move();
                SetUpNavMeshAgent(false);
                ZombieSpecialActions.MakeZombieBitting = false;

            }
            else
            {
                if (state != ZombieState.Idle && ZombieSpecialActions.MakeZombieIdle == false && ZombieSpecialActions.MakeZombieBitting == false)
                {
                    SetUpNavMeshAgent(true);

                    if (!agent.pathPending)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                            {
                                SetDestination(ref agent, _waypoints);
                            }
                        }
                    }
                }
                else
                {
                    SetUpNavMeshAgent(false);
                    state = ZombieState.Idle;
                    Speed = 0;
                    PlayAnimation(ZombieState.Idle);
                }
            }

            if (ZombieSpecialActions.MakeZombieBitting)
                animator.SetBool("Bitting", true);
            else
                animator.SetBool("Bitting",false);

            AttackPlayer();

            if (PlayerInventoryManager.Singleton.CurrentWeapon != null)
                playerWeaponSounds = PlayerInventoryManager.Singleton.CurrentWeapon.transform.GetComponent<AudioSource>();
        }


        public void SetTarget(Transform target)
        {
            if (IsInLineOfSight(target, this.gameObject.transform, Angle, Radius, layerMask) || player != null)
                return;

            player = target;
        }

        //Check if zombie don't see player but can hear him
        void CheckForPotentialTarget(Transform target)
        {
            if (IsInLineOfSight(target, this.gameObject.transform, Angle, Radius, layerMask) || player != null)
                return;
            var distance = Vector3.Distance(gameObject.transform.position, target.position);

            if (PlayerInventoryManager.Singleton.CurrentWeapon != null)
            {
                if (distance <= Radius && playerWeaponSounds.isPlaying)
                    player = target;
            }

        }

        public void SetUpNavMeshAgent(bool enabled)
        {
            agent.enabled = enabled;
            agent.speed = Speed;
        }

        void SetState()
        {

            if (player != null)
            {

                if (animator.GetBool("IsWalking") == true || animator.GetBool("IsRunning") == true)
                    return;
                else
                {


                    var behaviour = Random.Range(0, 2);

                    //Random zombie behavior so it's not the same every time
                    if (behaviour == 0)
                    {
                        Speed = configuration.WalkSpeed;
                        state = ZombieState.Walking;
                    }

                    if (behaviour == 1)
                    {
                        Speed = configuration.RunningSpeed;
                        state = ZombieState.Running;
                    }

                }
            }
            else
            {

                Speed = configuration.PatrollingSpeed;
                state = ZombieState.Patrolling;
            }


            PlaySoundEffects(state);
            PlayAnimation(state);
        }

        void PlayAnimation(ZombieState state)
        {
            switch (state)
            {
                case ZombieState.Idle:
                    {
                        animator.SetBool("IsWalking", false);
                        animator.SetBool("IsRunning", false);
                        animator.SetBool("IsPatrolling", false);

                    }
                    break;

                case ZombieState.Walking:
                    {
                        animator.SetBool("IsPatrolling", false);
                        animator.SetBool("IsWalking", true);
                    }
                    break;

                case ZombieState.Running:
                    {
                        animator.SetBool("IsPatrolling", false);
                        animator.SetBool("IsRunning", true);
                    }
                    break;
                case ZombieState.Patrolling:
                    {
                        animator.SetBool("IsRunning", false);
                        animator.SetBool("IsWalking", false);
                        animator.SetBool("IsPatrolling", true);
                    }
                    break;
            }
        }

        void Move()
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        void DisableRagdoll()
        {
            foreach (var rbody in ragdolls)
            {
                rbody.isKinematic = true;
            }
        }


        void AttackPlayer()
        {
            if (player == null)
                return;

            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

            if (distanceToPlayer <= configuration.distanceToAttack)
            {

                Speed = 0;
                animator.SetBool("Attack", true);
            }
            else
            {
                if (state == ZombieState.Walking)
                    Speed = configuration.WalkSpeed;
                else
                    Speed = configuration.RunningSpeed;

                animator.SetBool("Attack", false);
            }

        }

     

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponentInChildren<Knife>() != null)
            {
                if (InputController.StealthKill)
                {
                    GetComponent<ZombieHealth>().KilledByKnife = true;
                    other.GetComponentInChildren<Knife>().animator.SetBool("Stealth", true);
                    Timer.Singleton.Add(() =>
                    {
                        other.GetComponentInChildren<Knife>().animator.SetBool("Stealth", false);

                    }, .3f);
                    GetComponent<ZombieHealth>().GiveDamage(100);
                }
            }
        }



        public void GiveDamageToPlayer()
        {
            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

            if (distanceToPlayer <= configuration.distanceToAttack)
            {
                player.GetComponent<PlayerHealth>().OnHit += () =>
                {
                    StartCoroutine(UIManager.Singleton.SetBloodOverlay());
                    GameManager.Singleton.localPlayer.GetComponent<FirstPersonController>().AudioSource.clip = GameManager.Singleton.localPlayer.GetComponent<FirstPersonController>().hitSound;
                    GameManager.Singleton.localPlayer.GetComponent<FirstPersonController>().AudioSource.Play();
                };

                player.GetComponent<PlayerHealth>().GiveDamage(Damage);
            }

        }

        void PlaySoundEffects(ZombieState state)
        {
            switch (state)
            {
                case ZombieState.Idle:
                    break;
                case ZombieState.Walking:
                    {
                        audioSource.clip = configuration.WalkAndPatrolSFX;
                        audioSource.Play();
                    }
                    break;
                case ZombieState.Running:
                    {
                        audioSource.clip = configuration.RunSFX;
                        audioSource.Play();
                    }
                    break;
                case ZombieState.Attacking:
                    {
                        audioSource.clip = configuration.AttackSFX;
                        audioSource.Play();
                    }
                    break;
                case ZombieState.Patrolling:
                    {
                        audioSource.clip = configuration.WalkAndPatrolSFX;
                        audioSource.Play();
                    }
                    break;
                default:
                    break;
            }
        }
    }

}

