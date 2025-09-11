using UnityEngine;

// お題フェーズ
public class SubjectPhase : PhaseBase
{
    IMessageState _subjectState;
    [SerializeField] AiManager _aiManager;

    protected override void Awake()
    {
        base.Awake();
        _subjectState = GetComponent<SubjectState>();
    }

    // AIにお題への回答を生成させる
    protected override void AiAction()
    {
        _aiManager.CreateAnswer();
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
