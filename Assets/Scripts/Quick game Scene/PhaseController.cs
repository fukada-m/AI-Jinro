using UniRx;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    [SerializeField] FlashMessage _flashMessage; // フラッシュメージ
    [SerializeField] CountdownTimer _countdownTimer;  // 残り秒数を表示する
    [SerializeField] MessageContext _messageContext;  // メッセージのステートパターン
    [SerializeField] ChatState _chatState; // チャットステート
    [SerializeField] SubjectState _subjectState; // お題ステート
    [SerializeField] int _subjectTime; // お題フェーズの時間
    [SerializeField] int _chatTime; // チャットフェーズの時間
    string _currentPhase; 

    void Start()
    {
        _currentPhase = "お題";
        _countdownTimer.EndCount.Subscribe(isCounting => ChangePhase()).AddTo(this); // カウントダウンが終わると通知されるイベントを購読
        _countdownTimer.StartCountdown(_subjectTime); // カウントダウンスタート
        _messageContext.SetState(_subjectState); // 最初はお題ステート
        _flashMessage.ShowMessage("お題に答えよう");
    }

    // フェーズは お題 ⇒ チャット⇒ 投票 ⇒ 結果発表
    void ChangePhase()
    {
        // お題 ⇒ チャット
        if (_currentPhase == "お題")
        {
            ChangeChatState();
            _currentPhase = "チャット";
        }
        // チャット ⇒ 投票
        else if (_currentPhase == "チャット")
        {
            _currentPhase = "投票";

        }
        // 投票 ⇒ 結果発表
        else if (_currentPhase == "投票")
        {

        }
    }

    // チャットステートに遷移
    void ChangeChatState()
    {
        _messageContext.SetState(_chatState);
        _flashMessage.ShowMessage("誰がAIなのかチャットで話し合おう");
        _countdownTimer.StartCountdown(_chatTime);
    }
}
