using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSounds : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;

    [Header("Player Settings")]
    public Rigidbody rb;
    public LayerMask groundLayer;
    public float groundCheckDistance = 2.0f;
    public float minSpeedToStep = 0.2f;

    private float stepTimer;

    void Start()

    {
        // Hämta komponenter om de inte är manuellt tillagda
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (rb != null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Skicka raycast från lite ovanför botten av spelaren
        bool isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, groundCheckDistance, groundLayer);

        // Kolla spelarens hastighet
        float speed = rb.linearVelocity.magnitude;

        // Debug för att se vad som händer (ta bort om du vill)
        // Debug.Log($"Grounded: {isGrounded} | Speed: {speed}");

        // Spela fotsteg om man rör sig på marken
        if (isGrounded && speed > minSpeedToStep)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        int index = Random.Range(0, footstepClips.Length);
        AudioClip clip = footstepClips[index];

        // Lite variation i pitch för mer naturligt ljud
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(clip);

        // Debug.Log("Footstep: " + clip.name);
    }
}
