using System.Collections;
using UnityEngine;
using TMPro;
public class LoginManager : MonoBehaviour
{
    [SerializeField] private string inputText;
    [SerializeField] private string correctPassword = "opensesame";
    [SerializeField] private GameObject loggingInText, incorrectPasswordText, loggedIn;
    public TMP_InputField inputField;

    private void Start()
    {
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
        if (input == correctPassword.ToLower())
        {
            loggingInText.SetActive(true);
            incorrectPasswordText.SetActive(false);
            Debug.Log("Password is correct.");
            StartCoroutine(Quarantine(4));
        }
        else
        {
            incorrectPasswordText.SetActive(true);
            Debug.Log("Password is incorrect.");
        }
    }
    IEnumerator Quarantine(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(this.gameObject);
        loggedIn.SetActive(true);
    }
}
