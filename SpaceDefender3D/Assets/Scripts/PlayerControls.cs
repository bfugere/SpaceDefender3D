using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("Screen Clamp Constraints")]
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 8f;

    [Header("Ship Tuning")]
    [SerializeField] float moveSpeed = 30f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlRollFactor = -30f;

    [Header("Weapons")]
    [SerializeField] GameObject[] lasers;
    [SerializeField] AudioClip projectileSFX;
    [SerializeField] [Range(0, 1)] float projectileVolume = 0.025f;

    float xTilt;
    float yTilt;

    void Update()
    {
        ProcessPosition();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessPosition()
    {
        xTilt = Input.GetAxis("Horizontal");
        yTilt = Input.GetAxis("Vertical");

        float xOffset = xTilt * moveSpeed * Time.deltaTime;
        float yOffset = yTilt * moveSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yTilt * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xTilt * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            ActivateLasers(true);
            AudioSource.PlayClipAtPoint(projectileSFX, Camera.main.transform.position, projectileVolume);
        }
        else
            ActivateLasers(false);
    }

    public void ActivateLasers(bool isActive)
    {
        foreach (var laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
