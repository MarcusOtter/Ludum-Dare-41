using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private bool _spawnedOnce;

    private void OnEnable()
    {
        MouseInput.RightMouseHeld += SpawnPlatform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            _spawnedOnce = false;
        }
    }

    private void SpawnPlatform()
    {
        if (!_spawnedOnce)
        {
            print("called");
            _spawnedOnce = true;
        }
        // TODO
    }

    private void OnDisable()
    {
        MouseInput.RightMouseHeld -= SpawnPlatform;
    }
}
