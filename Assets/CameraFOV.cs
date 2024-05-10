using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    public Camera targetCamera; // Assign the target camera in the inspector
    public float normalFOV = 60f; // Normal FOV of the camera
    public float newFOV = 140f; // Target FOV to transition to
    public float transitionDuration = 5f; // Duration of the FOV transition

    private void Start()
    {
        Debug.Log("Camera FOV changer started.");
        // Start the repeating change every 60 seconds
        InvokeRepeating(nameof(ChangeFOV), 360f, 60f);
    }

    private void ChangeFOV()
    {
        Debug.Log("Starting FOV change from " + targetCamera.fieldOfView + " to " + newFOV);
        // Start the coroutine to change FOV
        StartCoroutine(ChangeFieldOfView(targetCamera.fieldOfView, newFOV, transitionDuration));
    }

    private IEnumerator ChangeFieldOfView(float startFOV, float endFOV, float duration)
    {
        float elapsed = 0f;
        float originalFOV = targetCamera.fieldOfView; // Store the original FOV

        while (elapsed < duration)
        {
            float newFOVValue = Mathf.Lerp(startFOV, endFOV, elapsed / duration);
            targetCamera.fieldOfView = newFOVValue;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure FOV is exactly the desired value
        targetCamera.fieldOfView = endFOV;

        // Wait for a short moment before returning to normal FOV
        yield return new WaitForSeconds(10f);

        // Return to normal FOV
        StartCoroutine(ChangeFieldOfView(endFOV, originalFOV, transitionDuration));
    }
}