using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnRope : MonoBehaviour
{

    [SerializeField] private Tile _ropeTile;
    private Tilemap _ropeTilemap;

    private void Start()
    {
        //_controller = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AmbientAudioController>();
        _ropeTilemap = GetComponent<Tilemap>();
        SpawnAllRopes();
    }

    internal void SpawnAllRopes()
    {
        // Last minute hardcoding panic
        Vector3Int startPos = new Vector3Int(13, 13, 0);

        for (int i = 0; i < AmbientAudioController.RopeCount; i++)
        {
            _ropeTilemap.SetTile(new Vector3Int(startPos.x, startPos.y - i, 0), _ropeTile);
        }
    }
}
