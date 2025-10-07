using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float interactRange;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera mainCam;
<<<<<<< HEAD
    [Header("Camera Positions")] // lägg till felr positioner för interactables
    [SerializeField] private Transform ComputerCamPos; // cameraposition för datorn
    [SerializeField] private Transform PlayerCamPos; // Kamera återgå till spelaren efter interaction
   

=======
    [Header("Camera Positions")] // lï¿½gg till felr positioner fï¿½r interactables
    [SerializeField] private Transform ComputerCamPos; // cameraposition fï¿½r datorn
    [SerializeField] private Transform PlayerCamPos; // Kamera ï¿½tergï¿½ till spelaren efter interaction
    [SerializeField] private Transform ShelfCamPos; // cameraposition fï¿½r hyllan
    [SerializeField] private Transform VentCamPos; // cameraposition fï¿½r ventilen
    [SerializeField] private Transform LÃ¥sCamPos; // cameraposition fï¿½r lÃ¥set

    private Transform LastCamPos; // fÃ¶r att titta vilken cam pos var senast (ex veta om man gÃ¥r fÃ¥rn shelf till player)

    [Header("Items")]
    [SerializeField] private bool hasScrewdriver = false;

    [SerializeField] private ShelfLogic ShelfLogic;
>>>>>>> main

    private bool CamIsOnTheMove = false;
    private Quaternion CamPosPreInteract;

    private bool isInteracting = false;
    private playerMovment movementScript;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip mouseClickSound;
    public AudioClip startupSound;


    private void Start()
    {
        movementScript = GetComponent<playerMovment>();
        mainCam = Camera.main;
        ShelfLogic = FindFirstObjectByType<ShelfLogic>();
    }
    private void Update()
    {
        Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, interactRange, whatIsInteractable);
        Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * interactRange, Color.red);

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

        if (isInteracting && Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
            // Här kan du lägga din befintliga kod som klickar på appar
        }

    }

    private void LockCamera() // namnet sï¿½geer ganska mycket
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

                StartCoroutine(MoveCameraPos(ComputerCamPos)); // ï¿½ndra ComputerCamPos beroende pï¿½ interactionen, hï¿½r ComputerCamPos
                break;

            case "shelf":
                LastCamPos = ShelfCamPos;
                StartCoroutine(MoveCameraPos(ShelfCamPos));
                if (ShelfLogic.HasShelfKey == true)
                {
                    ShelfLogic.DrawerOpen();
                }
                else
                {
                    SmoothCamExit();
                    Debug.Log("You are not capable of opening the drawer"); // man har inte nyckel
                }

                break;

            case "vent":
                StartCoroutine(MoveCameraPos(VentCamPos));
                if (hasScrewdriver)
                {
                    //Ã„ndra destroy till typ gÃ¥ in i venten eller liknande
                    Destroy(currentInteractable);
                    SmoothCamExit();

                }
                else
                {
                    Debug.Log("You need a screwdriver to open this vent");

                }

                break;
            case "lÃ¥s":
                StartCoroutine(MoveCameraPos(LÃ¥sCamPos));
                break;
            // lï¿½gg till fler object/tag hï¿½r
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
    
    public void SmoothCamExit() // public sÃ¥ andra kan exita den
    {

        StartCoroutine(MoveCameraPos(PlayerCamPos));
    }

    private void CamToPlayer()
    {
        mainCam.transform.position = PlayerCamPos.position; // ï¿½tergï¿½ till original position
        
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
            float t = Mathf.SmoothStep(0,1, elapsed/duration); // frï¿½ga inte, matte ï¿½r svï¿½rt
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

        /*if (LastCamPos == ShelfCamPos && target == PlayerCamPos)
        {
            ShelfLogic.DrawerClose();
        }*/
        

        CamIsOnTheMove = false;
    }

    private void PlayClickSound()
    {
        if (audioSource != null && mouseClickSound != null)
        {
            audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(mouseClickSound);
        }
    }


}
