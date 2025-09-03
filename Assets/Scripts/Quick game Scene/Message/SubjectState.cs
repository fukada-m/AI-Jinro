using UnityEngine;
using UnityEngine.UI;

// お題への回答を投稿するステート
public class SubjectState : MonoBehaviour, IMessageState
{
    [SerializeField] Board _board;  // ボード
    [SerializeField] InputField inputField;  // 入力欄

    // メッセージを掲示板に送って入力欄をクリア
    public void SendMessage()
    {
        string text = inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        _board.SetText(1, text);

        inputField.text = ""; // 入力欄をクリア
    }
}
