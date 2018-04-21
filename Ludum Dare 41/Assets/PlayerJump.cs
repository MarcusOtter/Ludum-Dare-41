using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerCollisionInfo))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpHeight;

    private Rigidbody2D _rigidbody;
    private PlayerCollisionInfo _playerCollisionInfo;

    private void OnEnable()
    {
        MouseInput.LeftClick += Jump;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollisionInfo = GetComponent<PlayerCollisionInfo>();
    }

    private void Jump()
    {
        if (!_playerCollisionInfo.OnGround)
        {
            return;
        }

        _playerCollisionInfo.OnGround = false;
        _rigidbody.AddForce(new Vector2(0, _jumpHeight));
    }

    private void OnDisable()
    {
        MouseInput.LeftClick -= Jump;
    }
}
