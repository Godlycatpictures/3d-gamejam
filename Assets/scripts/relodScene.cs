using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class relodScene : MonoBehaviour
{
    [SerializeField] private Canvas transitionCanvas;
    [SerializeField] private Image transitionImage;
    [SerializeField] private float transitionSpeed = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ✅ Give player the trophy
            TrophyManager.Instance.hasTrophy = true;
            TrophyManager.Instance.UpdateTrophyState();

            // Fade and reload
            StartCoroutine(FadetoBlack(1f));
            StartCoroutine(DelayedReload(1f));
        }
    }

    private IEnumerator FadetoBlack(float targetAlpha)
    {
        transitionCanvas.gameObject.SetActive(true);
        Color color = transitionImage.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < transitionSpeed)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / transitionSpeed;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            transitionImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        transitionImage.color = color;

        if (targetAlpha == 0f)
            transitionCanvas.gameObject.SetActive(false);
    }

    private IEnumerator DelayedReload(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
