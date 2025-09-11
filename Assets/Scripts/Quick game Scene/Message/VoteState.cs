using UnityEngine;

// 投票中は投稿できなくするステート
public class VoteState : MonoBehaviour, IMessageState
{
    FlashMessage _flashMessage; // フラッシュメッセージ

    void Awake()
    {
        _flashMessage = GetComponent<FlashMessage>();
    }
    public void SendMessage()
    {
        _flashMessage.ShowMessage("投票中はメッセージを投稿できません");
    }
}
