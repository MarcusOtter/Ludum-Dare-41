using UnityEngine;

public class PlayerHeightRestriction : MonoBehaviour
{
    [SerializeField] private float _maxHeight;

    private Rigidbody2D _rigidbody;
    private Transform _transform;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    private void Update()
    {
        if (_transform.position.y > _maxHeight && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        }
    }

}
