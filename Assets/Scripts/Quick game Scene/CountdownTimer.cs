using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using System;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] Text _text; //あと何秒か表示する
    [SerializeField] float _startTime; // タイマーが何秒か決める

    private float _currentTime;
    private Coroutine _countdownCoroutine;

    // カウントダウンが終了したことを通知するイベント
    Subject<bool> noticeEndCount = new Subject<bool>();
    public IObservable<bool> EndCount => noticeEndCount;

    void Start()
    {
        _currentTime = _startTime;
        _text.text = $"あと{Mathf.CeilToInt(_currentTime)}秒";
    }
    public void StartCountdown()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
        }
        _countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        _currentTime = _startTime;

        while (_currentTime > 0)
        {
            _text.text = $"あと{Mathf.CeilToInt(_currentTime)}秒";
            yield return null; // 1フレーム待つ
            _currentTime -= Time.deltaTime;
        }

        _text.text = "終了";
        noticeEndCount.OnNext(false); //カウントダウン終了したことを通知
    }
    
}
