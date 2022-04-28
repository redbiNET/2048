
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SwipeHandler : MonoBehaviour,IDragHandler, IPointerUpHandler,IPointerDownHandler
{
    private Vector2 _direction;

    private bool _ishandlerDown;

    private Action<Vector2Int> _action;
    [SerializeField] private Vector2 _sensitivity;

    public void SetAction(Action<Vector2Int> action)
    {
        _action = action;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _direction += eventData.delta;
        if (!_ishandlerDown) return;

        if (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y))
        {
            if (_direction.x >= _sensitivity.x) InvokeSwipe(new Vector2Int(1, 0));
            else if (_direction.x <= -_sensitivity.x) InvokeSwipe(new Vector2Int(-1, 0));
        }
        else
        {
        if (_direction.y >= _sensitivity.y) InvokeSwipe(new Vector2Int(0, 1));
        else if (_direction.y <= -_sensitivity.y) InvokeSwipe(new Vector2Int(0, -1));
        }

    }
    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerUp(PointerEventData eventData)
    {
        _ishandlerDown = true;   
        _direction = Vector2.zero;        
    }
    private void InvokeSwipe(Vector2Int direction)
    {
        _ishandlerDown = false;
        Debug.Log(_direction);
        _action.Invoke(direction);

    }
}
