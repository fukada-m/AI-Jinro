using System.Collections;
using UnityEngine;

// 勝敗判定フェーズ
public class JudgePhase : PhaseBase
{
    JudgeState _judgeState;
    Board _board;

    protected override void Awake()
    {
        base.Awake();
        _judgeState = GetComponent<JudgeState>();
        if (_judgeState == null) Debug.LogError("JudgeStateクラスがGetComponentできませんでした");

        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした");
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
        StartCoroutine(Judge());
    }

    protected override void SetMessageState()
    {
        _messageContext.SetState(_judgeState);
    }

    // 勝敗判定処理
    IEnumerator Judge()
    {
        yield return new WaitForSeconds(1f);

        // フラッシュメッセージを表示するための準備
        GameObject flashMessageOBJ = Instantiate(_flashMessagePrefab, _canvasTransform);
        FlashMessage flashMessage = flashMessageOBJ.GetComponent<FlashMessage>();

        string circle1Name = _board.Circles[1].gameObject.name; // 2個目の丸の名前を取得

        // プレイヤー1の丸がなければ人の負け
        if (circle1Name != "Circle (1)")
        {
            flashMessage.ShowMessage("あなたの負けです");
            _countdownTimer.GameEnd("Game over");
        }
        // 残ってるCircleが3つなら人の勝ち
        else if (_board.Circles.Count == 3)
        {
            flashMessage.ShowMessage("あなたの勝ちです");
            _countdownTimer.GameEnd("おめでとう");
        }
        // それ以外はゲーム続行
        else
        {
            flashMessage.ShowMessage("AIが1人脱落しました。ゲームは続きます。");
        }

    }
}
