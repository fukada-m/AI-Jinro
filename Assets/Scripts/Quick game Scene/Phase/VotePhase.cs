using System.Collections;
using UnityEngine;
using UniRx;

// 投票フェーズ
public class VotePhase : PhaseBase
{
    IMessageState _voteState;
    Vote _vote;
    AiManager _aiManager;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _voteState = GetComponent<SubjectState>();
        if (_voteState == null) Debug.LogError("SubjectStateクラスがGetComponentできませんでした");

        _vote = GetComponent<Vote>();
        if (_vote == null) Debug.LogError("VoteクラスがGetComponentできませんでした");
        _aiManager = GetComponent<AiManager>();

        if (_aiManager == null) Debug.LogError("AiManagerクラスがGetComponentできませんでした");
        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした");

    }
    void Start()
    {
        // 投票数が格納されたリアクティブプロパティを監視
        _vote.VoteCount.Subscribe(voteCount =>
            {
                VoteEndAction(voteCount);
            }).AddTo(this);
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
        _vote.CheckActiveVoteButton(); // このフェーズでは投票ボタンをアクティブにする
    }

    // AIに投票させる 
    protected override void AiAction()
    {
        _aiManager.Vote(_board.Circles, _vote);
    }

    // 全員の投票が終わったらこのフェーズを終わらせる
    void VoteEndAction(int vouteCount)
    {
        if (vouteCount == 7)
        {
            StartCoroutine(ForceEnd());
        }
    }

    protected override void SetMessageState()
    {
        _messageContext.SetState(_voteState);
    }

    // フェーズを強制終了
    IEnumerator ForceEnd()
    {
        yield return new WaitForSeconds(1f); // 最後の人が投票してから待つ時間
        _flashMessage.ShowMessage("全員の投票が終わりました。");
        yield return new WaitForSeconds(1f); // このフラッシュメッセージを表示する時間

        _countdownTimer.ForceEnd(); // フェーズの強制終了
    }
}
