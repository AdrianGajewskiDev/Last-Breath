using UnityEngine;

namespace LB.Player
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats Singleton;

        private float m_Score;
        public float Score { get => m_Score; set => m_Score = value; }

        private int m_KilledZombies;
        public int KilledZombies { get => m_KilledZombies; set => m_KilledZombies = value; }


        private void Awake()
        {
            Singleton = this;
        }

        public void AddScore(float amount)
        {
            m_Score += amount;
        }

        public void AddKilledZombies(int amount)
        {
            m_KilledZombies += amount;
        }
    }

}
