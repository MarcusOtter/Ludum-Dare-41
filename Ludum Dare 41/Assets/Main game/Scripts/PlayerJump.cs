using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerCollisionInfo))]
public class PlayerJump : MonoBehaviour
{
    [Header("General jump settings")]
    [SerializeField] private float _jumpHeight;

    [Space(10)]
    [Header("Jump sound settings")]
    [SerializeField] private AudioClip _jump01;
    [SerializeField] private AudioClip _jump02;

    [SerializeField] private float _maxJumpPitch;
    [SerializeField] private float _minJumpPitch;

    private Rigidbody2D _rigidbody;
    private PlayerCollisionInfo _playerCollisionInfo;

    private bool _canDoubleJump = true;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        MouseInput.LeftClick += Jump;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollisionInfo = GetComponent<PlayerCollisionInfo>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_playerCollisionInfo.OnGround)
        {
            _canDoubleJump = true;
        }
    }

    private void Jump()
    {
        // TODO : Add check for paused

        if (!_playerCollisionInfo.OnGround)
        {
            if (!_canDoubleJump)
            {
                return;
            }

            // Double jump
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, _jumpHeight));
            _canDoubleJump = false;
            _playerCollisionInfo.OnGround = false;
            PlayJumpSound(_jump01);
            return;
        }

        // Normal jump
        _playerCollisionInfo.OnGround = false;
        _rigidbody.AddForce(new Vector2(0, _jumpHeight));
        _canDoubleJump = true;
        PlayJumpSound(_jump02);
    }

    private void PlayJumpSound(AudioClip clip)
    {
        float pitch = Random.Range(_minJumpPitch, _maxJumpPitch);
        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(clip);
    }

    private void OnDisable()
    {
        MouseInput.LeftClick -= Jump;
    }
}
