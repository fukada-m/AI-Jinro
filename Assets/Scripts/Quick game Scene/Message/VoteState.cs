using UnityEngine;

// 投票中は投稿できなくするステート
public class VoteState : MonoBehaviour, IMessageState
{
    [SerializeField] FlashMessage _flashMessage; // フラッシュメッセージ

    public void SendMessage()
    {
        _flashMessage.ShowMessage("投票中はメッセージを投稿できません");
    }
}
