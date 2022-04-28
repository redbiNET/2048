
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastomSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _view;
    [SerializeField] private TextMeshProUGUI _outputValue;
    [SerializeField] private Sprite[] _spritesForView;

    public int Value 
    {
        get
        {
            return (int)_slider.value;
        }
        private set
        {
            _slider.value = value;
        }
    }
    [SerializeField] private int _minValue;
    public int MaxValue => _spritesForView.Length + _minValue - 1;
    private void OnValidate()
    {
        _slider.minValue = _minValue;
        _slider.maxValue = MaxValue;
    }
    private void Awake()
    {
        Value = PlayerPrefs.GetInt("size");
        ChangeImage();
    }
    public void ChangeImage()
    {
        _view.sprite = _spritesForView[Value - _minValue];
        _outputValue.text = $"{Value}X{Value}";
    }

}
