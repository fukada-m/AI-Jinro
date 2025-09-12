using UnityEngine;

// 投票フェーズ
public class VotePhase : PhaseBase
{
    IMessageState _voteState;
    Vote _vote;
    AiManager _aiManager;

    protected override void Awake()
    {
        base.Awake();
        _voteState = GetComponent<SubjectState>();
        if (_voteState == null) Debug.LogError("SubjectStateクラスがGetComponentできませんでした");
        _vote = GetComponent<Vote>();
        if (_vote == null) Debug.LogError("VoteクラスがGetComponentできませんでした");
        _aiManager = GetComponent<AiManager>();
        if (_aiManager == null) Debug.LogError("AiManagerクラスがGetComponentできませんでした");

    }

    // AIに投票させる 
    // TODO 非同期メソッドにして終わったら投票完了にする。今はUpdateで監視してる
    protected override void AiAction()
    {
        _aiManager.Vote();
    }

    void Update()
    {
        if (_aiManager.VoteDone)
        {
            Debug.Log("投票完了");
            _aiManager.VoteDone = false;
        }
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
