using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryScript : MonoBehaviour
{
   public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
}
