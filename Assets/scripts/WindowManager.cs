using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField] GameObject notebookUI;
    [SerializeField] GameObject mailUI;
    [SerializeField] GameObject taskUI;
    [SerializeField] GameObject trashcanUI;
    [SerializeField] GameObject myPcUI;
    [SerializeField] GameObject passwordHintUI;
    public void EnableUI(string uiName)
    {
        DisableAllUI();
        switch (uiName.ToLower())
        {
            case "notebook":
                notebookUI.SetActive(true);
                break;
            case "mail":
                mailUI.SetActive(true);
                break;
            case "task":
                taskUI.SetActive(true);
                break;
            case "trashcan":
                trashcanUI.SetActive(true);
                break;
            case "myPc":
                myPcUI.SetActive(true);
                break;
            case "passwordhint":
                passwordHintUI.SetActive(true);
                break;
        }
    }
    public void DisableUI(string uiName)
    {
        switch (uiName.ToLower())
        {
            case "notebook":
                notebookUI.SetActive(false);
                break;
            case "mail":
                mailUI.SetActive(false);
                break;
            case "task":
                taskUI.SetActive(false);
                break;
            case "trashcan":
                trashcanUI.SetActive(false);
                break;
            case "myPc":
                myPcUI.SetActive(false);
                break;
            case "passwordhint":
                passwordHintUI.SetActive(false);
                break;
        }

    }
    public void DisableAllUI()
    {
        notebookUI.SetActive(false);
        mailUI.SetActive(false);
        taskUI.SetActive(false);
        trashcanUI.SetActive(false);
        myPcUI.SetActive(false);
        passwordHintUI.SetActive(false);
    }
}
