using UnityEngine;

public class ObjectMechanic : MonoBehaviour
{
    [Tooltip("Delay before the object is destroyed (in seconds). Set to 0 for instant destruction.")]
    public float destroyDelay = 1f;
    
    [Tooltip("Animator located under a child object")]
    public Animator childAnimator;

    [Tooltip("Name of the trigger parameter in the Animator")]
    public string animationTriggerName = "Play";

    public bool isWrong;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayAnimation();
            if (isWrong)
            {
                Destroy(gameObject, destroyDelay);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayAnimation();
            if (isWrong)
            {
                Destroy(gameObject, destroyDelay);
            }
        }
    }

    private void PlayAnimation()
    {
        if (childAnimator != null && !string.IsNullOrEmpty(animationTriggerName))
        {
            childAnimator.SetTrigger(animationTriggerName);
        }
        else
        {
            Debug.LogWarning("Animator or trigger name not set on " + gameObject.name);
        }
    }
}
