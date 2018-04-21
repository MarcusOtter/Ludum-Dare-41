using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _rock;
    [SerializeField] private float _rocksPerSecond;
    [SerializeField] private Transform _leftBoundary, _rightBoundary;

    private bool _spawnRocks = true;

    private void Start()
    {
        StartCoroutine(SpawnRocks());
    }

    private IEnumerator SpawnRocks()
    {
        while (_spawnRocks)
        {
            yield return new WaitForSeconds(1 / _rocksPerSecond);
            float randomX = Random.Range(_leftBoundary.position.x, _rightBoundary.position.x);
            float yPos = _leftBoundary.position.y;

            Instantiate(_rock, new Vector3(randomX, yPos, 0), transform.rotation);

        }
    }

}
