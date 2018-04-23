using System.Collections;
using UnityEngine;

public class AmbientAudioController : MonoBehaviour
{
    internal static AmbientAudioController Instance { get; private set; }

    internal static int RopeCount { get; private set; }

    [Header("General droplet settings")]
    [SerializeField] private AudioSource _dropletAudioSource;

    [Header("Droplet volume")]
    [SerializeField] private float _dropletMaxVolume;
    [SerializeField] private float _dropletMinVolume;

    [Header("Droplet pitch")]
    [SerializeField] private float _dropletMaxPitch;
    [SerializeField] private float _dropletMinPitch;

    [Header("Droplet delay")]
    [SerializeField] private float _dropletMaxDelay;
    [SerializeField] private float _dropletMinDelay;

    private AudioClip _dropletClip;
    private bool _playDroplets = true;

    private int _ropesThisLife;

    internal void AddRope()
    {
        RopeCount++;
        _ropesThisLife++;
    }

    internal void ResetRopesThisLife()
    {
        _ropesThisLife = 0;
    }

    internal void RemoveRopesThisLife()
    {
        RopeCount -= _ropesThisLife;
        _ropesThisLife = 0;
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _dropletClip = _dropletAudioSource.clip;
        StartCoroutine(PlayDroplets());
    }

    private IEnumerator PlayDroplets()
    {
        while (_playDroplets)
        {
            float delay = Random.Range(_dropletMinDelay, _dropletMaxDelay);
            yield return new WaitForSeconds(delay);

            float volume = Random.Range(_dropletMinVolume, _dropletMaxVolume);
            float pitch = Random.Range(_dropletMinPitch, _dropletMaxPitch);

            _dropletAudioSource.pitch = pitch;
            _dropletAudioSource.PlayOneShot(_dropletClip, volume);
        }
    }
}
