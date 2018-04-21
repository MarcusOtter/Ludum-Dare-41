using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Tilemap _platformTilemap;
    [SerializeField] private Tile _platformTile;

    [SerializeField] private int _maxPlatforms;

    private List<Platform> _spawnedPlatforms = new List<Platform>();

    private Transform _playerTransform;

    private bool _spawnedOnce;
    private Vector3Int _platformSpawnOffset;

    private void OnEnable()
    {
        MouseInput.RightMouseHeld += InitializePlatformSpawning;
    }

    private void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            _spawnedOnce = false;
        }
    }

    private void InitializePlatformSpawning()
    {
        if (_spawnedOnce)
        {
            return;
        }

        int middleTileX = Mathf.RoundToInt(_playerTransform.position.x - 0.5f);
        int middleTileY = Mathf.RoundToInt(_playerTransform.position.y + 2);
        _platformSpawnOffset = new Vector3Int(-(int)_platformTilemap.transform.position.x, -(int)_platformTilemap.transform.position.y, 0);

        var platform = new Platform();
        platform.Positions.Add(new Vector3Int(middleTileX - 1 + _platformSpawnOffset.x, middleTileY + _platformSpawnOffset.y, 0));    // Leftmost tile
        platform.Positions.Add(new Vector3Int(middleTileX + _platformSpawnOffset.x, middleTileY + _platformSpawnOffset.y, 0));        // Middle tile
        platform.Positions.Add(new Vector3Int(middleTileX + 1 + _platformSpawnOffset.x, middleTileY + _platformSpawnOffset.y, 0));    // Rightmost tile

        platform.Tiles.Add(_platformTile);
        platform.Tiles.Add(_platformTile);
        platform.Tiles.Add(_platformTile);

        if (_spawnedPlatforms.Count >= _maxPlatforms)
        {
            RemoveFirstPlatform();
        }

        SpawnPlatform(platform);
    }

    private void RemoveFirstPlatform()
    {
        var platformToRemove = _spawnedPlatforms[0];

        foreach (var tilePosition in platformToRemove.Positions)
        {
            _platformTilemap.SetTile(tilePosition, null);
        }

        _spawnedPlatforms.RemoveAt(0);
    }

    private void SpawnPlatform(Platform platform)
    {
        for (int i = 0; i < platform.Positions.Count; i++)
        {
            _platformTilemap.SetTile(platform.Positions[i], platform.Tiles[i]);
        }

        _spawnedPlatforms.Add(platform);
        _spawnedOnce = true;
    }

    private void OnDisable()
    {
        MouseInput.RightMouseHeld -= InitializePlatformSpawning;
    }

    [System.Serializable]
    public class Platform
    {
        internal List<Tile> Tiles = new List<Tile>();
        internal List<Vector3Int> Positions = new List<Vector3Int>();
    }
}
