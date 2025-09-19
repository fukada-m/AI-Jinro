using UnityEngine;

// 処刑中は投稿できなくなるステート
public class ExecutionState : MonoBehaviour, IMessageState
{
    FlashMessage _flashMessage; // フラッシュメッセージ

    void Awake()
    {
        _flashMessage = GetComponent<FlashMessage>();
    }
    public void SendMessage()
    {
        _flashMessage.ShowMessage("余計なことを言ってないで処刑するんだ");
    }
}
