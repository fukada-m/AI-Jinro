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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _board.Initialize();  // 掲示板を初期化
        _countdownTimer.EndCount.Subscribe(isCounting => ChangeState(isCounting)).AddTo(this); // カウントダウンが終わると通知されるイベントを購読
        _countdownTimer.StartCountdown(); // カウントダウンスタート
        _messageContext.SetState(_subjectState); // 最初はお題ステート
        _flashMessage.ShowMessage("お題に答えよう");
    }

    // カウントダウンが終了したらステートを変える
    void ChangeState(bool isCounting)
    {
        if (!isCounting)
        {
             _messageContext.SetState(_chatState);
            _flashMessage.ShowMessage("誰がAIなのかチャットで話し合おう");
        }   
    }

}
