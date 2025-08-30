using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // [SerializeField] Text _themeText; // お題を表示する
    [SerializeField] BoadManager _boardManager; // 掲示板を管理する
    [SerializeField] CountdownTimer _countdownTimer;  // 残り秒数を表示する
    [SerializeField] MessageContext _messageContext;  // メッセージのステートパターン
    [SerializeField] ChatState _chatState; // チャットステート
    [SerializeField] SubjectState _subjectState; // お題ステート

    // お題をセットする
    // public void SetTheme(string theme)
    // {
    //     _themeText.text = theme;
    // }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boardManager.Initialize();
        _countdownTimer.StartCountdown(); // カウントダウンスタート
        _messageContext.SetState(_subjectState); // 最初はお題ステート
    }

    void Update()
    {
        // カウントダウン中ならお題ステート、カウントダウンしていなければチャットステート
        if (_countdownTimer.IsCounting)
        {
            _messageContext.SetState(_subjectState);
        }
        else
        {
            _messageContext.SetState(_chatState);
        }
    }

}
