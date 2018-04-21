using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private void LateUpdate()
    {
        transform.position += new Vector3(_movementSpeed * Time.deltaTime, 0, 0);
    }
}
