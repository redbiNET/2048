
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private bool _testMode = true;

    [SerializeField] private string _gameID;

    [SerializeField] private string _videoID;

    private static AdsManager _instance;
    private void Start()
    {
        if (!_instance) _instance = this;
        Advertisement.Initialize(_gameID, _testMode);       
    }
    static public void ShowVideoAds()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(_instance._videoID);
        }
    }
}
