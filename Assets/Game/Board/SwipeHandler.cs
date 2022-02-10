using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class SwipeHandler : MonoBehaviour,IDragHandler, IPointerUpHandler,IPointerDownHandler
{
    private Vector2 direction;

    private bool _ishandlerDown;

    private Action<Vector2Int> _action;
    [SerializeField] private Vector2 sensitivity;

    public void SetAction(Action<Vector2Int> action)
    {
        _action = action;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.delta);
        Debug.Log(direction);
        direction += eventData.delta;
        if (!_ishandlerDown) return;
        if (direction.x >= sensitivity.x) _action.Invoke(new Vector2Int(1, 0));
        else if (direction.y >= sensitivity.y) _action.Invoke(new Vector2Int(0, 1));
        else if (direction.x <= -sensitivity.x) _action.Invoke(new Vector2Int(-1, 0));
        else if (direction.y <= -sensitivity.y) _action.Invoke(new Vector2Int(0, -1));
        _ishandlerDown = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        direction = Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _ishandlerDown = true;     
    }
}
