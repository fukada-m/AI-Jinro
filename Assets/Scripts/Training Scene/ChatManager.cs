using UnityEngine;
using UnityEngine.UI;
using System;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private InputField inputField;  // 入力欄
    [SerializeField] private Transform content;          // Content オブジェクト
    [SerializeField] private GameObject messagePrefab;   // メッセージのPrefab
    [SerializeField] private ScrollRect scrollRect;      // ScrollView本体

    public void OnSendMessage()
    {
        string text = inputField.text;
        if (string.IsNullOrWhiteSpace(text)) return;

        // メッセージ生成
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
