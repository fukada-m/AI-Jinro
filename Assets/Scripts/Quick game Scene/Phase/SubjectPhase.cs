using UnityEngine;

// お題フェーズ
public class SubjectPhase : PhaseBase
{
    IMessageState _subjectState;
    AiManager _aiManager;

    protected override void Awake()
    {
        base.Awake();
        _subjectState = GetComponent<SubjectState>();
        if (_subjectState == null) Debug.LogError("SubjectStateクラスがGetComponentできませんでした");
        _aiManager = GetComponent<AiManager>();
        if (_aiManager == null) Debug.LogError("AiManagerクラスがGetComponentできませんでした");
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
