using UnityEngine;
using UnityEngine.UI;


public class ChatState : MonoBehaviour, IMessageState
{
    [SerializeField] private InputField inputField;  // 入力欄
    [SerializeField] private ScrollRect scrollRect;      // ScrollView本体
    [SerializeField] private Transform content;          // メッセージを表示する Content オブジェクト
    [SerializeField] private GameObject messagePrefab;   // メッセージのPrefab

    public void SendMessage()
    {
        string text = inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        // contentの子としてメッセージプレハブを生成して入力した文字を表示
        GameObject newMessage = Instantiate(messagePrefab, content);
        Text messageText = newMessage.GetComponent<Text>();
        messageText.text = text;

        // 入力欄をクリア
        inputField.text = "";

        // スクロールを一番下へ
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
