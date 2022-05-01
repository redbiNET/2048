using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{   
    private static AdsManager _instance;

    [SerializeField] private bool _testMode = true;

    private readonly string _gameID = "4729143";
    private readonly string _videoID = "Interstitial_Android";
    private readonly string _bannerID = "Banner_Android";

    private Coroutine _showBanner;

    private void Awake()
    {
        if (!_instance) _instance = this;
        Advertisement.Initialize(_gameID, _testMode);
    }
    private void SetBanner(bool isActive, BannerPosition position = BannerPosition.BOTTOM_CENTER)
    {
        if (isActive)
        {
            _showBanner = StartCoroutine(ShowBanner(position));
        }
        else
        {
            if (_showBanner != null) 
            {
                StopCoroutine(_showBanner);    
            }
            Advertisement.Banner.Hide();  
        }
    }
    static public void ShowVideoAds()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(_instance._videoID);
        }
    }
    static public void StartShowingBanner(BannerPosition position = BannerPosition.BOTTOM_CENTER)
    {
        _instance.SetBanner(true ,position);
    }
    private IEnumerator ShowBanner(BannerPosition position)
    {
        while (!Advertisement.IsReady(_bannerID)) 
        {
            yield return new WaitForSeconds(.5f);
        }
        Advertisement.Banner.SetPosition(position);
        Advertisement.Banner.Show(_bannerID);
    }
    static public void StopShowingBanner()
    { 
        _instance.SetBanner(false);
    } 
}
