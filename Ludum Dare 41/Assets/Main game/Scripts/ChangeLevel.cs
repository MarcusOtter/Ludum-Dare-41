using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private LevelToLoad _levelToLoad;

    [SerializeField] private Animator _fadeAnimator;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _textToDeactivate;

    [Header("Player hit by rock")]
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private float _hurtVolume;

    [Header("Player fall down pit")]
    [SerializeField] private AudioClip _falldownSound;
    [SerializeField] private float _falldownVolume;
    [SerializeField] private float _falldownPitch;

    private AmbientAudioController _controller;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            if (name == "Death Floor")
            {
                AudioSource _playerFallingSource = GameObject.FindGameObjectWithTag("AudioController").transform
                    .Find("PlayerFalling").GetComponent<AudioSource>();

                _playerFallingSource.pitch = _falldownPitch;
                _playerFallingSource.volume = _falldownVolume;
                _playerFallingSource.Play();

                _controller = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AmbientAudioController>();
                _controller.RemoveRopesThisLife();
            }

            LoadLevel();
        }

        if (gameObject.CompareTag("Player") && other.CompareTag("Rock"))
        {
            _controller = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AmbientAudioController>();
            _controller.RemoveRopesThisLife();

            GetComponentInParent<AudioSource>().PlayOneShot(_hurtSound, _hurtVolume);
            LoadLevel();
        }
    }

    public void LoadLevel()
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

        _controller = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AmbientAudioController>();
        _controller.ResetRopesThisLife();
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
