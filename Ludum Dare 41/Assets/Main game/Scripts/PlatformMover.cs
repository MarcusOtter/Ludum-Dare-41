using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private PlatformSpawner _platformSpawner;

    [Space(10)]
    [Header("Arrow tiles")]
    [SerializeField] private Tile _leftArrowTile;
    [SerializeField] private Tile _upArrowTile;
    [SerializeField] private Tile _rightArrowTile;
    [SerializeField] private Tile _downArrowTile;

    [Space(10)]
    [Header("Tilemap to move")]
    [SerializeField] private Transform _movingPlatformTilemapTransform;
    
    [Space(10)]
    [Header("Restrictions")]
    [SerializeField] private float _maxHeight;

    [Space(10)]
    [Header("Required player variables")]
    [SerializeField] private Transform _upperLeft;
    [SerializeField] private Transform _upperRight;
    [SerializeField] private Transform _bottomLeft;
    [SerializeField] private Transform _bottomRight;
    [SerializeField] private GameObject _player;

    private Tilemap _tilemap;
    private MouseInput _mouseInput;

    private bool _mouseOverButton;
    private Vector3 _platformOffset;
    

    private void OnEnable()
    {
        MouseInput.RightClick += TryMovePlatforms;
    }

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
        _mouseInput = Camera.main.GetComponent<MouseInput>();
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
            MovePlatform(newPlatformPosition);
        }

        if (_tilemap.GetTile(tilePosition) == _upArrowTile)
        {
            newPlatformPosition = new Vector3(_movingPlatformTilemapTransform.position.x, _movingPlatformTilemapTransform.position.y + 1);

            if (TilesOutOfBounds(newPlatformPosition))
            {
                return;
            }

            if (PositionHasInterferenceWithPlayer(newPlatformPosition))
            {
                MovePlayerUp();
            }

            MovePlatform(newPlatformPosition);
        }

        if (_tilemap.GetTile(tilePosition) == _rightArrowTile)
        {
            newPlatformPosition = new Vector3(_movingPlatformTilemapTransform.position.x + 1, _movingPlatformTilemapTransform.position.y);
            MovePlatform(newPlatformPosition);
        }

        if (_tilemap.GetTile(tilePosition) == _downArrowTile)
        {
            newPlatformPosition = new Vector3(_movingPlatformTilemapTransform.position.x, _movingPlatformTilemapTransform.position.y - 1);

            if (PositionHasInterferenceWithPlayer(newPlatformPosition))
            {
                return;
            }

            MovePlatform(newPlatformPosition);

        }

    }

    private bool TilesOutOfBounds(Vector3 newPlatformPosition)
    {
        _platformOffset = new Vector3Int((int)newPlatformPosition.x, (int)newPlatformPosition.y, 0);

        foreach (var platform in _platformSpawner.SpawnedPlatforms)
        {
            if (platform.Positions[0].y + _platformOffset.y > _maxHeight)
            {
                return true;
            }
        }

        return false;
    }

    private void MovePlayerUp()
    {
        _player.transform.position += Vector3.up; 
    }

    private void MovePlatform(Vector3 newPlatformPosition)
    {
        _movingPlatformTilemapTransform.position = newPlatformPosition;
    }

    private bool PositionHasInterferenceWithPlayer(Vector3 newPlatformPosition)
    {
        _platformOffset = new Vector3Int((int)newPlatformPosition.x, (int)newPlatformPosition.y, 0);
        
        Vector3Int upperLeft = new Vector3Int(Mathf.FloorToInt(_upperLeft.position.x), Mathf.FloorToInt(_upperLeft.position.y), 0);
        Vector3Int upperRight = new Vector3Int(Mathf.FloorToInt(_upperRight.position.x), Mathf.FloorToInt(_upperRight.position.y), 0);
        Vector3Int bottomLeft = new Vector3Int(Mathf.FloorToInt(_bottomLeft.position.x), Mathf.FloorToInt(_bottomLeft.position.y), 0);
        Vector3Int bottomRight = new Vector3Int(Mathf.FloorToInt(_bottomRight.position.x), Mathf.FloorToInt(_bottomRight.position.y), 0);

        for (int i = 0; i < _platformSpawner.SpawnedPlatforms.Count; i++)
        {
            for (int j = 0; j < _platformSpawner.SpawnedPlatforms[i].Positions.Count; j++)
            {
                if (_platformSpawner.SpawnedPlatforms[i].Positions[j] + _platformOffset == bottomLeft ||
                    _platformSpawner.SpawnedPlatforms[i].Positions[j] + _platformOffset == bottomRight ||
                    _platformSpawner.SpawnedPlatforms[i].Positions[j] + _platformOffset == upperLeft ||
                    _platformSpawner.SpawnedPlatforms[i].Positions[j] + _platformOffset == upperRight)
                {
                    return true;
                }
            }
        }

        return false;
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
