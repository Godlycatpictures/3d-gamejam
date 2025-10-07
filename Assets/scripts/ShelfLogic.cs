using UnityEngine;

public class ShelfLogic : MonoBehaviour
{

    private Animator anim;
    [SerializeField] private bool HasShelfKey = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawerOpen()
    {
        if (HasShelfKey)
        {
            anim.SetBool("InteractShelf", true);
        } else
        {
            //playerInteract.SmoothCamExit(); // har inte du neckeln elr nåt så kan du inte öppna
        }
        
    }
    private void DrawerClose()
    {
        anim.SetBool("InteractShelf", false);
    }
}