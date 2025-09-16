using System.Collections;
using UniRx;
using UnityEngine;

// フェーズを管理する お題 ⇒ チャット⇒ 投票 ⇒ 結果発表 のループ
public class PhaseController : MonoBehaviour
{
    public string CurrentPhase { get; private set; } = "お題";
    SubjectPhase _subjectPhase; // お題フェーズ
    ChatPhase _chatPhase; // チャットフェーズ
    VotePhase _votePhase; // 投票フェーズ
    ResultPhase _resultPhase; // 投票結果発表フェーズ
    JudgePhase _judgePhase; // 勝敗判定フェーズ

    void Awake()
    {
        // カウントダウンが終わったらChangePhaseを実行
        CountdownTimer countdownTimer = GetComponent<CountdownTimer>();
        countdownTimer.EndCount.Subscribe(isCounting => ChangePhase()).AddTo(this);

        _subjectPhase = GetComponent<SubjectPhase>();
        if (_subjectPhase == null) Debug.LogError("SubjectPhaseクラスがGetComponentできませんでした");

        _chatPhase = GetComponent<ChatPhase>();
        if (_chatPhase == null) Debug.LogError("ChatPhaseクラスがGetComponentできませんでした");

        _votePhase = GetComponent<VotePhase>();
        if (_votePhase == null) Debug.LogError("VotePhaseクラスがGetComponentできませんでした");

        _resultPhase = GetComponent<ResultPhase>();
        if (_resultPhase == null) Debug.LogError("ResultPhaseクラスがGetComponentできませんでした");

        _judgePhase = GetComponent<JudgePhase>();
        if (_judgePhase == null) Debug.LogError("JudgePhaseクラスがGetComponentできませんでした");
    }
    void Start()
    {
        StartCoroutine(StartGame()); // お題フェーズでスタート
    }

    // 1フレーム待ってからお題フェーズでスタート
    IEnumerator StartGame()
    {
        yield return null; // 1フレーム待つ

        ToSubjectPhase();
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
        // 結果発表 ⇒ 勝敗判定
        else if (CurrentPhase == "投票結果発表")
        {
            ToJudgePhase();
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

    // 投票結果発表フェーズに遷移
    void ToResultPhase()
    {
        CurrentPhase = "投票結果発表";
        _resultPhase.ChangePhase();
    }

    // 勝敗判定フェーズに遷移
    void ToJudgePhase()
    {
        CurrentPhase = "勝敗判定";
        _judgePhase.ChangePhase();
    }
}
