using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private TextMeshProUGUI _outputValue;

    [SerializeField] private float _speed;
    private float _size;

    public int Points { get; private set; }
    public int Value { get; private set; }

    public bool IsMerged { get; set; }

    public Vector2Int Position { get; private set; }

    [SerializeField] private Image _image;
    private Sequence _sequence;

    public void IncreaceNumber()
    {
        Points *= 2;
        Game.AddPoints(Points);
        ++Value;
        IsMerged = true;
        DoScale();
    }
    private void DoScale()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(_rectTransform.DOScale(_rectTransform.localScale * 1.2f, _speed));
        _sequence.Append(_rectTransform.DOScale(_rectTransform.localScale, _speed));
        _sequence.OnComplete(UpdateCell);
    }
    public void Initialize(Vector2Int startPosition, int startValue)
    {
        _size = _rectTransform.sizeDelta.x *_rectTransform.localScale.x;
        Position = startPosition;
        _rectTransform.localPosition = new Vector3(startPosition.x * _size + _size/2, startPosition.y * _size + _size / 2,0);
        Points = 2 * (startValue + 1);
        Value = startValue;
        UpdateCell();
    }
    private void SetColor()
    {
        _image.color = ColorContainer.GetCellColor(Value);
        _outputValue.color = ColorContainer.GetTextColor(Value);

    }
    public bool TryMerge(Cell cellToMerge)
    {
        if(!cellToMerge.IsMerged && cellToMerge.Value == Value)
        {
            Move(cellToMerge.Position, true);

            cellToMerge.IncreaceNumber();
            Game.AddPoints(Points);
            return true;
        }
        return false;
    }
    public void Move(Vector2Int position, bool destroyOnComplete = false)
    {
        Position = position;
        var _sequence = _rectTransform.DOLocalMove(new Vector3(position.x * _size + _size/2, position.y * _size + _size/2, 0), _speed);
        if (destroyOnComplete)
        { 
            _outputValue.text = string.Empty;
            _sequence.OnComplete(Delete);
        }
    }
    private void UpdateCell()
    {
        SetColor();
        _outputValue.text = Points.ToString();
    }
    public void Delete()
    {
        _sequence.Kill();
        Destroy(gameObject);
    }
}