using UnityEngine;
using UniRx;

public class Execution : MonoBehaviour
{
    Board _board;
    Transform _canvasTransform; // フラッシュメッセージの開始位置
    [SerializeField] GameObject _flashMessagePfefab;
    PhaseController _phaseController;
    [SerializeField] Circle[] _circles = new Circle[8]; //イベント購読用
    [SerializeField] GameObject _ExecuteButton;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 丸がクリックされたら投票ボタンを表示するかチェックする
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    CheckActiveExecutionButton(); // 投票ボタンを表示するかチェックする
                })
                .AddTo(this);
        }
        CheckActiveExecutionButton(); // 投票ボタンを表示するかチェックする
    }

    // Update is called once per frame
    void Update()
    {

    }

    // クリックされたプレイヤーを処刑する
    public void OnClick()
    {

    }

    void CheckActiveExecutionButton()
    {
 
        string currentPhase = _phaseController.CurrentPhase;
        //処刑フェーズでなければ非アクティブ
        if (currentPhase != "処刑")
        {
            _ExecuteButton.SetActive(false);
            return;
        }

        Circle currentCircle = _board.CurrentCircle;
        // 掲示板の表示がお題とプレイヤー1でなければアクティブ
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
