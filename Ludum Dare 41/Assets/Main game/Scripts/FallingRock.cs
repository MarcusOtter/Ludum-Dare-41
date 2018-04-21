using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField] private float decayTime;

	private void Start ()
	{
		Destroy(gameObject, decayTime);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            // TODO: Damage player
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
