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
    [SerializeField] private Transform LåsCamPos; // cameraposition f�r låset

    private Transform LastCamPos; // för att titta vilken cam pos var senast (ex veta om man går fårn shelf till player)

    [Header("Items")]
    [SerializeField] private bool hasScrewdriver = false;

    [SerializeField] private ShelfLogic ShelfLogic;
    [SerializeField] private PlayerUIScript PlayerUIScript;

    private bool CamIsOnTheMove = false;
    private Quaternion CamPosPreInteract;

    private bool isInteracting = false;
    private playerMovment movementScript;



    private void Start()
    {
        movementScript = GetComponent<playerMovment>();
        mainCam = Camera.main;
        ShelfLogic = FindFirstObjectByType<ShelfLogic>();
        PlayerUIScript = FindFirstObjectByType<PlayerUIScript>();
    }
    private void Update()
    {
        Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hit, interactRange, whatIsInteractable);
        Debug.DrawRay(mainCam.transform.position, mainCam.transform.forward * interactRange, Color.red);

        if (!isInteracting && hit.collider != null)
        {
            string interaction_text = hit.collider.tag;
            PlayerUIScript.TextToggle(true);
            PlayerUIScript.ChangeUIText(interaction_text, "E");


            if (Input.GetKeyDown(KeyCode.E) && !CamIsOnTheMove)
            {
                PlayerUIScript.TextToggle(false);
                isInteracting = true;
                LockCamera();
                WhatWasInteracted(hit.collider.gameObject); // skickar interactables gameObject till saken
            }

        }
        else
        {
            PlayerUIScript.TextToggle(false); // scenen måste ha PlayerUI prefab för att den ska fungera, annars skiter sig allt
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
                    //Ändra destroy till typ gå in i venten eller liknande
                    Destroy(currentInteractable);
                    SmoothCamExit();

                }
                else
                {
                    Debug.Log("You need a screwdriver to open this vent");

                }

                break;
            case "lock":
                StartCoroutine(MoveCameraPos(LåsCamPos));
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

    public void SmoothCamExit() // public så andra kan exita den
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
            float t = Mathf.SmoothStep(0, 1, elapsed / duration); // fr�ga inte, matte �r sv�rt
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
}