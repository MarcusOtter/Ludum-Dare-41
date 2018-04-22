using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenuCheck : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Main menu");
        }
    }
}
