using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuotaManager : MonoBehaviour
{
    [SerializeField] int requiredTasks;
    [SerializeField] float givenTime;
    [SerializeField] Slider TasksSlider;
    [SerializeField] TMPro.TextMeshProUGUI TasksText;
    int tasksCompleted = 0;
    float currentTime = 0;
    void Start()
    {
        currentTime = givenTime;
        UpdateUI();
    }
    public void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            if (tasksCompleted >= requiredTasks)
            {
                Debug.Log("Quota reached!");
                Win();
            }
            else
            {
                Debug.Log("Quota not reached! Not enough tasks completed.");
                Lose();
            }
            tasksCompleted = 0;
            currentTime = givenTime;
        }
    }
    public void CompleteTask()
    {
        tasksCompleted++;
        Debug.Log("Task completed!");
        UpdateUI();
    }
    public void UpdateUI()
    {
        TasksSlider.maxValue = requiredTasks;
        TasksSlider.value = tasksCompleted;
        if (tasksCompleted < requiredTasks)
        {
            TasksText.text = tasksCompleted + " / " + requiredTasks;
        }
        else
        {
            TasksText.text = "All tasks completed!";
        }
    }
    public void Win()
    {
        Debug.Log("You win!");
    }
    public void Lose()
    {
        Debug.Log("You lose!");
    }
}
