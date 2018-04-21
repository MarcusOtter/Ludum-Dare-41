using UnityEngine;

public class PlayerCollisionInfo : MonoBehaviour
{
    internal bool OnGround;
    internal bool OnWall;

    [Header("Wall raycast settings")]
    [SerializeField] private float _raycastYOffset = 0.4f;
    [SerializeField] private float _wallRayLength = 0.8f;

    [Space(10)]
    [Header("Ground raycast settings")]
    [SerializeField] private float _raycastXOffset = 0.4f;
    [SerializeField] private float _groundRayLength = 1.1f;

    [Space(10)]
    [Header("Other settings")]
    [SerializeField] private bool _drawRays;

    #region RaycastHits

    private RaycastHit2D _leftGroundRaycastHit;
    private RaycastHit2D _rightGroundRaycastHit;
    private RaycastHit2D _upperLeftRaycastHit;
    private RaycastHit2D _bottomLeftRaycastHit;
    private RaycastHit2D _upperRightRaycastHit;
    private RaycastHit2D _bottomRightRaycastHit;

    #endregion

    #region Raycast points

    // Left ground
    private Vector2 _leftGroundRaycastOrigin;
    private Vector2 _leftGroundRaycastEnd;

    // Right ground
    private Vector2 _rightGroundRaycastOrigin;
    private Vector2 _rightGroundRaycastEnd;

    // Upper left
    private Vector2 _upperLeftRaycastOrigin;
    private Vector2 _upperLeftRaycastEnd;

    // Bottom left
    private Vector2 _bottomLeftRaycastOrigin;
    private Vector2 _bottomLeftRaycastEnd;

    // Upper right
    private Vector2 _upperRightRaycastOrigin;
    private Vector2 _upperRightRaycastEnd;

    // Bottom right
    private Vector2 _bottomRightRaycastOrigin;
    private Vector2 _bottomRightRaycastEnd;

    #endregion

    private void Update()
    {
        CalculateRaycastPoints();
        ShootRaycasts();
        CheckIfOnWall();
        CheckIfGrounded();

        if (_drawRays)
        {
            DrawRaycasts();
        }
    }

    private void DrawRaycasts()
    {
        // Ground
        Debug.DrawLine(_leftGroundRaycastOrigin, _leftGroundRaycastEnd);
        Debug.DrawLine(_rightGroundRaycastOrigin, _rightGroundRaycastEnd);

        // Walls
        Debug.DrawLine(_upperLeftRaycastOrigin, _upperLeftRaycastEnd);
        Debug.DrawLine(_bottomLeftRaycastOrigin, _bottomLeftRaycastEnd);
        Debug.DrawLine(_upperRightRaycastOrigin, _upperRightRaycastEnd);
        Debug.DrawLine(_bottomRightRaycastOrigin, _bottomRightRaycastEnd);
    }

    private void ShootRaycasts()
    {
        _leftGroundRaycastHit = Physics2D.Linecast(_leftGroundRaycastOrigin, _leftGroundRaycastEnd);
        _rightGroundRaycastHit = Physics2D.Linecast(_rightGroundRaycastOrigin, _rightGroundRaycastEnd);
        _upperLeftRaycastHit = Physics2D.Linecast(_upperLeftRaycastOrigin, _upperLeftRaycastEnd);
        _bottomLeftRaycastHit = Physics2D.Linecast(_bottomLeftRaycastOrigin, _bottomLeftRaycastEnd);
        _upperRightRaycastHit = Physics2D.Linecast(_upperRightRaycastOrigin, _upperRightRaycastEnd);
        _bottomRightRaycastHit = Physics2D.Linecast(_bottomRightRaycastOrigin, _bottomRightRaycastEnd);
    }

    private void CalculateRaycastPoints()
    {
        float currentXPos = transform.position.x;
        float currentYPos = transform.position.y;

        _leftGroundRaycastOrigin = new Vector2(currentXPos - _raycastXOffset, currentYPos);
        _leftGroundRaycastEnd = new Vector2(_leftGroundRaycastOrigin.x, currentYPos - _groundRayLength);

        _rightGroundRaycastOrigin = new Vector2(currentXPos + _raycastXOffset, currentYPos);
        _rightGroundRaycastEnd = new Vector2(_rightGroundRaycastOrigin.x, currentYPos - _groundRayLength);

        _upperLeftRaycastOrigin = new Vector2(currentXPos, currentYPos + _raycastYOffset);
        _upperLeftRaycastEnd = new Vector2(currentXPos - _wallRayLength, _upperLeftRaycastOrigin.y);

        _bottomLeftRaycastOrigin = new Vector2(currentXPos, currentYPos - _raycastYOffset);
        _bottomLeftRaycastEnd = new Vector2(currentXPos - _wallRayLength, _bottomLeftRaycastOrigin.y);

        _upperRightRaycastOrigin = new Vector2(currentXPos, currentYPos + _raycastYOffset);
        _upperRightRaycastEnd = new Vector2(currentXPos + _wallRayLength, _upperLeftRaycastOrigin.y);

        _bottomRightRaycastOrigin = new Vector2(currentXPos, currentYPos - _raycastYOffset);
        _bottomRightRaycastEnd = new Vector2(currentXPos + _wallRayLength, _bottomRightRaycastOrigin.y);
    }

    private void CheckIfOnWall()
    {
        if (_upperLeftRaycastHit == false && _bottomLeftRaycastHit == false && _upperRightRaycastHit == false &&
            _bottomRightRaycastHit == false)
        {
            OnWall = false;
            return;
        }

        OnWall = true;
    }

    private void CheckIfGrounded()
    {
        // If both of raycasts are not colliding, return
        if (_leftGroundRaycastHit == false && _rightGroundRaycastHit == false)
        {
            OnGround = false;
            return;
        }

        OnGround = true;
    }
}
