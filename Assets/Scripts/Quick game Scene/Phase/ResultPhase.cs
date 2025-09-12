using UnityEngine;

// 結果発表フェーズ
public class ResultPhase : PhaseBase
{
    IMessageState _resultState;
    Vote _vote;

    protected override void Awake()
    {
        base.Awake();
        _resultState = GetComponent<ResultState>();
        if (_resultState == null) Debug.LogError("ResultStateクラスがGetComponentできなかった");
        _vote = GetComponent<Vote>();
        if (_vote == null) Debug.LogError("VoteクラスがGetComponentできなかった");
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
        _vote.CheckActiveVoteButton(); // このフェーズでは投票ボタンを非アクティブにする
        _flashMessage.ShowMessage($"脱落者はプレイヤー{_vote.GetTotal()}です。残念!");
    }
    protected override void SetMessageState()
    {
        _messageContext.SetState(_resultState);
    }

}
