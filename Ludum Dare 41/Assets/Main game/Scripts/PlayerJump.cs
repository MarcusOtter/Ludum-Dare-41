using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerCollisionInfo))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpHeight;

    private Rigidbody2D _rigidbody;
    private PlayerCollisionInfo _playerCollisionInfo;

    private bool _canDoubleJump = true;

    private void OnEnable()
    {
        MouseInput.LeftClick += Jump;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollisionInfo = GetComponent<PlayerCollisionInfo>();
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
            return;
        }

        // Normal jump
        _playerCollisionInfo.OnGround = false;
        _rigidbody.AddForce(new Vector2(0, _jumpHeight));
        _canDoubleJump = true;
    }

    private void OnDisable()
    {
        MouseInput.LeftClick -= Jump;
    }
}
