using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General setup settings")]
    [Tooltip("how far left and right ship moves")] [SerializeField] float xOffset = 30f;
    [Tooltip("how far up and down ship moves")] [SerializeField] float yOffset = 30f;

    [Header("Screen position based tunings")]
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float yawFactor = 2f;

    [Header("User input based tunings")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controllRollFactor = -15f;

    [Header("Laser gun array")]
    [Tooltip("add lasers here")]
    [SerializeField] GameObject[] lasers;

    AudioSource laserVFX;

    float xThrow;
    float yThrow;
    
    void Start()
    {
        laserVFX = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float newX = xThrow * Time.deltaTime * xOffset + transform.localPosition.x;
        float newY = yThrow * Time.deltaTime * yOffset + transform.localPosition.y;

        float xClamp = Mathf.Clamp(newX, -12f, 12f);
        float yClamp = Mathf.Clamp(newY, -8f, 10f);

        transform.localPosition = new Vector3(xClamp, yClamp, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchPosition = transform.localPosition.y * pitchFactor;
        float pitchControl = yThrow * controlPitchFactor;
        float pitch = pitchPosition + pitchControl;

        float yaw = transform.localPosition.x * yawFactor;

        float roll = xThrow * controllRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            ActivateLasers(true);
        }
        else
        {
            ActivateLasers(false);
        }
    }
    private void ActivateLasers(bool isActive)
    {
        if(isActive && !laserVFX.isPlaying){
            laserVFX.Play();
        }
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
