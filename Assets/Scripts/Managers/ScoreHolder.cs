using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreHolder : MonoBehaviour
{
    [HideInInspector]
    public float score = 0;
    [HideInInspector]
    public float highScore = 0;
    [HideInInspector]
    public bool newHighScoreBool = false;
    public static ScoreHolder Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver scene"))
        {
            FindObjectOfType<UIManager>().NewHighScore();
        }
    }

    public void SetScore()
    {
        score = FindObjectOfType<GameManager>().score;
        if (score >= highScore)
        {
            highScore = score;
            newHighScoreBool = true;
        }
    }
}
