using UnityEngine;

// お題フェーズ
public class SubjectPhase : PhaseBase
{
    AiManager _aiManager;
    Board _board;
    Subject _subject;
    SubjectState _subjectState;
    [SerializeField] Circle _subjectCircle;

    protected override void Awake()
    {
        base.Awake();
        _aiManager = GetComponent<AiManager>();
        if (_aiManager == null) Debug.LogError("AiManagerクラスがGetComponentできませんでした");

        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした");

        _subject = GetComponent<Subject>();
        if (_subject == null) Debug.LogError("SubjectクラスがGetComponentできませんでした");

        _subjectState = GetComponent<SubjectState>();
        if (_subjectState == null) Debug.LogError("SubjectStateクラスがGetComponentできませんでした");

        if (_subjectCircle == null) Debug.LogError("Circleクラスがありませんでした");
    }

    // AIにお題とお題への回答を生成させる
    // TODO 非同期にする
    protected override void AiAction()
    {
        _subjectCircle.Name = "お題";
        _subjectCircle.Text = _subject.CreateSubject();
        _subjectCircle.OnClick();
        _aiManager.CreateAnswer(_board.Circles);
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
