using UnityEngine;
using UnityEngine.UI;

// 勝敗判定フェーズでは投稿できなくなるステート
public class JudgeState : MonoBehaviour, IMessageState
{
    [SerializeField] InputField _inputField;      // 入力欄
    [SerializeField] ScrollRect _scrollRect;      // ScrollView本体
    [SerializeField] Transform _content;          // メッセージを表示する Content オブジェクト
    [SerializeField] GameObject _messagePrefab;   // メッセージのPrefab

    // チャット同じこと書いてるよ
    public void SendMessage()
    {
        string text = _inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        // contentの子としてメッセージプレハブを生成して入力した文字を表示自動で一番下に追加される
        GameObject newMessage = Instantiate(_messagePrefab, _content);
        Text messageText = newMessage.GetComponentInChildren<Text>();
        messageText.text = text;

        // 入力欄をクリア
        _inputField.text = "";

        // スクロールを一番下へ
        Canvas.ForceUpdateCanvases();
        _scrollRect.verticalNormalizedPosition = 0f;
    }
}
