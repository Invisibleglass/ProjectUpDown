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
    [Header("Buttons")]
    public Button pauseButton;
    public Button playButton;
    public Button toMainButton;
    public Button toPlayButton;

    // Start is called before the first frame update
    void Start()
    {
        if(pauseButton)
            pauseButton.onClick.AddListener(PauseGame);
        if(playButton)
            playButton.onClick.AddListener(ResumeGame);
        if (toMainButton)
            toMainButton.onClick.AddListener(ToMainMenu);
        if (toPlayButton)
            toPlayButton.onClick.AddListener(StartGame);
        if (scoreText)
            scoreText.text = FindObjectOfType<GameManager>().score.ToString() + " Points";
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText)
        {
            scoreText.text = FindObjectOfType<GameManager>().score.ToString() + " Points";
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        UIMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    private void ResumeGame()
    {
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

    public void TapTextActive()
    {
        tapText.text = "IT WORKED!!!";
    }
    public void TapTextDeactive()
    {
        tapText.text = "";
    }
}
