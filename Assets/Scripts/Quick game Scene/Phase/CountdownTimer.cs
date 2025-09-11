using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using System;

// 各フェーズでカウントダウンを行う
public class CountdownTimer : MonoBehaviour
{
    [SerializeField] Text _text; //あと何秒か表示する

    private Coroutine _countdownCoroutine;

    // カウントダウンが終了したことを通知するイベント
    Subject<Unit> noticeEndCount = new Subject<Unit>();
    public IObservable<Unit> EndCount => noticeEndCount;

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

    private IEnumerator CountdownRoutine(float startTime)
    {
        float currentTime = startTime;

        while (currentTime > 0)
        {
            _text.text = $"あと{Mathf.CeilToInt(currentTime)}秒";
            yield return null; // 1フレーム待つ
            currentTime -= Time.deltaTime;
        }

        _text.text = "終了";
        noticeEndCount.OnNext(Unit.Default); //カウントダウン終了したことを通知
    }

}
