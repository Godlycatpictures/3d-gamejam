using UnityEngine;

public class timerManager : MonoBehaviour
{
    public float timer = 0f;
    public bool isRunning = false;

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
    public void ResetTimer()
    {
        timer = 0f;
    }
    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
        }   
        // There are 600 seconds in 10 minutes (10 * 60 = 600)
        if (timer >= 600f)
        {
            isRunning = false;
            Debug.Log("Timer reached 10 minutes!");
            //if qutoa = klar + man är på plats = ingen punishment else punishment
            //reset timer
            //ny dag etc
        }
    }

}
