using UnityEngine;

// 投票フェーズ
public class VotePhase : PhaseBase
{
    IMessageState _voteState;
    [SerializeField] AiManager _aiManager;

    protected override void Awake()
    {
        base.Awake();
        _voteState = GetComponent<SubjectState>();
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
    }
    protected override void SetMessageState()
    {
        _messageContext.SetState(_voteState);
    }


}
