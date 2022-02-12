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
    }
    static public Color GetCellColor(int value)
    {
        return value < _colorContainer._cellColors.Length ?
            _colorContainer._cellColors[value] :
            _colorContainer._cellColors[_colorContainer._textColors.Length - 1];
    }
    
    static public Color GetTextColor(int value)
    {
        return value < _colorContainer._textColors.Length ?
            _colorContainer._textColors[value] : 
            _colorContainer._textColors[_colorContainer._textColors.Length - 1];
    }
}
