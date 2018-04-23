using UnityEngine;

public class RopePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("AudioController").GetComponent<AmbientAudioController>().AddRope();
            Destroy(gameObject);
        }
    }
}
