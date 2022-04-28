
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] private CastomSlider _slider;
    public void LoadGame()
    {
        PlayerPrefs.SetInt("size",_slider.Value);
        SceneManager.LoadScene(1);
    }
}
