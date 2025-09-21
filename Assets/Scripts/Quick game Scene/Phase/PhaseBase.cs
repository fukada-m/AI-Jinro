using UnityEngine;
using UnityEngine.UI;

// フェーズの抽象クラス
public abstract class PhaseBase : MonoBehaviour
{
    protected Transform _canvasTransform; 
    [SerializeField] protected int _time; // フェーズの制限時間
    protected CountdownTimer _countdownTimer; // 残り秒数を表示するタイマー

    [SerializeField] protected GameObject _flashMessagePrefab ;
    [SerializeField] protected string _message; // フラッシュメッセージに表示する内容

    [SerializeField] protected string _text; // フェーズテキストに表示する文言
    [SerializeField] protected Text _phaseText; // 今どのフェーズか表示する

    protected MessageContext _messageContext; // メッセージの送信先を決めるステートパターン
    TimeController _timeController;

    protected virtual void Awake()
    {
        GameObject canvasOBJ = GameObject.Find("Canvas");
        _canvasTransform = canvasOBJ.transform;
        if (_canvasTransform == null) Debug.LogError("Canvasが見つかりませんでした");

        _countdownTimer = GetComponent<CountdownTimer>();
        if (_countdownTimer == null) Debug.LogError("CountdownTimerクラスがGetComponentできませんでした");

        _messageContext = GetComponent<MessageContext>();
        if (_messageContext == null) Debug.LogError("MessageContextクラスがGetComponentできませんでした");

        _timeController = GetComponent<TimeController>();
        if (_timeController == null) Debug.LogError("TimeControllerクラスがGetComponentできませんでした");

    }

    // カウントダウンを開始して、フラッシュメッセージを流して、フェーズテキストを表示して、AIがアクションを行う
    public virtual void ChangePhase()
    {
        _countdownTimer.StartCountdown(_time);

        // フラッシュメッセージを表示
        GameObject flashMessageOBJ = Instantiate(_flashMessagePrefab, _canvasTransform);
        FlashMessage flashMessage = flashMessageOBJ.GetComponent<FlashMessage>();
        flashMessage.ShowMessage(_message);

        _phaseText.text = _text;
        AiAction();
        _timeController.CheckActiveTimeButton();
    }

    // このフェーズで行うAIのアクション
    protected virtual void AiAction()
    {
        Debug.Log($"{this.GetType().Name}にAIのアクションはありません");
    }

    protected abstract void SetMessageState(); // 各フェーズで使用するステートを継承先でセットする
}
