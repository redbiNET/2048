
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private TextMeshProUGUI _outputValue;

    public int Points { get; private set; }
    public int Value { get; private set; }

    [SerializeField] private int _maxValue = 11;
    public bool IsMerged { get; set; }

    public Vector2Int Position { get; private set; }

    [SerializeField] private Image _image;

    public CellAnimation _cellAnimation;

    public void IncreaceValue()
    {
        Points *= 2;
        Game.AddPoints(Points);
        ++Value;
        if (Value == _maxValue) Game.WinGame();
        IsMerged = true;
        _cellAnimation.DoScale();
        _cellAnimation.CellSequence.OnComplete(UpdateCell);
    }

    public void Initialize(Vector2Int startPosition, int startValue)
    {
        _cellAnimation.Initialize(_rectTransform);
        _cellAnimation.Spawn(startPosition);
        Position = startPosition;

        Points = (int)Mathf.Pow(2, startValue + 1);
        Value = startValue;
        UpdateCell();
    }

    public bool TryMerge(Cell cellToMerge)
    {
        if(!cellToMerge.IsMerged && cellToMerge.Value == Value)
        {
            Move(cellToMerge.Position, true);

            cellToMerge.IncreaceValue();
            Game.AddPoints(Points);
            return true;
        }
        return false;
    }
    public void Move(Vector2Int position, bool destroyOnComplete = false)
    {
        Position = position;
        var _sequence = _cellAnimation.Move(position);
        if (destroyOnComplete)
        { 
            _outputValue.text = string.Empty;
            _sequence.OnComplete(Delete);
        }
    }
    private void UpdateCell()
    {
        _image.color = ColorContainer.GetCellColor(Value);
        _outputValue.color = ColorContainer.GetTextColor(Value);
        _outputValue.text = Points.ToString();
    }
    public void Delete()
    {
        _cellAnimation.Kill();
        Destroy(gameObject);
    }
}