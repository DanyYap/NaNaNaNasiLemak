using UnityEngine;

public class ObjectMechanic : MonoBehaviour
{
    [Tooltip("Delay before the object is destroyed (in seconds). Set to 0 for instant destruction.")]
    public float destroyDelay = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
