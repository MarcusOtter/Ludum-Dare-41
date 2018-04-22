using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformSpawner : MonoBehaviour
{
    internal List<Platform> SpawnedPlatforms = new List<Platform>();
    internal Vector3Int PlatformSpawnOffset;

    [SerializeField] private Tilemap _platformTilemap;
    [SerializeField] private Tile _platformTile;

    [SerializeField] private int _maxPlatforms;

    private Transform _playerTransform;

    private bool _spawnedOnce;

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
        PlatformSpawnOffset = new Vector3Int(-(int)_platformTilemap.transform.position.x, -(int)_platformTilemap.transform.position.y, 0);

        var platform = new Platform();
        platform.Positions.Add(new Vector3Int(middleTileX - 1 + PlatformSpawnOffset.x, middleTileY + PlatformSpawnOffset.y, 0));    // Leftmost tile
        platform.Positions.Add(new Vector3Int(middleTileX + PlatformSpawnOffset.x, middleTileY + PlatformSpawnOffset.y, 0));        // Middle tile
        platform.Positions.Add(new Vector3Int(middleTileX + 1 + PlatformSpawnOffset.x, middleTileY + PlatformSpawnOffset.y, 0));    // Rightmost tile

        platform.Tiles.Add(_platformTile);
        platform.Tiles.Add(_platformTile);
        platform.Tiles.Add(_platformTile);

        if (SpawnedPlatforms.Count >= _maxPlatforms)
        {
            RemoveFirstPlatform();
        }

        SpawnPlatform(platform);
    }

    private void RemoveFirstPlatform()
    {
        var platformToRemove = SpawnedPlatforms[0];

        foreach (var tilePosition in platformToRemove.Positions)
        {
            _platformTilemap.SetTile(tilePosition, null);
        }

        SpawnedPlatforms.RemoveAt(0);
    }

    private void SpawnPlatform(Platform platform)
    {
        for (int i = 0; i < platform.Positions.Count; i++)
        {
            _platformTilemap.SetTile(platform.Positions[i], platform.Tiles[i]);
        }

        SpawnedPlatforms.Add(platform);
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
