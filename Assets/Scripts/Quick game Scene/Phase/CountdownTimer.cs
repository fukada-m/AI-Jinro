using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using System;

// 各フェーズでカウントダウンを行う
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] Text _timerText; //あと何秒か表示する

    Coroutine _countdownCoroutine;

    // カウントダウンが終了したことを通知するイベント
    Subject<Unit> _noticeEndCount = new Subject<Unit>();
    public IObservable<Unit> EndCount => _noticeEndCount;

    void Start()
    {
        _timerText.text = "スタート！！";
    }

    public void StartCountdown(float startTime)
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
        }
        _countdownCoroutine = StartCoroutine(CountdownRoutine(startTime));
    }

    // カウントダウンを強制終了するが通知はしない
    public void GameEnd(string s)
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }
        _timerText.text = s;
    } 

    // カウントダウンを強制終了
    public void ForceEnd()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }
        _timerText.text = "終了";
        _noticeEndCount.OnNext(Unit.Default); //カウントダウン終了したことを通知
    }

    IEnumerator CountdownRoutine(float startTime)
    {
        float currentTime = startTime;

        while (currentTime > 0)
        {
            _timerText.text = $"あと{Mathf.CeilToInt(currentTime)}秒";
            yield return null; // 1フレーム待つ
            currentTime -= Time.deltaTime;
        }

        _timerText.text = "終了";
        _noticeEndCount.OnNext(Unit.Default); //カウントダウン終了したことを通知
    }

}
