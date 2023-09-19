using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startPanel;
    public Button startButton;

    [Space(6)]

    public Button restartButton;

    [Space(6)]

    public GameObject pausePanel;
    public Button pauseButton;
    public Button continueButton;
    public Button musicButton;
    public Button soundButton;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private GameManager gameManager;

    private void Awake()
    {
        startPanel.SetActive(true);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        SetMusicSprite();
        SetSoundSprite();

        StartPanel();
        LosePanel();
        PausePanel();
    }

    private void StartPanel()
    {
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(() =>
            {
                gameManager.NewGame();
                startPanel.SetActive(false);
            });
        }
    }

    private void LosePanel()
    {
        if (restartButton != null)
        {
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
            });
        }
    }

    private void PausePanel()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveAllListeners();
            pauseButton.onClick.AddListener(() =>
            {
                gameManager.PauseGame();
                pausePanel.SetActive(true);
            });
        }

        if (continueButton != null)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                pausePanel.SetActive(false);
                gameManager.UnPauseGame();
            });
        }

        if (musicButton != null)
        {
            musicButton.onClick.RemoveAllListeners();
            musicButton.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetFloat("MusicVolume") == 0)
                {
                    AudioManager.instance.OnMusic();
                    SetMusicSprite();
                }
                else
                {
                    AudioManager.instance.OffMusic();
                    SetMusicSprite();
                }
            });
        }

        if (soundButton != null)
        {
            soundButton.onClick.RemoveAllListeners();
            soundButton.onClick.AddListener(() =>
            {
                if (PlayerPrefs.GetFloat("SoundVolume") == 0)
                {
                    AudioManager.instance.OnSound();
                    SetSoundSprite();
                }
                else
                {
                    AudioManager.instance.OffSound();
                    SetSoundSprite();
                }
            });
        }
    }

    private void SetMusicSprite()
    {
        if (PlayerPrefs.GetFloat("MusicVolume") == 0)
        {
            musicButton.GetComponent<Image>().sprite = musicOffSprite;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = musicOnSprite;
        }
    }

    private void SetSoundSprite()
    {
        if (PlayerPrefs.GetFloat("SoundVolume") == 0)
        {
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
        }
    }
}
