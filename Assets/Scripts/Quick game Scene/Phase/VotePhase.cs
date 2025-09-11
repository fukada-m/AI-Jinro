using UnityEngine;

// 投票フェーズ
public class VotePhase : PhaseBase
{
    IMessageState _voteState;

    protected override void Awake()
    {
        base.Awake();
        _voteState = GetComponent<SubjectState>();
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
    }
    protected override void SetMessageState()
    {
        _messageContext.SetState(_voteState);
    }


}
