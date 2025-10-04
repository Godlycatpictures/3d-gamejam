using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float interactRange;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera mainCam;
    private bool isInteracting = false;
    private playerMovment movementScript;

    private void Start()
    {
        movementScript = GetComponent<playerMovment>();
        mainCam = Camera.main;
    }
    private void Update()
    {
        Physics.Raycast(orientation.position, orientation.forward, out RaycastHit hit, interactRange, whatIsInteractable);
        if (hit.collider != null)
        {
            Debug.Log("looking at " + hit.collider.name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                isInteracting = true;

            }

        }
        if (isInteracting)
        {
            movementScript.canMove = false;
            mainCam.GetComponent<cameraMovment>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isInteracting = false;
        }
        while (!isInteracting)
        {
            movementScript.canMove = true;
            mainCam.GetComponent<cameraMovment>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            break;
        }
        
    }
}
