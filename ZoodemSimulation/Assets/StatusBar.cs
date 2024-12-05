
using UnityEngine;

public class StatusBar : MonoBehaviour
{
    private Transform _barTransform;
    private SpriteRenderer _actualBar;
    private SpriteRenderer _backgroundBar;

    private float _maxValue;
    private float _currentValue;
    private float _startingSize;
    private bool _isCounting;
    
    // Start is called before the first frame update
    void Awake()
    {
        _actualBar = transform.Find("Front").GetComponent<SpriteRenderer>();
        _barTransform = _actualBar.transform;
        _backgroundBar = transform.Find("Background").GetComponent<SpriteRenderer>();

        _startingSize = _actualBar.size.x;
    }

    public void SetColors(Color colorFront, Color colorBack)
    {
        _actualBar.color = colorFront;
        _backgroundBar.color = colorBack;
    }

    public void StartTimeWithDuration(float time)
    {
        _actualBar.enabled = true;
        _maxValue = time;
        _currentValue = time;
        _isCounting = true;
    }

    // private delegate finishBarDelegate
    public void SetMax(float maxValue)
    {
        _maxValue = maxValue;
        _currentValue = maxValue;
    }

    public void SetCurrentValue(float current)
    {
        _currentValue = Mathf.Clamp(current, 0, _maxValue);
        SetSizeAndPosition();
    }

    private void SetSizeAndPosition()
    {
        var updatedSize = _startingSize * (_currentValue / _maxValue);
        _actualBar.size = new Vector2(updatedSize, _actualBar.size.y);
        
        var displacement = (_startingSize - updatedSize) / _startingSize / 8f*10f;
        _barTransform.position = _backgroundBar.transform.position - _barTransform.right*(displacement)/2f;
    }
    public void Countdown(float deltaTime)
    {
        _currentValue -= deltaTime;
        if (_currentValue <= 0)
        {
            _actualBar.enabled = false;
            _isCounting = false;
            Destroy(gameObject);
        }
        
        SetSizeAndPosition();
    }

    private void Update()
    {
        if(!_isCounting)    return;
        
        Countdown(Time.deltaTime);
    }
}
