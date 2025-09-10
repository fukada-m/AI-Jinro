using UniRx;
using UnityEngine;
using UnityEngine.UI;

// フェーズを管理する お題 ⇒ チャット⇒ 投票 ⇒ 結果発表 のループ
public class PhaseController : MonoBehaviour
{
    [SerializeField] FlashMessage _flashMessage; // フラッシュメージ
    [SerializeField] CountdownTimer _countdownTimer;  // 残り秒数を表示する
    [SerializeField] MessageContext _messageContext;  // メッセージのステートパターン
    [SerializeField] ChatState _chatState; // チャットステート
    [SerializeField] SubjectState _subjectState; // お題ステート
    [SerializeField] VoteState _voteState; // 投票ステート
    [SerializeField] ResultState _resultState; // 結果発表ステート
    [SerializeField] int _subjectTime; // お題フェーズの時間
    [SerializeField] int _chatTime; // チャットフェーズの時間
    [SerializeField] int _resultTime; //結果発表フェーズの時間
    [SerializeField] Button _voteButton; //投票ボタン
    [SerializeField] Text _phaseText; // 現在のフェーズを表示するテキスト
    string _currentPhase;

    void Start()
    {
        _countdownTimer.EndCount.Subscribe(isCounting => ChangePhase()).AddTo(this); // カウントダウンが終わると通知されるイベントを購読
        ToSubjectPhase(); //お題フェーズでスタート
    }

    // フェーズは お題 ⇒ チャット⇒ 投票 ⇒ 結果発表
    void ChangePhase()
    {
        // お題 ⇒ チャット
        if (_currentPhase == "お題")
        {
            ToChatPhase();
        }
        // チャット ⇒ 投票
        else if (_currentPhase == "チャット")
        {
            ToVotePhase();
        }
        // 投票 ⇒ 結果発表
        else if (_currentPhase == "投票")
        {
            ToResultPhase();
        }
    }

    // お題フェーズに遷移
    void ToSubjectPhase()
    {
        _currentPhase = "お題";
        _messageContext.SetState(_subjectState); // お題ステート
        _countdownTimer.StartCountdown(_subjectTime); // カウントダウンスタート
        _flashMessage.ShowMessage("お題に回答しよう");
        _voteButton.gameObject.SetActive(false); // 投票ボタンを非アクティブ化
        _phaseText.text = "お題に回答しよう";
    }
    // チャットフェーズに遷移
    void ToChatPhase()
    {
        _currentPhase = "チャット";
        _messageContext.SetState(_chatState); // チャットステート
        _countdownTimer.StartCountdown(_chatTime); //カウントダウンスタート
        _flashMessage.ShowMessage("誰がAIなのかチャットで話し合おう");
        _phaseText.text = "チャットで話し合おう";

    }

    // 投票フェーズに遷移
    void ToVotePhase()
    {
        _currentPhase = "投票";
        _messageContext.SetState(_voteState); // 投票ステート
        _countdownTimer.StartCountdown(_resultTime); //カウントダウンスタート
        _flashMessage.ShowMessage("人間だと思うプレイヤーに投票しよう");
        _phaseText.text = "投票タイム";
        _voteButton.gameObject.SetActive(true); // 投票ボタンをアクティブ化
    }

    // 結果発表フェーズに遷移
    void ToResultPhase()
    {
        _currentPhase = "結果発表";
        _messageContext.SetState(_resultState); // 結果発表ステート
        _flashMessage.ShowMessage("結果発表！！");
        _phaseText.text = "結果発表中";
        _voteButton.gameObject.SetActive(false); // 投票ボタンを非アクティブ化
    }
}
