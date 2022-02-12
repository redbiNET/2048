using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastomSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public int Value 
    {
        get
        {
            return (int)_slider.value;
        }
    }

}
