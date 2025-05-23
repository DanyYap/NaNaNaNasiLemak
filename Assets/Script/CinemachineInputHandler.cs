using UnityEngine;
using Unity.Cinemachine;

public class CinemachineInputHandler : MonoBehaviour
{
    public float sensitivityX = 2f;
    public float sensitivityY = 1.5f;
    public Transform target;

    private float rotationX;
    private float rotationY;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -30f, 80f);

        target.rotation = Quaternion.Euler(rotationY, rotationX, 0);
    }
}
