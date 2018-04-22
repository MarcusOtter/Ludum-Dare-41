using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private LevelToLoad _levelToLoad;

    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _textToDeactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadLevel();
        }
    }

    private void LoadLevel()
    {
        int sceneIndex = 0;

        switch (_levelToLoad)
        {
            case LevelToLoad.None:
                Debug.LogWarning(name + " does not change a level.");
                break;
            default:
                Debug.LogWarning(name + " does not change a level.");
                break;
            case LevelToLoad.NextLevel:
                sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                break;
            case LevelToLoad.RestartLevel:
                sceneIndex = SceneManager.GetActiveScene().buildIndex;
                break;
        }

        StartCoroutine(ChangeLevelAfterFade(sceneIndex));
    }

    private IEnumerator ChangeLevelAfterFade(int sceneIndex)
    {
        _fadeAnimator.SetBool("FadeIn", true);
        _textToDeactivate.SetActive(false);
        yield return new WaitUntil(() => _canvasGroup.alpha >= 1);
        SceneManager.LoadScene(sceneIndex);
    }
}

[System.Serializable]
public enum LevelToLoad
{
    None = 0,
    RestartLevel = 1,
    NextLevel = 2
}
