using UnityEngine;

// 投票フェーズ
public class VotePhase : PhaseBase
{
    IMessageState _voteState;
    Vote _vote;
    AiManager _aiManager;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _voteState = GetComponent<SubjectState>();
        if (_voteState == null) Debug.LogError("SubjectStateクラスがGetComponentできませんでした");
        _vote = GetComponent<Vote>();
        if (_vote == null) Debug.LogError("VoteクラスがGetComponentできませんでした");
        _aiManager = GetComponent<AiManager>();
        if (_aiManager == null) Debug.LogError("AiManagerクラスがGetComponentできませんでした");
        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした");

    }

    // AIに投票させる 
    // TODO 非同期メソッドにして終わったら投票完了にする。今はUpdateで監視してる
    protected override void AiAction()
    {
        _aiManager.Vote(_board.Circles, _vote);
    }

    void Update()
    {
        if (_vote.GetVoteCount() == 7)
        {
            _countdownTimer.ForceEnd(); // フェーズの強制終了
        }
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
        _vote.CheckActiveVoteButton(); // このフェーズでは投票ボタンをアクティブにする
    }
    protected override void SetMessageState()
    {
        _messageContext.SetState(_voteState);
    }


}
