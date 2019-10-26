using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class ZombieAI : AI
{

    public float WalkSpeed;
    public float RunningSpeed;
    float Speed;

    Rigidbody rBody;
    Animator animator;

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

    LayerMask layerMask;


    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(this.gameObject.transform, 5,40);
    }

    private void Update()
    {
       player = ScanForTarget<FirstPersonController>(this.gameObject.transform,layerMask, 5, 40);

        if (player != null)
            RotateToTarget(this.gameObject.transform, player, 3f);

        SetState();
    }

    void SetState()
    {
        if (player != null)
        {
            Speed = WalkSpeed;

            var behaviour = Random.Range(0, 2);

            Debug.Log(behaviour);

            if (behaviour == 0)
                state = ZombieState.Walking;

            if (behaviour == 1)
                state = ZombieState.Running;    

            PlayAnimation(state);
        }
        else
        {
            Speed = 0;
            state = ZombieState.Idle;
            PlayAnimation(state);
        }

        Debug.Log(player);
        Debug.Log(Speed);
    }

    void PlayAnimation(ZombieState state)
    {
        switch(state)
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

    
}
