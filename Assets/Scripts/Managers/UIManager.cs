using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject UIMenu;
    [Header("Score")]
    public Text scoreText;
    public Text tapText;
    public GameObject newHighScoreText;
    public Text HighScoreAmount;
    [Header("Buttons")]
    public Button pauseButton;
    public Button playButton;
    public Button toMainButton;
    public Button toPlayButton;

    // Start is called before the first frame update
    void Start()
    {
        if (pauseButton)
            pauseButton.onClick.AddListener(PauseGame);
        if (playButton)
            playButton.onClick.AddListener(ResumeGame);
        if (toMainButton)
            toMainButton.onClick.AddListener(ToMainMenu);
        if (toPlayButton)
            toPlayButton.onClick.AddListener(StartGame);
        if (HighScoreAmount)
            HighScoreAmount.text = FindAnyObjectByType<ScoreHolder>().highScore.ToString() + " Points";
        if (scoreText)
        {
            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Play Scene"))
            {
                scoreText.text = FindObjectOfType<GameManager>().score.ToString() + " Points";
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver Scene"))
            {
                scoreText.text = FindObjectOfType<ScoreHolder>().score.ToString() + " Points";
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Play scene"))
        {
            FindObjectOfType<ScoreHolder>().newHighScoreBool = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Play Scene"))
            {
                scoreText.text = FindObjectOfType<GameManager>().score.ToString() + " Points";
            }
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver Scene"))
            {
                scoreText.text = FindObjectOfType<ScoreHolder>().score.ToString() + " Points";
            }
        }
        if (HighScoreAmount)
            HighScoreAmount.text = FindAnyObjectByType<ScoreHolder>().highScore.ToString() + " Points";
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        FindAnyObjectByType<BasePlayerController>().OnDisable();
        UIMenu.SetActive(false);
        pauseMenu.SetActive(true);
        GameObject.FindWithTag("MusicToggle").GetComponent<ToggleAudio>().SwapColorMusic();
        GameObject.FindWithTag("EffectsToggle").GetComponent<ToggleAudio>().SwapColorEffects();
    }
    private void ResumeGame()
    {
        FindObjectOfType<BasePlayerController>().OnEnable();
        pauseMenu.SetActive(false);
        UIMenu.SetActive(true);
        Time.timeScale = 1;
    }

    private void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu scene");
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Play scene");
    }

    public void GameOver()
    {
        FindObjectOfType<ScoreHolder>().SetScore();
        SceneManager.LoadScene("GameOver scene");
    }

    public void NewHighScore()
    {
        if (FindAnyObjectByType<ScoreHolder>().newHighScoreBool == true)
        {
            newHighScoreText.SetActive(true);
        }
    }

    public void TapTextActive()
    {
        tapText.text = "IT WORKED!!!";
    }
    public void TapTextDeactive()
    {
        tapText.text = "";
    }
}
