using UnityEngine;

public class TrashcanManager : MonoBehaviour
{
    [SerializeField] GameObject root, document, system;
    public void ActivateUI(GameObject gameObject)
    {
        DisableAllUI();
        gameObject.SetActive(true);
    }
    void DisableAllUI()
    {
        root.SetActive(false);
        document.SetActive(false);
        system.SetActive(false);
    }
}
