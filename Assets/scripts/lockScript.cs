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
}
