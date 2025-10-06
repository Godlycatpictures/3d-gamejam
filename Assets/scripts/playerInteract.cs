using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float interactRange;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera mainCam;
    [Header("Camera Positions")] // l�gg till felr positioner f�r interactables
    [SerializeField] private Transform ComputerCamPos; // cameraposition f�r datorn
    [SerializeField] private Transform PlayerCamPos; // Kamera �terg� till spelaren efter interaction
    [SerializeField] private Transform ShelfCamPos; // cameraposition f�r hyllan
    [SerializeField] private Transform VentCamPos; // cameraposition f�r ventilen

    [Header("Items")]
    [SerializeField] private bool hasScrewdriver = false;

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
        Debug.DrawRay(orientation.position, orientation.forward * interactRange, Color.red);
        
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

    private void LockCamera() // namnet s�geer ganska mycket
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

                StartCoroutine(MoveCameraPos(ComputerCamPos)); // �ndra ComputerCamPos beroende p� interactionen, h�r ComputerCamPos
                break;

            case "shelf":

                StartCoroutine(MoveCameraPos(ShelfCamPos)); // h�r f�r du l�gga in en ny cameraposition
                break;

            case "vent":
                StartCoroutine(MoveCameraPos(VentCamPos));
                if (hasScrewdriver)
                {
                    //Ändra destroy till typ gå in i venten eller liknande
                    Destroy(currentInteractable);
                    SmoothCamExit();
                   
                }
                else
                {
                    Debug.Log("You need a screwdriver to open this vent");

                }
                
                break;
            // l�gg till fler object/tag h�r
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
        mainCam.transform.position = PlayerCamPos.position; // �terg� till original position
        
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
            float t = Mathf.SmoothStep(0,1, elapsed/duration); // fr�ga inte, matte �r sv�rt
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
