using UnityEngine;

// チャットフェーズ
public class ChatPhase : PhaseBase
{
    ChatState _chatState;
    AiManager _aiManager;
    Board _board;

    protected override void Awake()
    {
        base.Awake();

        _chatState = GetComponent<ChatState>();
        if (_chatState == null) Debug.LogError("ChatStateがGetComponentできませんでした");

        _aiManager = GetComponent<AiManager>();
        if (_aiManager == null) Debug.LogError("AiManagerがGetComponentできませんでした");

        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardがGetComponentできませんでした");

    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
    }

    protected override void SetMessageState()
    {
        _messageContext.SetState(_chatState);
    }

    protected override void AiAction()
    {
        _aiManager.Chat(_board.Circles);
    }

}
