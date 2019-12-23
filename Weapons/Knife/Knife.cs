using LB.AI;
using LB.GameMechanics;
using LB.Health;
using LB.InputControllers;
using UnityEngine;

namespace LB.Weapons.Knife
{
    public class Knife : MonoBehaviour
    {
        public int Damage;


        public Animator animator;

        new Camera camera;

        bool isAttacking;

        public AudioClip hitSound;
        public AudioClip sliceSound;

        public GameObject blood;

        private void Awake()
        {
            camera = Camera.main;
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Attack();

            Destroy(GameObject.Find("BloodFromKnife(Clone)"), 0.3f);
        }


        void Attack()
        {
            RaycastHit hit;

            var direction = camera.transform.forward;

            if (isAttacking)
            {
                if (Physics.Raycast(camera.transform.position, direction, out hit, 2f))
                {
                    if (hit.transform.GetComponentInParent<ZombieHealth>() != null)
                    {
                        hit.transform.GetComponentInParent<ZombieHealth>().GiveDamage(Damage);
                        hit.transform.GetComponentInParent<ZombieHealth>().OnHit += (ai) =>
                        {
                            if (ai.player == null)
                                ai.SetTarget(GameManager.Singleton.localPlayer.transform);
                        };
                        GetComponentInParent<AudioSource>().PlayOneShot(hitSound);
                        Instantiate(blood, hit.transform.position, hit.transform.rotation);

                    }
                }
                else
                    GetComponentInParent<AudioSource>().PlayOneShot(sliceSound);

            }

            animator.SetBool("Attack", InputController.LeftMouse);

        }

        /// <summary>
        /// Functions to call from animation Events
        /// </summary>
        public void SetIsAttackingToTrue() => isAttacking = true;
        public void SetIsAttackingToFalse() => isAttacking = false;


      
    }

}
