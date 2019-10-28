using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class ZombieAI : AI
{
    [Header("Locomotion")]
    public float WalkSpeed;
    public float RunningSpeed;
    [HideInInspector]public float Speed;

    [Header("FOV")]
    public float Angle;
    public float Radius;

    Animator animator;

    public AudioSource playerWeaponSounds;
    public LayerMask layerMask;

    Rigidbody[] ragdolls;

    enum ZombieState
    {
        Idle,
        Walking,
        Running,
        Attacking,
        Patrolling
    }

    ZombieState state;

    Transform player;
    Transform localPlayer;
    


    private void Awake()
    {
        animator = GetComponent<Animator>();
        ragdolls = GetComponentsInChildren<Rigidbody>();
        localPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        DisableRagdoll();
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(this.gameObject.transform, Radius, Angle);
    }

    private void Update()
    {
        player = ScanForTarget<FirstPersonController>(this.gameObject.transform, layerMask, Radius, Angle);

        CheckForPotentialTarget(localPlayer);

        if (player != null)
            RotateToTarget(this.gameObject.transform, player.transform.Find("LookAtTarget"), 3f);

        SetState();
        Move();
    }

    void CheckForPotentialTarget(Transform target)
    {
        if (IsInLineOfSight(target, this.gameObject.transform, Angle, Radius, layerMask) || player != null)
            return;
        var distance = Vector3.Distance(gameObject.transform.position, target.position);

        if (distance <= Radius && playerWeaponSounds.isPlaying)
            player = target;
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
                    Speed = WalkSpeed;
                    state = ZombieState.Walking;
                }

                if (behaviour == 1)
                {
                    Speed = RunningSpeed;
                    state = ZombieState.Running;
                }

                PlayAnimation(state);
            }
        }
        else
        {
            Speed = 0;
            state = ZombieState.Idle;
            PlayAnimation(state);
        }
    }

    void PlayAnimation(ZombieState state)
    {
        switch (state)
        {
            case ZombieState.Idle:
                {
                    animator.SetBool("IsWalking", false);
                    animator.SetBool("IsRunning", false);
                }
                break;

            case ZombieState.Walking:
                {
                    animator.SetBool("IsWalking", true);
                }
                break;

            case ZombieState.Running:
                {
                    animator.SetBool("IsRunning", true);
                }
                break;

        }
    }

    void Move()
    {
        if (player == null)
            return;

        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    void DisableRagdoll()
    {
        foreach (var rbody in ragdolls)
        {
            rbody.isKinematic = true;
        }
    }
}
