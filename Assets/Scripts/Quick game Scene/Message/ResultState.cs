using UnityEngine;

// 結果発表中は投票できなくするステート
public class ResultState : MonoBehaviour, IMessageState
{
    [SerializeField] FlashMessage _flashMessage; // フラッシュメッセージ

    public void SendMessage()
    {
        _flashMessage.ShowMessage("結果発表中はお静かに");
    }
}
