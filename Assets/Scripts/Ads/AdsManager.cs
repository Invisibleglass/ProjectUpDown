using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-2)]
[RequireComponent(typeof(RewardAd), typeof(BannerAd), typeof(InterstitialAd))]
public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public string AndroidGameID;
    public string iOSGameID;
    public bool testMode;

    [SerializeField] private Button adButton;

    private InterstitialAd interstitialAd;
    public InterstitialAd InterstitialAd => interstitialAd;

    private BannerAd bannerAd;
    public BannerAd BannerAd => bannerAd;

    private RewardAd rewardAd;
    public RewardAd RewardAd => rewardAd;

    private static AdsManager _instance = null;

    public static AdsManager instance
    {
        get => _instance;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;

        interstitialAd = GetComponent<InterstitialAd>();
        bannerAd = GetComponent<BannerAd>();
        rewardAd = GetComponent<RewardAd>();

        if (adButton)
            adButton.onClick.AddListener(PlayAd);

        string gameID = iOSGameID;
        if (Application.platform == RuntimePlatform.Android)
            gameID = AndroidGameID;

        if (string.IsNullOrEmpty(gameID))
            throw new InvalidDataException("No Game ID set");

        Advertisement.Initialize(gameID, testMode, this);
    }

    private void PlayAd()
    {
        //Play ad and if ad completed double coins
        RewardAd.LoadAd();
    }

    public void OnInitializationComplete()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            BannerAd.LoadBanner();
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new System.NotImplementedException();
    }
}