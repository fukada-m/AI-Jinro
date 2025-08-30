using UnityEngine;
using UnityEngine.UI;

public class MessageContext : MonoBehaviour
{
    IMessageState _messageState;

    public void SetState(IMessageState ms)
    {
        _messageState = ms;
    }

    public void OnClick()
    {
        _messageState.SendMessage();
    }
}
