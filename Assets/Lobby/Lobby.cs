
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    private Lobby _instanse;

    [SerializeField] private CastomSlider _slider;

    private void Awake()
    {
        if (!_instanse) _instanse = this;
        else Destroy(this);
    }

    private void Start()
    {
        AdsManager.StartShowingBanner();
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("size",_slider.Value);
        SceneManager.LoadScene(1);
    }
}
