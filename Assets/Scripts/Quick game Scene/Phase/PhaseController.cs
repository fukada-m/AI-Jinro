using UniRx;
using UnityEngine;

// フェーズを管理する お題 ⇒ チャット⇒ 投票 ⇒ 結果発表 のループ
public class PhaseController : MonoBehaviour
{
    public string CurrentPhase { get; private set; } = "お題"; 
    SubjectPhase _subjectPhase; // お題フェーズ
    ChatPhase _chatPhase; // チャットフェーズ
    VotePhase _votePhase; // 投票フェーズ
    ResultPhase _resultPhase; // 結果発表フェーズ

    void Awake()
    {
        // カウントダウンが終わったらChangePhaseを実行
        CountdownTimer countdownTimer = GetComponent<CountdownTimer>();
        countdownTimer.EndCount.Subscribe(isCounting => ChangePhase()).AddTo(this);

        _subjectPhase = GetComponent<SubjectPhase>();
        _chatPhase = GetComponent<ChatPhase>();
        _votePhase = GetComponent<VotePhase>();
        _resultPhase = GetComponent<ResultPhase>();
    }
    void Start()
    {
        ToSubjectPhase(); //お題フェーズでスタート
    }

    // カウントダウンが終わったらフェーズを お題 ⇒ チャット⇒ 投票 ⇒ 結果発表 の順に移行
    void ChangePhase()
    {
        // お題 ⇒ チャット
        if (CurrentPhase == "お題")
        {
            ToChatPhase();
        }
        // チャット ⇒ 投票
        else if (CurrentPhase == "チャット")
        {
            ToVotePhase();
        }
        // 投票 ⇒ 結果発表
        else if (CurrentPhase == "投票")
        {
            ToResultPhase();
        }
    }

    // お題フェーズに遷移
    void ToSubjectPhase()
    {
        CurrentPhase = "お題";
        _subjectPhase.ChangePhase();
    }
    // チャットフェーズに遷移
    void ToChatPhase()
    {
        CurrentPhase = "チャット";
        _chatPhase.ChangePhase();
    }

    // 投票フェーズに遷移
    void ToVotePhase()
    {
        CurrentPhase = "投票";
        _votePhase.ChangePhase();
    }

    // 結果発表フェーズに遷移
    void ToResultPhase()
    {
        CurrentPhase = "結果発表";
        _resultPhase.ChangePhase();
    }
}
