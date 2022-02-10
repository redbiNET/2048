using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _outputPoints;

    [SerializeField] private GameObject LosePanel;

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
    public static void AddPoints(int poins)
    {
        _game.Points += poins;
    }

    public static void LoseGame()
    {
        Time.timeScale = 0;
        _game.LosePanel.SetActive(true);
    }

    public void ReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
