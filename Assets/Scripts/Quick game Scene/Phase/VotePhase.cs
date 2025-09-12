using UnityEngine;

// 投票フェーズ
public class VotePhase : PhaseBase
{
    IMessageState _voteState;
    Vote _vote;
    [SerializeField] AiManager _aiManager;

    protected override void Awake()
    {
        base.Awake();
        _voteState = GetComponent<SubjectState>();
        if (_voteState == null) Debug.LogError("SubjectStateクラスがGetComponentできなかった");
        _vote = GetComponent<Vote>();
        if (_vote == null) Debug.LogError("VoteクラスがGetComponentできなかった");
    }

    // AIに投票させる
    protected override void AiAction()
    {
        _aiManager.Vote();
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
