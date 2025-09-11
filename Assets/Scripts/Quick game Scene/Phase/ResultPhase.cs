using UnityEngine;

// 結果発表フェーズ
public class ResultPhase : PhaseBase
{
    IMessageState _resultState;

    protected override void Awake()
    {
        base.Awake();
        _resultState = GetComponent<ResultState>();
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
    }
    protected override void SetMessageState()
    {
        _messageContext.SetState(_resultState);
    }

}
