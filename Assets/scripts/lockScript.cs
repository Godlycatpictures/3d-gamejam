using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class lockScript : MonoBehaviour
{
    public int num1, num2, num3, num4;
    public int correctNum1, correctNum2, correctNum3, correctNum4;
    public bool isUnlocked = false;
    public Canvas lockCanvas;
    private playerInteract playerInteractScript;
    [SerializeField] private TextMeshProUGUI num1Text;
    [SerializeField] private TextMeshProUGUI num2Text;
    [SerializeField] private TextMeshProUGUI num3Text;
    [SerializeField] private TextMeshProUGUI num4Text;



    public void addnum1()
    {
        addNUM(num1);
        num1Text.text = num1.ToString();

    }
    public void subnum1()
    {
        subNUM(num1);
        num1Text.text = num1.ToString();
    }
    public void addnum2()
    {
        addNUM(num2);
        num2Text.text = num2.ToString();
    }
    public void subnum2()
    {
        subNUM(num2);
        num2Text.text = num2.ToString();
    }
    public void addnum3()
    {
        addNUM(num3);
        num3Text.text = num3.ToString();
    }
    public void subnum3()
    {
        subNUM(num3);
        num3Text.text = num3.ToString();
    }
    public void addnum4()
    {
        addNUM(num4);
        num4Text.text = num4.ToString();
    }
    public void subnum4()
    {
        subNUM(num4);
        num4Text.text = num4.ToString();
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
            isUnlocked = true;
            lockCanvas.enabled = false;
            playerInteractScript = GameObject.Find("Player").GetComponent<playerInteract>();
            playerInteractScript.SmoothCamExit();

            Debug.Log("Unlocked");
        }
        else
        {
            Debug.Log("Incorrect");
        }
    }
}
