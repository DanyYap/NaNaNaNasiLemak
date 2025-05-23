using UnityEngine;

[CreateAssetMenu(fileName = "PogoSettings", menuName = "Pogo Stick/Settings")]
public class PogoSettings : ScriptableObject
{
    [Header("Jump Settings")]
    public float minJumpForce = 5f;
    public float maxJumpForce = 15f;
    public float chargeSpeed = 10f;

    [Header("Rotation Settings")]
    public float rotateSpeed = 100f;

    [Header("Bounce Settings")]
    public float minBounceVelocity = 2f;
    public float bounceMultiplier = 0.5f;

    [Header("Balance Settings")]
    public float autoBalanceStrength = 5f;
    public float maxTiltAngle = 60f;
    public float tiltRecoverySpeed = 2f;
    public float angularDrag = 2f;
    public Vector3 centerOfMassOffset = new Vector3(0, -0.5f, 0);
}
