using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject UIMenu;
    public GameObject MainMenu;
    public GameObject ShopMenu;
    [Header("Score")]
    public Text scoreText;
    public Text tapText;
    public GameObject newHighScoreText;
    public Text HighScoreAmount;
    public Text topHatAmount;
    public Text capsuleAmount;
    public Text coinAmount;
    public Text lastRunCoins;
    [Header("Buttons")]
    public Button pauseButton;
    public Button playButton;
    public Button toMainButton;
    public Button toPlayButton;
    public Button toShopButton;
    public Button buyTopHatButton;
    public Button buyCapsuleButton;
    public Button equipTopHatButton;
    public Button equipCapsuleButton;

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
        if (toShopButton)
            toShopButton.onClick.AddListener(OpenShop);
        if (buyTopHatButton)
            buyTopHatButton.onClick.AddListener(UnlockTopHat);
        if (buyCapsuleButton)
            buyCapsuleButton.onClick.AddListener(UnlockCapsule);
        if (equipTopHatButton)
            equipTopHatButton.onClick.AddListener(ScoreHolder.Instance.EquipTopHat);
        if (equipCapsuleButton)
            equipCapsuleButton.onClick.AddListener(ScoreHolder.Instance.EquipCapsule);
        if (HighScoreAmount)
            HighScoreAmount.text = FindAnyObjectByType<ScoreHolder>().highScore.ToString() + " Points";
        if (topHatAmount)
            topHatAmount.text = ScoreHolder.Instance.topHatAmount.ToString() + "Coins";
        if (capsuleAmount)
            capsuleAmount.text = ScoreHolder.Instance.capsuleAmount.ToString() + "Coins";
        if (coinAmount)
            coinAmount.text = ScoreHolder.Instance.coins.ToString() + " Coins";
        if (lastRunCoins)
            lastRunCoins.text = ScoreHolder.Instance.coinsCollected.ToString() + " Coins";
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

        if (topHatAmount)
            ToggleTopHat();
        if (capsuleAmount)
            ToggleCapsule();
    }

    private void UnlockTopHat()
    {
        if(ScoreHolder.Instance.coins >= ScoreHolder.Instance.topHatAmount)
        {
            ScoreHolder.Instance.PurchasedTopHat();
            ToggleTopHat();
        }
    }

    private void UnlockCapsule()
    {
        if (ScoreHolder.Instance.coins >= ScoreHolder.Instance.capsuleAmount)
        {
            ScoreHolder.Instance.PurchasedCapsule();
            ToggleCapsule();
        }
    }

    private void OpenShop()
    {
        MainMenu.SetActive(false);
        ShopMenu.SetActive(true);
    }
    private void CloseShop()
    {
        MainMenu.SetActive(true);
        ShopMenu.SetActive(false);
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
        if (coinAmount)
            coinAmount.text = FindAnyObjectByType<ScoreHolder>().coins.ToString() + " Coins";
        if (topHatAmount)
            ToggleTopHat();
        if (capsuleAmount)
            ToggleCapsule();
        if (HighScoreAmount)
            HighScoreAmount.text = FindAnyObjectByType<ScoreHolder>().highScore.ToString() + " Points";
    }

    private void ToggleTopHat()
    {
        if (ScoreHolder.Instance.purchasedTopHat)
        {
            buyTopHatButton.gameObject.SetActive(false);
            equipTopHatButton.gameObject.SetActive(true);
            if (ScoreHolder.Instance.equipTopHat == false)
            {
                topHatAmount.text = "Equip Me?";
            }
            else
            {
                topHatAmount.text = "Unequip Me?";
            }
        }
    }

    private void ToggleCapsule()
    {
        if (ScoreHolder.Instance.purchasedCapsule)
        {
            buyCapsuleButton.gameObject.SetActive(false);
            equipCapsuleButton.gameObject.SetActive(true);
            if (ScoreHolder.Instance.becomeCapsule == false)
            {
                capsuleAmount.text = "Equip Me?";
            }
            else
            {
                capsuleAmount.text = "Unequip Me?";
            }
        }
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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            CloseShop();
            return;
        }

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu scene");
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Play scene");
    }

    public void GameOver()
    {
        ScoreHolder.Instance.SetScore();
        ScoreHolder.Instance.SetCoins();
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
