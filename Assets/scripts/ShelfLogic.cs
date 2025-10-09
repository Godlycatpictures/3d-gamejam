using UnityEngine;

public class ShelfLogic : MonoBehaviour
{
    public AudioSource audioSource;
    private Animator anim;
    public bool HasShelfKey = false;
    private playerInteract playerInteract;

    public AudioClip openCabinet;
    public AudioClip closeCabinet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HasShelfKey = false;
        anim = GetComponent<Animator>();
        playerInteract = FindFirstObjectByType<playerInteract>();
    }

    // Update is called once per frame
    void Update()
    {
         // HasShelfKey ska kopplas till anim.Setbool("hasOpened", true)
    }

    public void DrawerOpen()
    {
        if (HasShelfKey)
        {
            anim.SetBool("InteractShelf", true);
            audioSource.PlayOneShot(openCabinet);
        } else
        {
            playerInteract.SmoothCamExit(); // har inte du neckeln elr nåt så kan du inte öppna
        }
        
    }
    public void DrawerClose()
    {
        anim.SetBool("InteractShelf", false);
        audioSource.PlayOneShot(closeCabinet);
    }
}