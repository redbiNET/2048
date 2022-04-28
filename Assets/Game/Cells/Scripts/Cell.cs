
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    [SerializeField] private TextMeshProUGUI _outputValue;

    [SerializeField] private float _speed;
    [SerializeField] private float _sizeIncrease;

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
        _cellAnimation = new CellAnimation(_rectTransform,_speed,_sizeIncrease);
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
[System.Serializable]
public class CellAnimation
{
    [SerializeField] private float _speed;
    [SerializeField] private float _sizeIncrease;
    private float _size;

    public Sequence CellSequence { get; private set; }
    private readonly RectTransform _rectTransform;

    public CellAnimation(RectTransform transform, float speed, float sizeIncrease)
    {
        _rectTransform = transform;
        _size = _rectTransform.sizeDelta.x *_rectTransform.localScale.x;
        _speed = speed;
        _sizeIncrease = sizeIncrease;
    }
    public void DoScale()
    {
        CellSequence = DOTween.Sequence();
        CellSequence.Append(_rectTransform.DOScale(_rectTransform.localScale * _sizeIncrease, _speed));
        CellSequence.Append(_rectTransform.DOScale(_rectTransform.localScale, _speed));
    }
    public Tween Move(Vector2Int position)
    {
        return _rectTransform.DOLocalMove(new Vector3(position.x * _size + _size/2, position.y * _size + _size/2, 0), _speed);
    }
    public void Spawn(Vector2Int position)
    {
        _rectTransform.DOScale(_rectTransform.localScale, _speed).From(Vector3.zero);
        _rectTransform.localPosition = new Vector3(position.x * _size + _size/2, position.y * _size + _size / 2,0);    
    }
    public void Kill()
    {
        CellSequence.Kill();
    }
}