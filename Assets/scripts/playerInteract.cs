using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float interactRange;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera mainCam;
    [Header("Camera Positions")] // lägg till felr positioner för interactables
    [SerializeField] private Transform ComputerCamPos; // cameraposition för datorn
    [SerializeField] private Transform PlayerCamPos; // Kamera återgå till spelaren efter interaction

    private Transform TargetCamPos = null;


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
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                isInteracting = true;
                LockCamera();
                WhatWasInteracted(hit.collider.gameObject); // skickar interactables gameObject till saken
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FreeCamera();
        }

        if (isInteracting && TargetCamPos != null)
        {
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, TargetCamPos.position, Time.deltaTime * 5f);
            mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, TargetCamPos.rotation, Time.deltaTime * 5f);
        }
        
        if (!isInteracting)
        {
            CamToPlayer();
        }
        
    }

    private void LockCamera() // namnet sägeer ganska mycket
    {
        movementScript.canMove = false;
        mainCam.GetComponent<cameraMovment>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void WhatWasInteracted(GameObject currentInteractable) // borde heta changeCamPos men orka
    {
        string currentInteractableTag = currentInteractable.tag;
        switch (currentInteractableTag)
        {
            default: Debug.Log("Forgot tag on interactable"); break;
            case "computer":
                Debug.Log("interactable tag: " + currentInteractableTag);
                TargetCamPos = ComputerCamPos; // ändra target pos beroende på interactionen, här ComputerCamPos
                
                break;
            // lägg till fler object/tag här
        }
    }

    private void FreeCamera()
    {
        isInteracting = false;
        TargetCamPos = null;
        movementScript.canMove = true;
        mainCam.GetComponent<cameraMovment>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    private void CamToPlayer()
    {
        mainCam.transform.position = PlayerCamPos.position; // återgå till original position
        mainCam.transform.rotation = PlayerCamPos.rotation;
    }

    
}
