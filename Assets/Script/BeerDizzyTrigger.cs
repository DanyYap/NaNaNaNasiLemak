using UnityEngine;

public class BeerDizzyTrigger : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDizzyEffect dizzy = other.GetComponent<PlayerDizzyEffect>();
            Debug.Log("Got particle");
            if (dizzy != null)
            {
                Debug.Log("Go Dizzy");
                dizzy.TriggerDizzy(); 
            }
        }
    }
}
