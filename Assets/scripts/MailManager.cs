using UnityEngine;

public class MailManager : MonoBehaviour
{
    [SerializeField] GameObject mail1, mail2, mail3;
    public void OpenMail(string name)
    {
        CloseAllMail();
        switch (name.ToLower())
        {
            case "mail1":
                mail1.SetActive(true);
                break;
            case "mail2":
                mail2.SetActive(true);
                break;
            case "mail3":
                mail3.SetActive(true);
                break;
        }
    }
    void CloseAllMail()
    {
        mail1.SetActive(false);
        mail2.SetActive(false);
        mail3.SetActive(false);
    }
}
