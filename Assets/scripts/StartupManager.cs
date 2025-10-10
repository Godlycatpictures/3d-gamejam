using UnityEngine;

public class StartupManager : MonoBehaviour
{
    [SerializeField] private AudioClip startupSFX;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private playerInteract player;
    [SerializeField] private ComputerSoundManager computerSoundManager;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (player.isInteracting && player.isOnPc)
        {
            print("Clicked");
            anim.SetTrigger("Startup");
            computerSoundManager.EnableComputerSounds();
        }
    }
    public void PlayStartSFX()
    {
        audioSource.PlayOneShot(startupSFX);
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
