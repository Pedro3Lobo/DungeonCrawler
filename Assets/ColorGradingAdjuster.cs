using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorGradingAdjuster : MonoBehaviour
{
    public PostProcessVolume volume;
    private ColorGrading colorGrading;
    private float changeInterval = 2.0f; // Time in seconds between changes
    private float timer = 0.0f;
    private float startDelay = 120.0f; // Delay before starting the random changes, set to 2 minutes
    private bool startChanging = false;
    private Vector4 currentLift;
    private Vector4 targetLift;
    private float lerpDuration = 8.0f; // Duration over which to interpolate the lift
    private float lerpTimer = 0.0f;

    void Start()
    {
        if (volume.profile.TryGetSettings(out colorGrading))
        {
            currentLift = colorGrading.lift.value; // Initialize current lift
            Invoke(nameof(StartRandomChanges), startDelay); // Wait for 2 minutes before starting changes
        }
    }

    void Update()
    {
        if (startChanging)
        {
            if (lerpTimer < lerpDuration)
            {
                // Smoothly interpolate from currentLift to targetLift over lerpDuration seconds
                colorGrading.lift.value = Vector4.Lerp(currentLift, targetLift, lerpTimer / lerpDuration);
                lerpTimer += Time.deltaTime;
            }
            else
            {
                // Reset the timer and set new target lift
                timer += Time.deltaTime;
                if (timer >= changeInterval)
                {
                    timer = 0f;
                    currentLift = colorGrading.lift.value;
                    targetLift = new Vector4(Random.Range(0.75f, 1.2f), Random.Range(0.75f, 1.2f), Random.Range(0.75f, 1.2f), 0); // Randomize each RGB component of lift slightly
                    lerpTimer = 0f; // Reset lerp timer
                }
            }
        }
    }

    private void StartRandomChanges()
    {
        startChanging = true;
        targetLift = new Vector4(Random.Range(0f, 3f), Random.Range(0f, 3f), Random.Range(0f, 3f), 0); // Initial random lift
        lerpTimer = 0f; // Start interpolation
    }
}
