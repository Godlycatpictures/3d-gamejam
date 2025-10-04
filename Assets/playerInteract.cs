using UnityEngine;

public class playerInteract : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsInteractable;
    [SerializeField] private float interactRange;
    [SerializeField] private Transform orientation;
    private playerMovment movementScript;
    private void Start()
    {
        movementScript = GetComponent<playerMovment>();
    }
    private void Update()
    {
        Physics.Raycast(orientation.position, orientation.forward, out RaycastHit hit, interactRange, whatIsInteractable);
        if (hit.collider != null)
        {
            Debug.Log("looking at " + hit.collider.name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                movementScript.canMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }

        }

        movementScript.canMove = true;


    }
}
