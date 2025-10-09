using System.Collections;
using UnityEngine;
using TMPro;
public class PasskeyManager : MonoBehaviour
{
    [SerializeField] private string inputText;
    [SerializeField] private string correctPasskey = "0545";
    [SerializeField] private GameObject correct, incorrect, destroyable, unlocked;
    public bool passkeyCorrect;
    public TMP_InputField inputField;
    [SerializeField] private ShelfLogic ShelfLogic;

    private void Start()
    {
        ShelfLogic = FindFirstObjectByType<ShelfLogic>();
        inputField.onEndEdit.AddListener(ReadStringInput);
    }
    public void ReadStringInput(string s)
    {
        inputText = s.ToLower();
        Debug.Log(s);
        CheckPassword(s.ToLower());
    }
    public void CheckPassword(string input)
    {
        if (input == correctPasskey.ToLower())
        {
            correct.SetActive(true);
            incorrect.SetActive(false);
            StartCoroutine(Quarantine(4));
        }
        else
        {
            incorrect.SetActive(true);
            Debug.Log("Passkey is incorrect.");
        }
    }
    IEnumerator Quarantine(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        passkeyCorrect = true;
        ShelfLogic.HasShelfKey = true;
        Debug.Log("Passkey is correct.");
        Destroy(destroyable);
        unlocked.SetActive(true);
    }
}
