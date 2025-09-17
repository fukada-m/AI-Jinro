using UnityEngine;

// 勝敗判定フェーズ
public class JudgePhase : PhaseBase
{
    JudgeState _judgeState;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _judgeState = GetComponent<JudgeState>();
        if (_judgeState == null) Debug.LogError("JudgeStateクラスがGetComponentできませんでした");

        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした");
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
        Judge();
    }

    protected override void SetMessageState()
    {
        _messageContext.SetState(_judgeState);
    }

    void Judge()
    {
        // プレイヤー1がなければ人の負け
        string circle1Name = _board.Circles[1].gameObject.name;
        if (circle1Name != "Circle (1)") _flashMessage.ShowMessage("あなたの負けです");

        // Circleが3つなら人の勝ち
        if (_board.Circles.Count == 3) _flashMessage.ShowMessage("あなたの勝ちです");

        // それ以外は続行
        _flashMessage.ShowMessage("続行します");
    }
}
