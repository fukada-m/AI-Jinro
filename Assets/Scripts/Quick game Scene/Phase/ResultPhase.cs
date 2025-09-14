using UnityEngine;

// 結果発表フェーズ
public class ResultPhase : PhaseBase
{
    IMessageState _resultState;
    Vote _vote;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _resultState = GetComponent<ResultState>();
        if (_resultState == null) Debug.LogError("ResultStateクラスがGetComponentできなかった");
        _vote = GetComponent<Vote>();
        if (_vote == null) Debug.LogError("VoteクラスがGetComponentできなかった");
        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできなかった");
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
        _vote.CheckActiveVoteButton(); // このフェーズでは投票ボタンを非アクティブにする
        _flashMessage.ShowMessage($"脱落者はプレイヤー{_vote.GetResult()}です。残念!");
        _board.Remove(_vote.GetResult());
        _vote.ResetResult();
    }
    
    protected override void SetMessageState()
    {
        _messageContext.SetState(_resultState);
    }

}
