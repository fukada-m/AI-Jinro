using UnityEngine;
using UniRx;
using System;

public class Execution : MonoBehaviour
{
    Board _board;
    Transform _canvasTransform; // フラッシュメッセージの開始位置
    PhaseController _phaseController;
    [SerializeField] GameObject _flashMessagePfefab;
    [SerializeField] Circle[] _circles = new Circle[8]; //イベント購読用
    [SerializeField] GameObject _ExecuteButton;

     // OnClickedイベント
    Subject<Unit> clicked = new Subject<Unit>();
    public IObservable<Unit> OnClicked => clicked;

    void Awake()
    {
        GameObject canvasOBJ = GameObject.Find("Canvas");
        _canvasTransform = canvasOBJ.transform;
        if (_canvasTransform == null) Debug.LogError("Canvasが見つかりませんでした。");

        _phaseController = GetComponent<PhaseController>();
        if (_phaseController == null) Debug.LogError("PhaseControllerクラスがGetComponentできませんでした。");

        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした。");
    }

    void Start()
    {
        // 丸がクリックされたら処刑ボタンを表示するかチェックする
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    CheckActiveExecutionButton(); // 処刑ボタンを表示するかチェックする
                })
                .AddTo(this);
        }
        CheckActiveExecutionButton(); // 処刑ボタンを表示するかチェックする
    }

    // クリックされたプレイヤーを処刑する
    public void OnClick()
    {
        string playerName = _board.CurrentCircle.Title;

        // フラッシュメッセージを表示
        GameObject flashMessageOBJ = Instantiate(_flashMessagePfefab, _canvasTransform);
        FlashMessage flashMessage = flashMessageOBJ.GetComponent<FlashMessage>();
        flashMessage.ShowMessage($"{playerName}を処刑しました");

        _board.Remove(playerName);
        clicked.OnNext(Unit.Default);
    }

    // 処刑ボタンを表示するかチェックする
    void CheckActiveExecutionButton()
    {
        string currentPhase = _phaseController.CurrentPhase;
        //処刑フェーズでなければ非アクティブ
        if (currentPhase != "処刑")
        {
            _ExecuteButton.SetActive(false);
            return;
        }

        // 掲示板の表示がお題とプレイヤー1でなければアクティブ
        Circle currentCircle = _board.CurrentCircle;
        if (currentCircle.Title == "お題" || currentCircle.Title == "プレイヤー1")
        {
            _ExecuteButton.SetActive(false);
        }
        else
        {
            _ExecuteButton.SetActive(true);
        }
    }
}
