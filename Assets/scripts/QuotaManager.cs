using UnityEngine;

public class QuotaManager : MonoBehaviour
{
    [SerializeField] int requiredTasks;
    [SerializeField] float givenTime;
    int tasksCompleted = 0;
    float currentTime = 0;
    void Start()
    {
        currentTime = givenTime;
    }
    public void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            if (tasksCompleted >= requiredTasks)
            {
                Debug.Log("Quota reached!");
            }
            else
            {
                Debug.Log("Quota not reached! Not enough tasks completed.");
            }
            tasksCompleted = 0;
            currentTime = givenTime;
        }
    }
    public void CompleteTask()
    {
        tasksCompleted++;
        Debug.Log("Task completed!");
    }
}
