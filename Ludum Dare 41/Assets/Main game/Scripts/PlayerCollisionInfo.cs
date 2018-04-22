using UnityEngine;

public class PlayerCollisionInfo : MonoBehaviour
{
    internal bool OnGround;

    //internal bool OnWall;
    internal bool OnRightWall;
    internal bool OnLeftWall;

    [Header("Wall raycast settings")]
    [SerializeField] private Vector2 _raycastYOffset;
    [SerializeField] private float _raycastYSpreadDistance = 0.4f;
    [SerializeField] private float _wallRayLength = 0.8f;

    [Space(10)]
    [Header("Ground raycast settings")]
    [SerializeField] private Vector2 _raycastXOffset;
    [SerializeField] private float _raycastXSpeadDistance = 0.4f;
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

        _leftGroundRaycastOrigin = new Vector2(currentXPos + _raycastXOffset.x - _raycastXSpeadDistance, currentYPos + _raycastXOffset.y);
        _leftGroundRaycastEnd = new Vector2(_leftGroundRaycastOrigin.x, currentYPos - _groundRayLength + _raycastXOffset.y);

        _rightGroundRaycastOrigin = new Vector2(currentXPos + _raycastXOffset.x + _raycastXSpeadDistance, currentYPos + _raycastXOffset.y);
        _rightGroundRaycastEnd = new Vector2(_rightGroundRaycastOrigin.x, currentYPos - _groundRayLength + _raycastXOffset.y);

        _upperLeftRaycastOrigin = new Vector2(currentXPos + _raycastYOffset.x, currentYPos + _raycastYSpreadDistance + _raycastYOffset.y);
        _upperLeftRaycastEnd = new Vector2(currentXPos - _wallRayLength + _raycastYOffset.x, _upperLeftRaycastOrigin.y);

        _bottomLeftRaycastOrigin = new Vector2(currentXPos + _raycastYOffset.x, currentYPos - _raycastYSpreadDistance + _raycastYOffset.y);
        _bottomLeftRaycastEnd = new Vector2(currentXPos - _wallRayLength + _raycastYOffset.x, _bottomLeftRaycastOrigin.y);

        _upperRightRaycastOrigin = new Vector2(currentXPos + _raycastYOffset.x, currentYPos + _raycastYSpreadDistance + _raycastYOffset.y);
        _upperRightRaycastEnd = new Vector2(currentXPos + _wallRayLength + _raycastYOffset.x, _upperRightRaycastOrigin.y);

        _bottomRightRaycastOrigin = new Vector2(currentXPos + _raycastYOffset.x, currentYPos - _raycastYSpreadDistance + _raycastYOffset.y);
        _bottomRightRaycastEnd = new Vector2(currentXPos + _wallRayLength + _raycastYOffset.x, _bottomRightRaycastOrigin.y);
    }

    private void CheckIfOnWall()
    {
        if (_upperLeftRaycastHit == false && _bottomLeftRaycastHit == false && _upperRightRaycastHit == false &&
            _bottomRightRaycastHit == false)
        {
            OnRightWall = false;
            OnLeftWall = false;
            return;
        }

        // The 4 if statements below this could be a bit sketchy.
        // If you're having bugs with colliders and being on walls,
        // this is probably where that happens.

        #region SketchyButtonColliderException

        if (_upperLeftRaycastHit == true && _upperLeftRaycastHit.collider.gameObject.layer == 8)
        {
            OnLeftWall = false;
            return;
        }

        if (_bottomLeftRaycastHit == true && _bottomLeftRaycastHit.collider.gameObject.layer == 8)
        {
            OnLeftWall = false;
            return;
        }

        if (_upperRightRaycastHit == true && _upperRightRaycastHit.collider.gameObject.layer == 8)
        {
            OnRightWall = false;
            return;
        }

        if (_bottomRightRaycastHit == true && _bottomRightRaycastHit.collider.gameObject.layer == 8)
        {
            OnRightWall = false;
            return;
        }

        #endregion

        if (_upperLeftRaycastHit == true || _bottomLeftRaycastHit == true)
        {
            OnLeftWall = true;
            OnRightWall = false;
            return;
        }

        if (_upperRightRaycastHit == true || _bottomRightRaycastHit == true)
        {
            OnRightWall = true;
            OnLeftWall = false;
        }
    }

    private void CheckIfGrounded()
    {
        // If both of raycasts are not colliding, return
        if (_leftGroundRaycastHit == false && _rightGroundRaycastHit == false)
        {
            OnGround = false;
            return;
        }

        if (_leftGroundRaycastHit == true && _leftGroundRaycastHit.collider.gameObject.layer == 8)
        {
            OnGround = false;
            return;
        }

        if (_rightGroundRaycastHit == true && _rightGroundRaycastHit.collider.gameObject.layer == 8)
        {
            OnGround = false;
            return;
        }

        OnGround = true;
    }
}
