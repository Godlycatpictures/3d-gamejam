using UnityEngine;

public class NotebookUI : MonoBehaviour
{
    [SerializeField] GameObject notebookUI;
    public void EnableNotebookUI()
    {
        notebookUI.SetActive(true);
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
    }
    public void DisableNotebookUI()
    {
        notebookUI.SetActive(false);
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
}
