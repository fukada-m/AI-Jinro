using UnityEngine;
using UnityEngine.UI;

public class MessageContext : MonoBehaviour
{
    IMessageState _state;

    public void SetState(IMessageState ms)
    {
        _state = ms;
    }

    public void OnClick()
    {
        _state.SendMessage();
    }
}
