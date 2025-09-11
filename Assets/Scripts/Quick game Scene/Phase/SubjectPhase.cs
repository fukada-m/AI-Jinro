using UnityEngine;

// お題フェーズ
public class SubjectPhase : PhaseBase
{
    IMessageState _subjectState;

    protected override void Awake()
    {
        base.Awake();
        _subjectState = GetComponent<SubjectState>();
    }
    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
    }
    protected override void SetMessageState()
    {
        _messageContext.SetState(_subjectState);
    }

}
