using UnityEngine;
using UnityEngine.UI;

// お題への回答を投稿するステート
public class SubjectState : MonoBehaviour, IMessageState
{
    [SerializeField] Circle _circle; // プレイヤーの丸
    [SerializeField] InputField inputField;  // 入力欄
    [SerializeField] FlashMessage _flashMessage; // フラッシュメッセージ
    bool _isLocked = false; // お題に回答できるのは1度だけ

    // メッセージを掲示板に送って入力欄をクリア
    public void SendMessage()
    {
        if (_isLocked)
        {
            inputField.text = ""; // 入力欄をクリア
            _flashMessage.ShowMessage("既に回答済みです");
        }
        else
        {
            // 入力された文字列を取得
            string text = inputField.text;
            if (string.IsNullOrWhiteSpace(text)) return;

            // プレイヤー1の回答として掲示板にセット
            _circle.Text = text;
            _circle.OnClick();

            inputField.text = ""; // 入力欄をクリア
            _flashMessage.ShowMessage("お題に回答しました");
            _isLocked = true; // 投稿済み
        }
    }
}
