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
    int[] _results = new int[8]; // 投票結果を格納

    void Awake()
    {
        _phaseController = GetComponent<PhaseController>();
        _board = GetComponent<Board>();
        _flashMessage = GetComponent<FlashMessage>();
    }

    void Start()
    {
        // 全ての要素を0で初期化
        for (int i = 0; i < _results.Length; i++)
        {
            _results[i] = 0;
        }
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
        int index = _board.CurrentIndex;
        _results[index]++;
        _flashMessage.ShowMessage($"プレイヤー{index}に投票しました");
        // 投票したらボタンはずっと非アクティブ
        _voteButton.SetActive(false);
        _isLocked = true;
    }

    // AIの投票用
    public void AiVote(int i)
    {
        _results[i]++;
    }

    // 投票結果が同数の場合は一番小さい添え字になる
    public int GetTotal()
    {
        int max = _results.Max();
        return Array.IndexOf(_results, max);
    }

    // 投票フェーズでのみ投票ボタンを表示する
    void CheckActiveVoteButton()
    {
        string currentPhase = _phaseController.CurrentPhase;
        //投票フェーズでなければ非アクティブ
        if (currentPhase != "投票")
        {
            _voteButton.SetActive(false);
            return;
        }

        if (_isLocked == true) return; // ロック中はずっと非アクティブ

        int CurrentCircle = _board.CurrentIndex;
        // 掲示板の表示がお題とプレイヤー1でなければアクティブ
        if (CurrentCircle == 0 || CurrentCircle == 1)
        {
            _voteButton.SetActive(false);
        }
        else
        {
            _voteButton.SetActive(true);
        }
    }


}
