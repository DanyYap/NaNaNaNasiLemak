using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl : MonoBehaviour
{
    public PogoSettings settings;

    [Header("UI")]
    public GameObject jumpBarUI;
    public Image jumpBarFill;
    
    private Rigidbody rb;
    private float currentCharge = 0f;
    private float lastYVelocity;
    private bool isCharging = false;
    private bool isGrounded = true;
    private bool isTiltingInput = false;
    [SerializeField] private Animator pogoAnimator;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    
    void Start()
    {
        jumpBarUI.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rb.angularDamping = settings.angularDrag;
        rb.centerOfMass = settings.centerOfMassOffset;
    }

    void FixedUpdate()
    {
        lastYVelocity = rb.linearVelocity.y;

        AutoBalance();
        LimitTiltAngle();
    }

    void Update()
    {
        HandleJumpInput();
        HandleRotationInput();
        CheckGround();
        
        if (transform.position.y < -20f) // <- Adjust the threshold to your world size
        {
            Respawn();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
        }
    }
    
    private void Respawn()
    {
        // Set position to your designated respawn point
        transform.position = new Vector3(0, 15, -15); // Replace with your desired respawn point
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isCharging = true;
            currentCharge = settings.minJumpForce;
            jumpBarUI.SetActive(true);
    
            pogoAnimator?.SetBool("isCharging", true);
        }

        if (Input.GetKey(KeyCode.Space) && isCharging)
        {
            currentCharge += settings.chargeSpeed * Time.deltaTime;
            if (currentCharge >= settings.maxJumpForce || currentCharge <= settings.minJumpForce)
            {
                settings.chargeSpeed *= -1f; // Reverse direction for loop
            }

            // Normalize value for UI (0 to 1)
            float normalizedCharge = Mathf.InverseLerp(settings.minJumpForce, settings.maxJumpForce, currentCharge);
            jumpBarFill.fillAmount = normalizedCharge;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isCharging)
        {
            Jump();
            isCharging = false;
            jumpBarUI.SetActive(false);
            settings.chargeSpeed = Mathf.Abs(settings.chargeSpeed);

            pogoAnimator?.SetBool("isCharging", false);
        }
    }

    private void Jump()
    {
        Vector3 jumpDirection = transform.forward; // change if needed based on your model orientation

        // Add both upward and directional force
        Vector3 force = (Vector3.up + jumpDirection.normalized) * currentCharge;

        rb.AddForce(force, ForceMode.Impulse);
        currentCharge = 0f;
    }

    private void HandleRotationInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        isTiltingInput = v!= 0 || h != 0;

        if (isTiltingInput)
        {
            Vector3 rotation = new Vector3(-v, 0, h) * (settings.rotateSpeed * Time.deltaTime);
            transform.Rotate(rotation, Space.Self);
        }
    }

    private void CheckGround()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

        if (!wasGrounded && isGrounded)
        {
            if (lastYVelocity < -settings.minBounceVelocity)
            {
                float bounceForce = Mathf.Abs(lastYVelocity) * settings.bounceMultiplier;
                rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }
    }

    private void AutoBalance()
    {
        if (isTiltingInput) return;
        
        Vector3 up = transform.forward;
        Vector3 torque = Vector3.Cross(up, Vector3.up) * settings.autoBalanceStrength;
        rb.AddTorque(torque, ForceMode.Acceleration);
    }

    private void LimitTiltAngle()
    {
        float tiltAngle = Vector3.Angle(transform.forward, Vector3.up);
        if (tiltAngle > settings.maxTiltAngle)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.forward, Vector3.up) * rb.rotation;
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * settings.tiltRecoverySpeed));
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
