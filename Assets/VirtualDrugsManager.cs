using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VirtualDrugsManager : MonoBehaviour
{
    public PostProcessVolume volume;
    private LensDistortion lensDistortion;
    private float changeInterval = 5.0f; // Time in seconds between changes
    private float timer = 0.0f;
    private float startDelay = 120.0f; // Delay before starting the random changes, set to 2 minutes
    private bool startChanging = false;
    private float currentIntensity;
    private float targetIntensity;
    private float lerpDuration = 2.0f; // Duration over which to interpolate the intensity
    private float lerpTimer = 0.0f;

    void Start()
    {
        if (volume.profile.TryGetSettings(out lensDistortion))
        {
            currentIntensity = lensDistortion.intensity.value; // Initialize current intensity
            Invoke(nameof(StartRandomChanges), startDelay); // Wait for 2 minutes before starting changes
        }
    }

    void Update()
    {
        if (startChanging)
        {
            if (lerpTimer < lerpDuration)
            {
                // Smoothly interpolate from currentIntensity to targetIntensity over lerpDuration seconds
                lensDistortion.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, lerpTimer / lerpDuration);
                lerpTimer += Time.deltaTime;
            }
            else
            {
                // Reset the timer and set new target intensity
                timer += Time.deltaTime;
                if (timer >= changeInterval)
                {
                    timer = 0f;
                    currentIntensity = lensDistortion.intensity.value;
                    targetIntensity = Random.Range(-50f, 50f);
                    lerpTimer = 0f; // Reset lerp timer
                }
            }
        }
    }

    private void StartRandomChanges()
    {
        startChanging = true;
        targetIntensity = Random.Range(-100f, 100f); // Initial random distortion
        lerpTimer = 0f; // Start interpolation
    }
}
