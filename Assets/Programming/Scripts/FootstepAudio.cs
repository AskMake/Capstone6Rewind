using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CharacterController))]
public class FootstepAudio : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public float walkStepRate = 0.5f;
    public float sprintStepRate = 0.3f;

    private AudioSource audioSource;
    private CharacterController controller;
    private float stepTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isMoving = controller.velocity.magnitude > 0.1f;
        bool isGrounded = controller.isGrounded;
        bool isSprinting = Input.GetKey(KeyCode.LeftShift); // Sprint key

        float currentStepRate = isSprinting ? sprintStepRate : walkStepRate;

        if (isGrounded && isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = currentStepRate;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}