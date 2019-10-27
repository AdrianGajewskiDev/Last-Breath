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

    public bool GetHit = false;

    enum ZombieState
    {
        Idle,
        Walking,
        Running,
        Attacking,
        Patrolling,
        GetHit
    }

    ZombieState state;

    Transform player;

    LayerMask layerMask;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(this.gameObject.transform, Radius, Angle);
    }

    private void Update()
    {
        player = ScanForTarget<FirstPersonController>(this.gameObject.transform, layerMask, Radius, Angle);

        if (player != null)
            RotateToTarget(this.gameObject.transform, player.transform.Find("LookAtTarget"), 3f);

        SetState();

        if(GetHit == false)
            Move();
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
}
