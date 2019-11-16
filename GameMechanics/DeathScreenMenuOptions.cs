using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenMenuOptions : MonoBehaviour
{
    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
