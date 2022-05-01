
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class CellAnimation
{
    [SerializeField] private float _speed;
    [SerializeField] private float _sizeIncrease;
    private float _size;

    public Sequence CellSequence { get; private set; }
    private RectTransform _rectTransform;

    public void Initialize(RectTransform transform)
    {
        _rectTransform = transform;
        _size = _rectTransform.sizeDelta.x * _rectTransform.localScale.x;
    }
    public void DoScale()
    {
        CellSequence = DOTween.Sequence();
        CellSequence.Append(_rectTransform.DOScale(_rectTransform.localScale * _sizeIncrease, _speed));
        CellSequence.Append(_rectTransform.DOScale(_rectTransform.localScale, _speed));
    }
    public Tween Move(Vector2Int position)
    {
        return _rectTransform.DOLocalMove(new Vector3(position.x * _size + _size / 2, position.y * _size + _size / 2, 0), _speed);
    }
    public void Spawn(Vector2Int position)
    {
        _rectTransform.DOScale(_rectTransform.localScale, _speed).From(Vector3.zero);
        _rectTransform.localPosition = new Vector3(position.x * _size + _size / 2, position.y * _size + _size / 2, 0);
    }
    public void Kill()
    {
        CellSequence.Kill();
    }
}
