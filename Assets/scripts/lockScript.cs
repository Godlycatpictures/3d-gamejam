using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class lockScript : MonoBehaviour
{
    public int num1, num2, num3, num4;
    public int correctNum1 = 1, correctNum2 = 9, correctNum3 = 8, correctNum4 = 4;
    public bool isUnlocked = false;
    public Canvas lockCanvas;
    private playerInteract playerInteractScript;

    [SerializeField] private GameObject Door, incorrectLight;
    [SerializeField] private TextMeshProUGUI num1Text;
    [SerializeField] private TextMeshProUGUI num2Text;
    [SerializeField] private TextMeshProUGUI num3Text;
    [SerializeField] private TextMeshProUGUI num4Text;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip unlockSound;



    public void addnum1()
    {
        num1 = addNUM(num1);

        num1Text.text = num1.ToString();
        checkLock();

    }
    public void subnum1()
    {
        num1 = subNUM(num1);
        num1Text.text = num1.ToString();
        checkLock();
    }
    public void addnum2()
    {
        num2 = addNUM(num2);
        num2Text.text = num2.ToString();
        checkLock();
    }
    public void subnum2()
    {
        num2 = subNUM(num2);
        num2Text.text = num2.ToString();
        checkLock();
    }
    public void addnum3()
    {
        num3 = addNUM(num3);
        num3Text.text = num3.ToString();
        checkLock();
    }
    public void subnum3()
    {
        num3 = subNUM(num3);
        num3Text.text = num3.ToString();
        checkLock();
    }
    public void addnum4()
    {
        num4 = addNUM(num4);
        num4Text.text = num4.ToString();
        checkLock();
    }
    public void subnum4()
    {
        num4 = subNUM(num4);
        num4Text.text = num4.ToString();
        checkLock();
    }


    private int addNUM(int num)
    {
        num++;
        if (num > 9)
        {
            num = 0;
        }
        return num;
    }
    private int subNUM(int num)
    {
        num--;
        if (num < 0)
        {
            num = 9;
        }
        return num;
    }
    public void checkLock()
    {
        if (num1 == correctNum1 && num2 == correctNum2 && num3 == correctNum3 && num4 == correctNum4)
        {
            Destroy(incorrectLight);
            audioSource.PlayOneShot(unlockSound);
            StartCoroutine(UnlockLock(2f));
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
    public IEnumerator UnlockLock(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isUnlocked = true;

        playerInteractScript = GameObject.Find("Player").GetComponent<playerInteract>();
        playerInteractScript.SmoothCamExit();
        Door.transform.position = new Vector3(0f, -10f, 0f);
        Debug.Log("Unlocked");
        lockCanvas.enabled = false;
        this.gameObject.SetActive(false);
    }
}
