using UnityEngine;

public class AddLastRope : MonoBehaviour
{
    [SerializeField] private SpawnRope _spawnRope;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _spawnRope.SpawnAllRopes();
        }
    }
}
