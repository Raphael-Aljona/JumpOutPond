using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public PlayerScore playerScore;
    public int score;
    public GameObject gameCanvas;

    public Text scoreText;

    public CanvasGroup canvasGroup;
    private float fadeDuration = 2f;
    private void OnEnable()
    {

        playerScore = FindObjectOfType<PlayerScore>();

        score = playerScore.score;

        scoreText.text = "Score: " + score.ToString();

        if(canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            StartCoroutine(FadeIn());
        }
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        gameCanvas.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }

    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration) {

            elapsed += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;

        }
        Time.timeScale = 0f;
    }
}   
