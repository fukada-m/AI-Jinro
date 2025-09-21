using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] GameObject _skipButton;
    [SerializeField] GameObject _addTimeButton;
    CountdownTimer _countdownTimer;
    PhaseController _phaseController;
    void Awake()
    {
        _countdownTimer = GetComponent<CountdownTimer>();
        if (_countdownTimer == null) Debug.LogError("クラスがGetCompomentできなかった");

         _phaseController = GetComponent<PhaseController>();
        if (_phaseController == null) Debug.LogError("PhaseControllerクラスがGetComponentできませんでした。");
    }

    // 時短
    public void Skip()
    {
        _countdownTimer.ForceEnd();
    }

    // 延長
    public void AddTime()
    {
        _countdownTimer.CurrentTime += 60f;
    }

    public void CheckActiveTimeButton()
    {
        string currentPhase = _phaseController.CurrentPhase;

        // お題フェーズまたは、チャットフェーズならアクティブ
        if (currentPhase == "お題" || currentPhase == "チャット")
        {
            _skipButton.SetActive(true);
            _addTimeButton.SetActive(true);
        }
        // 投票または、処刑フェーズなら延長ボタンのみアクティブ
        else if (currentPhase == "投票" || currentPhase == "処刑")
        {
            _skipButton.SetActive(false);
            _addTimeButton.SetActive(true);
        }
        else
        {
            _skipButton.SetActive(false);
            _addTimeButton.SetActive(false);
        }
    }
}
