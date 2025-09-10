using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PhaseController : MonoBehaviour
{
    [SerializeField] FlashMessage _flashMessage; // フラッシュメージ
    [SerializeField] CountdownTimer _countdownTimer;  // 残り秒数を表示する
    [SerializeField] MessageContext _messageContext;  // メッセージのステートパターン
    [SerializeField] ChatState _chatState; // チャットステート
    [SerializeField] SubjectState _subjectState; // お題ステート
    [SerializeField] VoteState _voteState; // 投票ステート
    [SerializeField] int _subjectTime; // お題フェーズの時間
    [SerializeField] int _chatTime; // チャットフェーズの時間
    [SerializeField] Button _voteButton; //投票ボタン
    string _currentPhase;

    void Start()
    {
        _countdownTimer.EndCount.Subscribe(isCounting => ChangePhase()).AddTo(this); // カウントダウンが終わると通知されるイベントを購読
        ChangeSubjectPhase(); //お題フェーズでスタート
    }

    // フェーズは お題 ⇒ チャット⇒ 投票 ⇒ 結果発表
    void ChangePhase()
    {
        // お題 ⇒ チャット
        if (_currentPhase == "お題")
        {
            ChangeChatPhase();
        }
        // チャット ⇒ 投票
        else if (_currentPhase == "チャット")
        {
            ChangeVotePhase();
        }
        // 投票 ⇒ 結果発表
        else if (_currentPhase == "投票")
        {

        }
    }

    // お題フェーズに遷移
    void ChangeSubjectPhase()
    {
        _currentPhase = "お題";
        _messageContext.SetState(_subjectState); // お題ステート
        _countdownTimer.StartCountdown(_subjectTime); // カウントダウンスタート
        _flashMessage.ShowMessage("お題に答えよう");
        _voteButton.gameObject.SetActive(false); // 投票ボタンを非アクティブ
    }
    // チャットフェーズに遷移
    void ChangeChatPhase()
    {
        _currentPhase = "チャット";
        _messageContext.SetState(_chatState); // チャットステート
        _countdownTimer.StartCountdown(_chatTime); //カウントダウンスタート
        _flashMessage.ShowMessage("誰がAIなのかチャットで話し合おう");
    }

    // 投票フェーズに遷移
    void ChangeVotePhase()
    {
        _currentPhase = "投票";
        _messageContext.SetState(_voteState);
        _flashMessage.ShowMessage("人間だと思うプレイヤーに投票しよう");
        _voteButton.gameObject.SetActive(true); // 投票ボタンをアクティブ化

    }
}
