using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using System;

// 各フェーズでカウントダウンを行う
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] Text _text; //あと何秒か表示する

    Coroutine _countdownCoroutine;

    // カウントダウンが終了したことを通知するイベント
    Subject<Unit> _noticeEndCount = new Subject<Unit>();
    public IObservable<Unit> EndCount => _noticeEndCount;

    void Start()
    {
        _text.text = "スタート！！";
    }

    public void StartCountdown(float startTime)
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
        }
        _countdownCoroutine = StartCoroutine(CountdownRoutine(startTime));
    }

    // カウントダウンを強制終了
    public void ForceEnd()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }
        _text.text = "終了";
        _noticeEndCount.OnNext(Unit.Default); //カウントダウン終了したことを通知
    }

    IEnumerator CountdownRoutine(float startTime)
    {
        float currentTime = startTime;

        while (currentTime > 0)
        {
            _text.text = $"あと{Mathf.CeilToInt(currentTime)}秒";
            yield return null; // 1フレーム待つ
            currentTime -= Time.deltaTime;
        }

        _text.text = "終了";
        _noticeEndCount.OnNext(Unit.Default); //カウントダウン終了したことを通知
    }

}
