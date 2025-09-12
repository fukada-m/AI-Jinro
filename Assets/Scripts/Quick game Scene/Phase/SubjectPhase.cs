using UnityEngine;

// お題フェーズ
public class SubjectPhase : PhaseBase
{
    IMessageState _subjectState;
    AiManager _aiManager;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _subjectState = GetComponent<SubjectState>();
        if (_subjectState == null) Debug.LogError("SubjectStateクラスがGetComponentできませんでした");
        _aiManager = GetComponent<AiManager>();
        if (_aiManager == null) Debug.LogError("AiManagerクラスがGetComponentできませんでした");
        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした");
    }

    // AIにお題とお題への回答を生成させる
    // TODO 非同期にする
    protected override void AiAction()
    {
        _board.SetText(0, ThinkSubject()); // お題をセットするだけ
        _board.Display(0); // お題をボードに表示する
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

    // TODO AIに非同期でお題を考えさせる
    string ThinkSubject()
    {
        return "日本人の国民性";
    }

}
