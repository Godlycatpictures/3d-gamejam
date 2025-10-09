using UnityEngine;

public class MenuWindowManager : MonoBehaviour
{
    [SerializeField] GameObject startGameUI, optionsUI, quitGameUI;
    public void EnableUI(string uiName)
    {
        DisableAllUI();
        switch (uiName.ToLower())
        {
            case "startgame":
                startGameUI.SetActive(true);
                break;
            case "controls":
                optionsUI.SetActive(true);
                break;
            case "quitgame":
                quitGameUI.SetActive(true);
                break;
        }
    }
    public void DisableUI(string uiName)
    {
        switch (uiName.ToLower())
        {
            case "startgame":
                startGameUI.SetActive(false);
                break;
            case "controls":
                optionsUI.SetActive(false);
                break;
            case "quitgame":
                quitGameUI.SetActive(false);
                break;
        }

    }
    public void DisableAllUI()
    {
        startGameUI.SetActive(false);
        optionsUI.SetActive(false);
        quitGameUI.SetActive(false);
    }
}
