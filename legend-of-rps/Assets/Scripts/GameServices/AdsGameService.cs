using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsGameService : UnityActiveSingleton<AdsGameService>
{

#if UNITY_ANDROID
    string gameID = "4740105"; //For android
#else
    string gameID = "4740104";
#endif

    public static bool isAdsInit;

    private void Awake()
    {
        Advertisement.Initialize(gameID);
        isAdsInit = true;
    }
}
