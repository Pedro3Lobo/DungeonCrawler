using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOVChanger : MonoBehaviour
{
    public Camera targetCamera; // Assign the target camera in the inspector
    public float newFOV = 60f; // Target FOV to transition to
    public float transitionDuration = 5f; // Duration of the FOV transition

    private void Start()
    {
        // Start the repeating change every 5 seconds
        InvokeRepeating(nameof(ChangeFOV), 0f, 5f);
    }

    private void ChangeFOV()
    {
        // Start the coroutine to change FOV
        StartCoroutine(ChangeFieldOfView(targetCamera.fieldOfView, newFOV, transitionDuration));
    }

    private IEnumerator ChangeFieldOfView(float startFOV, float endFOV, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            // Interpolate the FOV over time
            targetCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        targetCamera.fieldOfView = endFOV;
    }
}
