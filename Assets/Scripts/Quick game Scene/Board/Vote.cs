using UnityEngine;
using UniRx;

public class Vote : MonoBehaviour
{
    [SerializeField] PhaseController _phaseController;
    [SerializeField] Board _board;
    [SerializeField] FlashMessage _flashMessage;
    [SerializeField] GameObject _voteButton;
    [SerializeField] Circle[] _circles = new Circle[7];
    bool isClicked = false;
    bool _isLocked = false;
    int[] _results = new int[7];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
                    UpdateSetActive();
                })
                .AddTo(this);
        }
        UpdateSetActive();
    }

    // クリックされたプレイヤーに投票する
    public void OnClick()
    {
        if (isClicked)
        {
            _flashMessage.ShowMessage("投票は一度しか行えません");
        }
        else
        {
            int index = _board.CurrentIndex;
            _results[index]++;
            isClicked = true;
            _flashMessage.ShowMessage($"プレイヤー{index}に投票しました");
        }
        // 投票したらボタンはずっと非アクティブ
        _voteButton.SetActive(false);
        _isLocked = true;
    }
    // 今のフェーズを参考に表示 / 非表示を切り替える
    void UpdateSetActive()
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
