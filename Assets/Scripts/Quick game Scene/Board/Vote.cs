using UnityEngine;
using UniRx;
using System.Linq;
using System.Collections.Generic;

// 投票を管理するクラス
public class Vote : MonoBehaviour
{
    public ReactiveProperty<int> VoteCount { get; private set; } = new ReactiveProperty<int>(0);
    PhaseController _phaseController;
    Board _board;
    Transform _canvasTransform; // フラッシュメッセージの開始位置
    [SerializeField] GameObject _flashMessagePfefab;
    [SerializeField] GameObject _voteButton;
    [SerializeField] Circle[] _circles = new Circle[8]; //イベント購読用
    bool _isLocked = false; // すでに投票したかどうか
    Dictionary<string, int> _scores = new Dictionary<string, int>(); // プレイヤー名と投票数を格納する辞書
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
        // 全てのプレイヤーの投票数を0で初期化
        for (int i = 0; i < 7; i++)
        {
            _scores[$"プレイヤー{i+1}"] = 0;
        }

        // 丸がクリックされたら投票ボタンを表示するかチェックする
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    CheckActiveVoteButton(); // 投票ボタンを表示するかチェックする
                })
                .AddTo(this);
        }
        CheckActiveVoteButton(); // 投票ボタンを表示するかチェックする
    }

    // クリックされたプレイヤーに投票する
    public void OnClick()
    {
        string playerName = _board.CurrentCircle.Name;
        _scores[playerName]++;
        VoteCount.Value++;

        // フラッシュメッセージを表示
        GameObject flashMessageOBJ = Instantiate(_flashMessagePfefab, _canvasTransform);
        FlashMessage flashMessage = flashMessageOBJ.GetComponent<FlashMessage>();
        flashMessage.ShowMessage($"{playerName}に投票しました");
        // 投票したらボタンはずっと非アクティブ
        _voteButton.SetActive(false);
        _isLocked = true;
    }

    // AIの投票用
    public void AiVote(string s)
    {
        _scores[s]++;
        VoteCount.Value++;
    }

    // 投票結果が同数の場合はランダムに選ばれる
    public string GetResult()
    {
        int maxValue = _scores.Values.Max();
        // 最大値と一致するプレイヤーをすべて候補にする
        var candidates = _scores
            .Where(kv => kv.Value == maxValue)
            .Select(kv => kv.Key)
            .ToList();
        string maxPlayerName = _scores.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        // 候補の中からランダムに1つ選んで返す
        string chosenKey  = candidates[Random.Range(0, candidates.Count)];
        return chosenKey ;
    }

    public void ResetResult()
    {
        // 全てのプレイヤーの投票数を初期化
        for (int i = 0; i < 7; i++)
        {
            _scores[$"プレイヤー{i + 1}"] = 0;
        }
        _isLocked = false;
        VoteCount.Value = 0;
    }

    // 投票フェーズでのみ投票ボタンを表示する
    public void CheckActiveVoteButton()
    {
        string currentPhase = _phaseController.CurrentPhase;
        //投票フェーズでなければ非アクティブ
        if (currentPhase != "投票")
        {
            _voteButton.SetActive(false);
            return;
        }

        if (_isLocked == true) return; // ロック中はずっと非アクティブ

        Circle currentCircle = _board.CurrentCircle;
        // 掲示板の表示がお題とプレイヤー1でなければアクティブ
        if (currentCircle.Name == "お題" || currentCircle.Name == "プレイヤー1")
        {
            _voteButton.SetActive(false);
        }
        else
        {
            _voteButton.SetActive(true);
        }
    }

}
