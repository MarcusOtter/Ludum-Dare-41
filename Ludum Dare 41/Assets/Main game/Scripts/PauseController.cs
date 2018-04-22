using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    internal static bool Paused { get; private set; }

    [SerializeField] private GameObject _pauseMenu;

    private void Start()
    {
        SetPause(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause(!Paused);
        }
    }

    public void SetPause(bool paused)
    {
        Paused = paused;
        _pauseMenu.SetActive(paused);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
