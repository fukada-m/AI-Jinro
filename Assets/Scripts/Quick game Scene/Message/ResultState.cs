using UnityEngine;

// 結果発表中は投稿できなくなるステート
public class ResultState : MonoBehaviour, IMessageState
{
    FlashMessage _flashMessage; // フラッシュメッセージ

    void Awake()
    {
        _flashMessage = GetComponent<FlashMessage>();
    }
    public void SendMessage()
    {
        _flashMessage.ShowMessage("結果発表中はお静かに");
    }
}
