using UnityEngine;

// Ai処刑フェーズ
public class ExecutionPhase : PhaseBase
{
    ExecutionState _excutionState;
    protected override void Awake()
    {
        base.Awake();
        _excutionState = GetComponent<ExecutionState>();
        if (_excutionState == null) Debug.LogError("ExecutionクラスがGetComponentできませんでした");

    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
    }

    protected override void SetMessageState()
    {
        _messageContext.SetState(_excutionState);
    }
}
