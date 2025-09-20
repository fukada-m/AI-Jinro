using UnityEngine;

// チャットフェーズ
public class ChatPhase : PhaseBase
{
    ChatState _chatState;

    protected override void Awake()
    {
        base.Awake();
        _chatState = GetComponent<ChatState>();
        if (_chatState == null) Debug.LogError("ChatStateがGetComponentできませんでした");
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

}
