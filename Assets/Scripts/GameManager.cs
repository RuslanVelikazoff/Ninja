using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Text recordText;

    public GameObject losePanel;

    private int score;

    private Blade blade;
    private Spawner spawner;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();

        blade.enabled = false;
        spawner.enabled = false;
        scoreText.enabled = false;
    }

    public void NewGame()
    {
        score = 0;
        scoreText.text = score.ToString();

        scoreText.enabled = true;
        blade.enabled = true;
        spawner.enabled = true;

        Time.timeScale = 1;
    }

    public void LoseGame()
    {
        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            recordText.text = "Record: " + PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            recordText.text = "Record: " + PlayerPrefs.GetInt("HighScore");
        }

        losePanel.SetActive(true);

        scoreText.enabled = false;
        blade.enabled = false;
        spawner.enabled = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        scoreText.enabled = false;
        blade.enabled = false;
        spawner.enabled = false;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        scoreText.enabled = true;
        blade.enabled = true;
        spawner.enabled = true;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
