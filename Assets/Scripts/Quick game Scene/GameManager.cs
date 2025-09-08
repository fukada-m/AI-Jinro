using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Board _board; // 掲示板
    [SerializeField] FlashMessage _flashMessage; // フラッシュメージ
    [SerializeField] CountdownTimer _countdownTimer;  // 残り秒数を表示する
    [SerializeField] MessageContext _messageContext;  // メッセージのステートパターン
    [SerializeField] ChatState _chatState; // チャットステート
    [SerializeField] SubjectState _subjectState; // お題ステート
    string _currentPhase;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentPhase = "お題";
        _board.Initialize();  // 掲示板を初期化
        _countdownTimer.EndCount.Subscribe(isCounting => ChangePhase()).AddTo(this); // カウントダウンが終わると通知されるイベントを購読
        _countdownTimer.StartCountdown(30); // カウントダウンスタート
        _messageContext.SetState(_subjectState); // 最初はお題ステート
        _flashMessage.ShowMessage("お題に答えよう");
    }

    // フェーズは お題 ⇒ チャット⇒ 投票
    void ChangePhase()
    {
        if (_currentPhase == "お題")
        {
            ChangeChatState();
            _currentPhase = "チャット";
        } else if (_currentPhase == "チャット")
        {

        }

    }
    // お題フェーズのカウントダウンが終了したらチャットステートに変える
    void ChangeChatState()
    {
        _messageContext.SetState(_chatState);
        _flashMessage.ShowMessage("誰がAIなのかチャットで話し合おう");
        _countdownTimer.StartCountdown(120);
    }
    

}
