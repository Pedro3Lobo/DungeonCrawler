using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    public Camera targetCamera; // Assign the target camera in the inspector
    public float newFOV = 60f; // Target FOV to transition to
    public float transitionDuration = 5f; // Duration of the FOV transition
    public int x = 5;

    private void Start()
    {
        Debug.Log("Camera FOV changer started.");
        // Start the repeating change every 5 seconds
        InvokeRepeating(nameof(ChangeFOV), 0f, 60f);
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
        while (elapsed < duration)
        {
            targetCamera.fieldOfView = x+5;
            x = x + 5; 
            Debug.Log("Changing FOV: " + targetCamera.fieldOfView);
            elapsed += Time.deltaTime;
            yield return null;
        }

        targetCamera.fieldOfView = 60;
        x = 60;
        Debug.Log("FOV change complete: " + targetCamera.fieldOfView);
        yield return new WaitForSeconds(60);
    }
}