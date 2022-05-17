using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] bool testMode = true;
    public static AdManager Instance;

#if UNITY_ANDROID
    string gameId = "4718141";
#elif UNITY_IOS
    string gameId = "4718140";
#endif
    GameOverHandler gameOverHandler;

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // This will call all methods in IUnityAdsListener
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameId, testMode);
        }
    }

    public void ShowAd(GameOverHandler gameOverHandler)
    {
        this.gameOverHandler = gameOverHandler;
        Advertisement.Show("rewardedVideo");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Finished:
                gameOverHandler.ContinueGame();
                break;
            case ShowResult.Skipped:
                // Ad was skipped
                break;
            case ShowResult.Failed:
                Debug.LogWarning("The ad did not finish due to an error.");
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ad Ready");
    }
    
}
