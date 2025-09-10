using UnityEngine;

// 送られてくるステートによってメッセージを送る挙動を変えられる
public class MessageContext : MonoBehaviour
{
    IMessageState _currentState;

    public void SetState(IMessageState ms)
    {
        _currentState = ms;
    }

    // 送信ボタンがクリックされたら
    public void OnClick()
    {
        _currentState.SendMessage();
    }
}
