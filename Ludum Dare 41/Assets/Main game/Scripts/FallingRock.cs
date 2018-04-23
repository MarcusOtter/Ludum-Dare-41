using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private float _decayTime;
    [SerializeField] private GameObject _stoneCrackAnimation;
    [SerializeField] private float _rotateSpeed;

	private void Start ()
	{
		Destroy(gameObject, _decayTime);
	}

    private void Update()
    {
        transform.Rotate(0, 0, -_rotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            return;
        }

        Instantiate(_stoneCrackAnimation, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject);
    }
}
