using UnityEngine;

public class PanCamera : MonoBehaviour
{
    [SerializeField] private float _cameraPanSpeed;
    [SerializeField] private Transform _targetTransform;

    private bool _moveCamera;
    private Transform _cameraTransform;

    private void Update()
    {
        if (_moveCamera && _cameraTransform.position.x < transform.position.x)
        {
            float cameraXMovement = Mathf.Lerp(_cameraTransform.position.x, _targetTransform.position.x, Time.deltaTime);
            cameraXMovement = Mathf.Clamp(cameraXMovement, 0, 0.1f) * _cameraPanSpeed;
            _cameraTransform.position = new Vector3(_cameraTransform.position.x + cameraXMovement, _cameraTransform.position.y, _cameraTransform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _moveCamera = true;
            _cameraTransform = Camera.main.transform;
        }
    }
}
