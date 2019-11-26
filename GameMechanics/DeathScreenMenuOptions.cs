using UnityEngine;
using UnityEngine.SceneManagement;

namespace LB.GameMechanics 
{

    public class DeathScreenMenuOptions : MonoBehaviour
    {
        public void ReloadCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
