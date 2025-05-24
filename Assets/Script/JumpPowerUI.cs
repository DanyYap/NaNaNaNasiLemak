using UnityEngine;
using UnityEngine.UI;

public class JumpPowerUI : MonoBehaviour
{
    public Image jumpPowerFillImage;
    public GameObject barContainer;
    public float maxChargeTime = 2f; // Max time to fully charge
    private float chargeTimer = 0f;
    private bool isCharging = false;

    public KeyCode chargeKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(chargeKey))
        {
            isCharging = true;
            chargeTimer = 0f;
            barContainer.SetActive(true);
        }

        if (Input.GetKey(chargeKey) && isCharging)
        {
            chargeTimer += Time.deltaTime;
            chargeTimer = Mathf.Clamp(chargeTimer, 0f, maxChargeTime);
            jumpPowerFillImage.fillAmount = chargeTimer / maxChargeTime;
        }

        if (Input.GetKeyUp(chargeKey))
        {
            isCharging = false;
            barContainer.SetActive(false);
            jumpPowerFillImage.fillAmount = 0f;
        }
    }
}
