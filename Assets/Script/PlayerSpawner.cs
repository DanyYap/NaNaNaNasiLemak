using UnityEngine;
using Unity.Cinemachine;
public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public float launchForce = 10f;

    public CinemachineCamera gameplayCamera;

    private GameObject spawnedPlayer;

    void Start()
    {
        SpawnAndLaunchPlayer();
    }

    public void SpawnAndLaunchPlayer()
    {
        if (playerPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Missing references.");
            return;
        }

        // Spawn player
        spawnedPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // Apply launch force
        Rigidbody rb = spawnedPlayer.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(spawnPoint.forward * launchForce, ForceMode.VelocityChange);
        }

        // Find the CameraTarget child
        Transform cameraTarget = spawnedPlayer.transform.Find("Camera Target");
        if (cameraTarget != null && gameplayCamera != null)
        {
            gameplayCamera.Follow = cameraTarget;
            gameplayCamera.LookAt = cameraTarget;
        }
        else
        {
            Debug.LogWarning("CameraTarget not found or gameplayCamera not assigned.");
        }
    }
}
