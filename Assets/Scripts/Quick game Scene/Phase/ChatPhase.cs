using UnityEngine;

// チャットフェーズ
public class ChatPhase : PhaseBase
{
    IMessageState _chatState;

    protected override void Awake()
    {
        base.Awake();
        _chatState = GetComponent<ChatState>();
    }
    protected override void SetMessageState()
    {
        _messageContext.SetState(_chatState);
    }

}
