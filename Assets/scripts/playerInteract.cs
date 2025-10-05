using System.Collections;
using Unity.Mathematics;
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

    private bool CamIsOnTheMove = false;
    private Quaternion CamPosPreInteract;

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
            
            if (Input.GetKeyDown(KeyCode.E) && !CamIsOnTheMove)
            {
                isInteracting = true;
                LockCamera();
                WhatWasInteracted(hit.collider.gameObject); // skickar interactables gameObject till saken
            }

        }

        if (isInteracting && Input.GetKeyDown(KeyCode.Escape) && !CamIsOnTheMove)
        {
            SmoothCamExit();
            
        }



        if (!isInteracting && !CamIsOnTheMove)
        {
            CamToPlayer();
        }


    }

    private void LockCamera() // namnet sägeer ganska mycket
    {
        CamPosPreInteract = mainCam.transform.rotation; // hatar
        movementScript.canMove = false;
        mainCam.GetComponent<cameraMovment>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void WhatWasInteracted(GameObject currentInteractable) // borde heta changeCamPos men orka
    {
        string currentInteractableTag = currentInteractable.tag;
        Debug.Log("interactable tag: " + currentInteractableTag);
        switch (currentInteractableTag)
        {
            default: Debug.Log("Forgot tag on interactable"); break;
            case "computer":
                
                StartCoroutine(MoveCameraPos(ComputerCamPos)); // ändra ComputerCamPos beroende på interactionen, här ComputerCamPos
                break;
            // lägg till fler object/tag här
        }
    }

    private void FreeCamera()
    {
        isInteracting = false;
        movementScript.canMove = true;
        mainCam.GetComponent<cameraMovment>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    
    private void SmoothCamExit()
    {

        StartCoroutine(MoveCameraPos(PlayerCamPos));
    }

    private void CamToPlayer()
    {
        mainCam.transform.position = PlayerCamPos.position; // återgå till original position
        
    }

    private IEnumerator MoveCameraPos(Transform target)
    {
        CamIsOnTheMove = true;

        float duration = 1f;
        float elapsed = 0f;

        Vector3 CamStartPos = mainCam.transform.position;
        Quaternion CamStartRot = mainCam.transform.rotation;

        Quaternion targetRot = target == PlayerCamPos ? CamPosPreInteract : target.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0,1, elapsed/duration); // fråga inte, matte är svårt
            mainCam.transform.position = Vector3.Lerp(CamStartPos, target.position, t);
            mainCam.transform.rotation = Quaternion.Lerp(CamStartRot, targetRot, t);
            yield return null;
        }
        mainCam.transform.position = target.position;
        mainCam.transform.rotation = targetRot;

        if (target == PlayerCamPos)
        {
            FreeCamera();
        }

        CamIsOnTheMove = false;
    }
}
