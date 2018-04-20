using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSplashScreen : MonoBehaviour
{
    private void ChangeScene()
    {
        // Changes to the next scene in the hierarchy
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
