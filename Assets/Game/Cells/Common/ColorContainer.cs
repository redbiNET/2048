using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorContainer : MonoBehaviour
{
    static private ColorContainer _colorContainer;

    [SerializeField] private Color[] _cellColors;
    [SerializeField] private Color[] _textColors;

    public void Awake()
    {
        if (!_colorContainer) _colorContainer = this;
        Debug.Log(_colorContainer);
    }
    static public Color GetCellColor(int value)
    {
        return _colorContainer._cellColors[value];
    }
    
    static public Color GetTextColor(int value)
    {
        return value < _colorContainer._textColors.Length ?
            _colorContainer._textColors[value] : 
            _colorContainer._textColors[_colorContainer._textColors.Length - 1];
    }
}
