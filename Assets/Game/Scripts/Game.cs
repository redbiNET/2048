using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Board _board;

    [SerializeField] private TextMeshProUGUI _outputPoints;

    [SerializeField] private GameObject _losePanel;

    private int _points;

    public int Points {
        get { return _points; }
        private set
        {
            _points = value;
            _outputPoints.text = _points.ToString();
        } 
    }

    private static Game _game;

    private void Awake()
    {
        if (!_game) _game = this;

    }
    private void Start()
    {
        _board.SetBoard(PlayerPrefs.GetInt("size"));
        Points = PlayerPrefs.GetInt("points" + _board.Size);
    }
    public static void AddPoints(int poins)
    {
        _game.Points += poins;
    }

    public static void LoseGame()
    {
        _game._losePanel.SetActive(true);
    }

    public void ReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        PlayerPrefs.SetInt("points" + _board.Size, Points);
        _board.Save();
        SceneManager.LoadScene(0);
    }
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("points" + _board.Size, Points);
        _board.Save();
    }
}
