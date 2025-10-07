using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIScript : MonoBehaviour
{

    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI keybindText;
    public GameObject UIPanel; // skrev nästan penis

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextToggle(false);
    }

    public void TextToggle(bool show)
    {
        UIPanel.SetActive(show);
    }

    public void ChangeUIText(string newInteraction, string newKeybind)
    {
        interactionText.text = newInteraction;
        keybindText.text = newKeybind;
    }
}
