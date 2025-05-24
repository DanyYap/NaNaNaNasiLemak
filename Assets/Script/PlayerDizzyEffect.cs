using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Unity.Cinemachine;

public class PlayerDizzyEffect : MonoBehaviour
{
    public Volume postProcessVolume;
    public float effectDuration = 15f;
    private float timer;

    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;

    private bool isDizzy = false;

    public CinemachineImpulseSource impulseSource;
    private Transform camTransform;
    private Vector3 originalCamPosition;

    public float wobbleIntensity = 0.8f;
    public float wobbleSpeed = 40f;

    void Start()
    {
        camTransform = Camera.main.transform;
        originalCamPosition = camTransform.localPosition;

        if (postProcessVolume.profile.TryGet(out lensDistortion) &&
            postProcessVolume.profile.TryGet(out chromaticAberration))
        {
            lensDistortion.intensity.value = 0;
            chromaticAberration.intensity.value = 0;
        }
    }

    public void TriggerDizzy()
    {
        isDizzy = true;
        timer = effectDuration;

        // Trigger camera shake
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
            Debug.Log("Generate Impulse");
        }
    }

    void Update()
    {
        if (isDizzy)
        {
            timer -= Time.deltaTime;

            // Post process animation
            float t = Mathf.PingPong(Time.time * 2f, 1f);
            lensDistortion.intensity.value = Mathf.Lerp(-0.8f, 0.8f, t);
            chromaticAberration.intensity.value = Mathf.Lerp(0f, 1.5f, t);

            // Wobble camera locally
            Vector3 wobble = new Vector3(
                Mathf.Sin(Time.time * wobbleSpeed),
                Mathf.Cos(Time.time * wobbleSpeed * 1.2f),
                0f) * wobbleIntensity;

            camTransform.localPosition = originalCamPosition + wobble;

            if (timer <= 0f)
            {
                isDizzy = false;
                lensDistortion.intensity.value = 0;
                chromaticAberration.intensity.value = 0;
                camTransform.localPosition = originalCamPosition;
            }
        }
    }
}
