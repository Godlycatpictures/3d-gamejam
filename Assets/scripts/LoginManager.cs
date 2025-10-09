using System.Collections;
using UnityEngine;
using TMPro;
public class LoginManager : MonoBehaviour
{
    [SerializeField] private string inputText;
    [SerializeField] private string correctPassword = "opensesame";
    [SerializeField] private GameObject loggingInText, incorrectPasswordText, loggedIn;
    public AudioSource audioSource;
    public AudioClip loginSound;
    public AudioClip loginFailSound;
    public TMP_InputField inputField;

    public bool stopLogic;

    private void Start()
    {
        inputField.onEndEdit.AddListener(ReadStringInput);
    }
    public void ReadStringInput(string s)
    {
        inputText = s.ToLower();
        Debug.Log(s);
        if (s != "")
        {
            CheckPassword(s.ToLower());
        }
    }
    public void CheckPassword(string input)
    {
        if (!stopLogic)
        {
            if (input == correctPassword.ToLower())
            {
                stopLogic = true;
                loggingInText.SetActive(true);
                incorrectPasswordText.SetActive(false);
                Debug.Log("Password is correct.");
                StartCoroutine(Quarantine(4));

            }
            else
            {
                audioSource.PlayOneShot(loginFailSound);
                incorrectPasswordText.SetActive(true);
                Debug.Log("Password is incorrect.");

            }
        }
    }
    IEnumerator Quarantine(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        audioSource.PlayOneShot(loginSound);
        Destroy(this.gameObject);
        loggedIn.SetActive(true);
    }
}
