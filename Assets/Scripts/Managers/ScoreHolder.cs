using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ScoreHolder : MonoBehaviour
{
    [HideInInspector]
    public float score = 0;
    [HideInInspector]
    public float highScore = 0;
    [HideInInspector]
    public bool newHighScoreBool = false;
    [HideInInspector]
    public bool equipTopHat;
    [HideInInspector]
    public bool becomeCapsule;
    [HideInInspector]
    public bool purchasedTopHat;
    [HideInInspector]
    public bool purchasedCapsule;
    [HideInInspector]
    public int coins;
    [HideInInspector]
    public int coinsCollected = 0;
    [HideInInspector]
    public int topHatAmount = 0;
    [HideInInspector]
    public int capsuleAmount = 0;

    public static ScoreHolder Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        // Load the player data on Awake
        Debug.Log("LoadedData");
        LoadPlayerData();

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            SetPlayerLook();
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver scene"))
        {
            FindObjectOfType<UIManager>().NewHighScore();
            LoadPlayerData();
        }
    }

    public void SetScore()
    {
        score = FindObjectOfType<GameManager>().score;
        if (score >= highScore)
        {
            highScore = score;
            newHighScoreBool = true;

            // Update the player's data and save it instantly
            PlayerData playerData = new PlayerData();
            playerData.highScore = highScore;

            SaveData(playerData);
            StartCoroutine(DelayedLoadPlayerData());
        }
    }

    public void SetCoins()
    {
        coinsCollected = FindObjectOfType<GameManager>().coinsCollected;
    }

    // Save the player's coin count using JSON serialization
    private static void SaveData(PlayerData playerData)
    {
        string data = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", data);
    }

    // Load the player's coin count using JSON deserialization
    private void LoadPlayerData()
    {
        PlayerData playerData = LoadPlayerDataFromDisk();
        highScore = playerData.highScore;
        equipTopHat = playerData.equipTopHat;
        becomeCapsule = playerData.equipCapsule;
        coins = playerData.coinCount;
        purchasedTopHat = playerData.purchasedTopHat;
        purchasedCapsule = playerData.purchasedCapsule;
    }


    private static PlayerData LoadPlayerDataFromDisk()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PlayerData>(data);
        }
        else
        {
            // Return a new PlayerData instance if the file doesn't exist
            return new PlayerData();
        }
    }

    private void SetPlayerLook()
    {
        LoadPlayerData();

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if(equipTopHat)
        {
            Transform TopHat = Player.transform.Find("TopHat");
            TopHat.gameObject.SetActive(true);
        }
        else
        {
            Transform TopHat = Player.transform.Find("TopHat");
            TopHat.gameObject.SetActive(false);
        }

        if(becomeCapsule)
        {
            Transform Capsule = Player.transform.Find("Capsule");
            Capsule.gameObject.SetActive(true);
            Player.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Transform Capsule = Player.transform.Find("Capsule");
            Capsule.gameObject.SetActive(false);
            Player.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void PurchasedTopHat()
    {
        // Load existing player data
        PlayerData playerData = LoadPlayerDataFromDisk();

        playerData.purchasedTopHat = true;
        playerData.equipTopHat = true;
        playerData.coinCount = playerData.coinCount - topHatAmount;

        SaveData(playerData);
        StartCoroutine(DelayedLoadPlayerData());
    }

    public void PurchasedCapsule()
    {
        // Load existing player data
        PlayerData playerData = LoadPlayerDataFromDisk();

        playerData.purchasedCapsule = true;
        playerData.equipTopHat = true;
        playerData.coinCount = playerData.coinCount - capsuleAmount;

        SaveData(playerData);
        StartCoroutine(DelayedLoadPlayerData());
    }

    public void EquipTopHat()
    {
        if (equipTopHat == false)
        {
            // Load existing player data
            PlayerData playerData = LoadPlayerDataFromDisk();

            playerData.equipTopHat = true;

            SaveData(playerData);
            StartCoroutine(DelayedLoadPlayerData());
        }
        else
        {
            // Load existing player data
            PlayerData playerData = LoadPlayerDataFromDisk();

            playerData.equipTopHat = false;

            SaveData(playerData);
            StartCoroutine(DelayedLoadPlayerData());
        }
    }

    public void EquipCapsule()
    {
        if (becomeCapsule == false)
        {
            // Load existing player data
            PlayerData playerData = LoadPlayerDataFromDisk();

            playerData.equipCapsule = true;

            SaveData(playerData);
            StartCoroutine(DelayedLoadPlayerData());
        }
        else
        {
            // Load existing player data
            PlayerData playerData = LoadPlayerDataFromDisk();

            playerData.equipCapsule = false;

            SaveData(playerData);
            StartCoroutine(DelayedLoadPlayerData());
        }
    }

    private IEnumerator DelayedLoadPlayerData()
    {
        // Wait for the end of the frame
        yield return null;

        // Load the player data on the next frame
        LoadPlayerData();
        SetPlayerLook();
    }
}
