using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Board _board;

    [SerializeField] private TextMeshProUGUI _outputPoints;

    [SerializeField] private GameObject LosePanel;

    private int _points;

    private int _boardSize;

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
        _boardSize = PlayerPrefs.GetInt("size");
        _board.SetBoard(_boardSize);        
    }
    public static void AddPoints(int poins)
    {
        _game.Points += poins;
    }

    public static void LoseGame()
    {
        _game.LosePanel.SetActive(true);
    }

    public void ReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        _board.Save();
        SceneManager.LoadScene(0);
    }
    private void OnApplicationPause(bool pause)
    {
        _board.Save();
    }
}
