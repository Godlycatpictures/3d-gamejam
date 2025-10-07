using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public Rigidbody rb;
    public LayerMask groundLayer;
    public float groundCheckDistance = 2f;

    private void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f; // Lägre startpunkt
        bool isGrounded = Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, groundCheckDistance, groundLayer);
        float speed = rb.linearVelocity.magnitude;

        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
        Debug.Log($"Grounded: {isGrounded} | Speed: {speed} | Audio: {(audioSource != null)}");

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (footstepClips.Length > 0)
            {
                Debug.Log("Manual Footstep test!");
                audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
            }
        }

        if (isGrounded && speed > 0.2f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f);
                audioSource.PlayOneShot(footstepClips[Random.Range(0, footstepClips.Length)]);
                Debug.Log("Auto Footstep played!");
            }
        }
    }
}