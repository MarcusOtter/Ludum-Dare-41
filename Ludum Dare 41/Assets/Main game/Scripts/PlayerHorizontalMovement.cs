using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerHorizontalMovement : MonoBehaviour
{
    internal bool PlayerMoving;

    [Tooltip("How far away does the cursor need to be for the player to move? (in units)")]
    [SerializeField] private float _movementDeadzone = 2f;
    [SerializeField] private float _movementSpeed;

    private MouseInput _mouseInput;
    private Rigidbody2D _rigidbody;
    private PlayerCollisionInfo _playerCollision;

    private void Awake()
    {
        _mouseInput = Camera.main.GetComponent<MouseInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollision = GetComponent<PlayerCollisionInfo>();
    }

    private void Update()
    {
        if (MouseGreaterThanDeadzone() && !_playerCollision.OnWall)
        {
            PlayerMoving = true;
            MovePlayerTowardsMouse();
        }
        else
        {
            PlayerMoving = false;
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
        }
    }

    private bool MouseGreaterThanDeadzone()
    {
        return Mathf.Abs(transform.position.x - _mouseInput.MouseWorldPosition.x) > _movementDeadzone;
    }

    private void MovePlayerTowardsMouse()
    {
        float direction = Mathf.Sign(transform.position.x - _mouseInput.MouseWorldPosition.x);
        float xVelocity = _movementSpeed * -direction * Time.deltaTime;
        _rigidbody.velocity = new Vector2(xVelocity, _rigidbody.velocity.y);
    }
}
