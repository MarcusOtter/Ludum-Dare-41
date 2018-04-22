using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformMover : MonoBehaviour
{
    [Header("Arrow tiles")]
    [SerializeField] private Tile _leftArrowTile;
    [SerializeField] private Tile _upArrowTile;
    [SerializeField] private Tile _rightArrowTile;
    [SerializeField] private Tile _downArrowTile;

    [Space(10)]
    [Header("Tilemap to move")]
    [SerializeField] private Transform _movingPlatformTilemapTransform;

    private Tilemap _movingPlatformTilemap;
    private Tilemap _tilemap;
    private MouseInput _mouseInput;

    private bool _mouseOverButton;

    private void OnEnable()
    {
        MouseInput.RightClick += TryMovePlatforms;
    }

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _mouseInput = Camera.main.GetComponent<MouseInput>();
        _movingPlatformTilemap = _movingPlatformTilemapTransform.GetComponent<Tilemap>();
    }

    private void TryMovePlatforms()
    {
        if (!_mouseOverButton)
        {
            return;
        }

        MovePlatforms(_mouseInput.MouseWorldPosition);
    }

    private void MovePlatforms(Vector2 mousePosition)
    {
        Vector3Int tilePosition = new Vector3Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y), 0);

        Vector3 newPlatformPosition = Vector3.zero;

        if (_tilemap.GetTile(tilePosition) == _leftArrowTile)
        {
            newPlatformPosition = new Vector3(_movingPlatformTilemapTransform.position.x - 1, _movingPlatformTilemapTransform.position.y);
        }

        if (_tilemap.GetTile(tilePosition) == _upArrowTile)
        {
            newPlatformPosition = new Vector3(_movingPlatformTilemapTransform.position.x, _movingPlatformTilemapTransform.position.y + 1);
        }

        if (_tilemap.GetTile(tilePosition) == _rightArrowTile)
        {
            newPlatformPosition = new Vector3(_movingPlatformTilemapTransform.position.x + 1, _movingPlatformTilemapTransform.position.y);
        }

        if (_tilemap.GetTile(tilePosition) == _downArrowTile)
        {
            newPlatformPosition = new Vector3(_movingPlatformTilemapTransform.position.x, _movingPlatformTilemapTransform.position.y - 1);
        }

        if (newPlatformPosition != Vector3.zero)
        {
            _movingPlatformTilemapTransform.position = newPlatformPosition;
        }
    }

    private void OnDisable()
    {
        MouseInput.RightClick -= TryMovePlatforms;
    }

    private void OnMouseOver()
    {
        _mouseOverButton = true;
    }

}
