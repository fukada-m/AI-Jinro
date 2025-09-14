using UnityEngine;
using UniRx;
using System.Linq;
using System;

// 投票を管理するクラス
public class Vote : MonoBehaviour
{
    PhaseController _phaseController;
    Board _board;
    FlashMessage _flashMessage;
    [SerializeField] GameObject _voteButton;
    [SerializeField] Circle[] _circles = new Circle[8]; //イベント購読用
    bool _isLocked = false; // すでに投票したかどうか
    int[] _results = new int[8]; // 投票結果を格納 添え字がそのままプレイヤー番号になる。よって0は使わない

    void Awake()
    {
        _phaseController = GetComponent<PhaseController>();
        if (_phaseController == null) Debug.LogError("PhaseControllerクラスがGetComponentできませんでした。");

        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした。");

        _flashMessage = GetComponent<FlashMessage>();
        if (_flashMessage == null) Debug.LogError("FlashMessageクラスがGetComponentできませんでした。");

    }

    void Start()
    {
        // 全ての要素を0で初期化
        for (int i = 0; i < _results.Length; i++)
        {
            _results[i] = 0;
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
        string playerName = _board.CurrentCircle.Title;
        switch (playerName)
        {
            case "プレイヤー1":
                _results[1]++;
                break;

            case "プレイヤー2":
                _results[2]++;
                break;

            case "プレイヤー3":
                _results[3]++;
                break;

            case "プレイヤー4":
                _results[4]++;
                break;

            case "プレイヤー5":
                _results[5]++;
                break;

            case "プレイヤー6":
                _results[6]++;
                break;
            
            case "プレイヤー7":
                _results[7]++;
                break;
            
        }
        _flashMessage.ShowMessage($"{playerName}に投票しました");
        // 投票したらボタンはずっと非アクティブ
        _voteButton.SetActive(false);
        _isLocked = true;
    }

    // AIの投票用
    public void AiVote(int i)
    {
        _results[i]++;
    }

    // 投票数を返す
    public int GetVoteCount()
    {
        return _results.Sum();
    }

    // 投票結果が同数の場合は一番小さい添え字になる
    public int GetResult()
    {
        int max = _results.Max();
        int result = Array.IndexOf(_results, max);
        return result;
    }

    public void ResetResult()
    {
        _results = new int[8];
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
        if (currentCircle.Title == "お題" || currentCircle.Title == "プレイヤー1")
        {
            _voteButton.SetActive(false);
        }
        else
        {
            _voteButton.SetActive(true);
        }
    }

}
