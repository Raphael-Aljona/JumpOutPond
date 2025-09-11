using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public PlayerScore playerScore;
    public int score;
    public GameObject gameCanvas;

    public Text scoreText;
    private void OnEnable()
    {

        playerScore = FindObjectOfType<PlayerScore>();

        score = playerScore.score;

        scoreText.text = "Score: " + score.ToString();
        //Debug.Log(score);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        gameCanvas.SetActive(true);
        SceneManager.LoadScene("SampleScene");
    }
}
