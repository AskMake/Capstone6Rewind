using UnityEngine;
using System.Collections; // <-- Add this at the top

public class TrainAccelerator : MonoBehaviour
{
    public Transform[] waypoints;
    public float maxSpeed = 10f;
    public float acceleration = 2f;
    public float fadeDuration = 2f;  // Duration of sound fade-out

    private float currentSpeed = 0f;
    private int currentIndex = 0;
    private bool moving = true;
    private AudioSource audioSource;
    private float initialVolume;

    void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Save the initial volume of the audio
        initialVolume = audioSource.volume;

        // Start the sound when the train starts moving
        audioSource.Play();
    }

    void Update()
    {
        if (!moving || currentIndex >= waypoints.Length)
            return;

        Transform target = waypoints[currentIndex];

        // Gradually increase speed
        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);

        // Move and rotate train
        transform.position = Vector3.MoveTowards(transform.position, target.position, currentSpeed * Time.deltaTime);
        transform.LookAt(target);

        // Reached current waypoint?
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentIndex++;

            // Reached final waypoint (Waypoint 2)?
            if (currentIndex >= waypoints.Length)
            {
                StartCoroutine(FadeOutSound(fadeDuration)); // Start fading the sound
                moving = false;
                gameObject.SetActive(false); // Hide the train after moving
            }
        }
    }

    // Coroutine to fade out the sound
    IEnumerator FadeOutSound(float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.volume = 0;
    }
}