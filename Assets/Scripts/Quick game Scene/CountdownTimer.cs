using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public bool IsCounting { get; private set; } // カウントダウンしているかどうか

    [SerializeField]
    Text _text;
    [SerializeField]
    float _startTime;

    private float _currentTime;

    public void StartCountdown()
    {
        IsCounting = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentTime = _startTime;
        IsCounting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime > 0 && IsCounting)
        {
            _currentTime -= Time.deltaTime; // 毎フレーム進んだ秒数を減らす
            string timeString = Mathf.CeilToInt(_currentTime).ToString();
            _text.text = $"あと{timeString}秒";
        }
        else
        {
            _text.text = "終了";
            IsCounting = false;
        }
    }
}
