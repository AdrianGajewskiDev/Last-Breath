using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ZombieAIConfiguration")]
public class ZombieAIConfiguration : ScriptableObject
{
    public float WalkSpeed;
    public float RunningSpeed;
    public float PatrollingSpeed;
    public float distanceToAttack;
    public float zombieScoreAmountOnHit;
    public float zombieScoreAmountOnDie;
    public AudioClip AttackSFX;
    public AudioClip WalkAndPatrolSFX;
    public AudioClip RunSFX;
}
