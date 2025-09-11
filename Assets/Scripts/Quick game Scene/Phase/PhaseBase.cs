using UnityEngine;
using UnityEngine.UI;

// フェーズの抽象クラス
public abstract class PhaseBase : MonoBehaviour
{
    [SerializeField] protected int _time; // フェーズの制限時間
    protected CountdownTimer _countdownTimer; // 残り秒数を表示する

    [SerializeField] protected string _message; // フラッシュメッセージに表示する内容
    protected FlashMessage _flashMessage; // フラッシュメッセージ

    [SerializeField] protected string _text; // フェーズテキストに表示する文言
    [SerializeField] protected Text _phaseText; // 今どのフェーズか表示する

    protected MessageContext _messageContext; // メッセージの送信先を決めるステートパターン

    protected virtual void Start()
    {
        _flashMessage = GetComponent<FlashMessage>();
        _countdownTimer = GetComponent<CountdownTimer>();
        _messageContext = GetComponent<MessageContext>();
    }

<<<<<<< HEAD
    // カウントダウンを開始して、フラッシュメッセージを流して、フェーズテキストを表示する
=======
>>>>>>> 52748b358108569bc8e1a7a9efd4c3041501f440
    public virtual void ChangePhase()
    {
        _countdownTimer.StartCountdown(_time);
        _flashMessage.ShowMessage(_message);
        _phaseText.text = _text;
    }

<<<<<<< HEAD
    protected abstract void SetMessageState(); // 各ステートを継承先でセットする
=======
    protected abstract void SetMessageState();
>>>>>>> 52748b358108569bc8e1a7a9efd4c3041501f440
}
