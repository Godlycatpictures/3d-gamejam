using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float interactRange;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform ComputerCamPos; // cameraposition för datorn
    [SerializeField] private Transform PlayerCamPos; // Kamera återgå till spelaren efter interaction

    private bool isInteracting = false;
    private playerMovment movementScript;

    private void Start()
    {
        movementScript = GetComponent<playerMovment>();
        mainCam = Camera.main;
    }
    private void FixedUpdate()
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
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, ComputerCamPos.position, Time.deltaTime * 5f);
            mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, ComputerCamPos.rotation, Time.deltaTime * 5f);
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
            mainCam.transform.position = PlayerCamPos.position;
            mainCam.transform.rotation = PlayerCamPos.rotation;
            break;
        }
        
    }
}
