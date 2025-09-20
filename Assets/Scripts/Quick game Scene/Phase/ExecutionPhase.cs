using System.Collections;
using UniRx;
using UnityEngine;

// Ai処刑フェーズ
public class ExecutionPhase : PhaseBase
{
    ExecutionState _executionState;
    Execution _execution; //処刑を制御するクラス
    protected override void Awake()
    {
        base.Awake();

        _executionState = GetComponent<ExecutionState>();
        if (_executionState == null) Debug.LogError("ExecutionStateクラスがGetComponentできませんでした");

        _execution = GetComponent<Execution>();
        if (_execution == null) Debug.LogError("ExecutionクラスがGetComponentできませんでした");
    }

    void Start()
    {
        // 処刑ボタンがクリックされたときのイベント
        _execution.OnClicked
            .Subscribe(_ =>
            {
                StartCoroutine(ForceEnd());
            })
            .AddTo(this);
    }

    public override void ChangePhase()
    {
        base.ChangePhase();
        SetMessageState();
    }

    protected override void SetMessageState()
    {
        _messageContext.SetState(_executionState);
    }

    // フェーズを強制終了
    IEnumerator ForceEnd()
    {
        yield return new WaitForSeconds(1f); // 処刑するプレイヤーのフラッシュメッセージを表示する時間

        // フラッシュメッセージを表示
        GameObject flashMessageOBJ = Instantiate(_flashMessagePrefab, _canvasTransform);
        FlashMessage flashMessage = flashMessageOBJ.GetComponent<FlashMessage>();
        flashMessage.ShowMessage("処刑が終わりました");

        yield return new WaitForSeconds(1f); // 次のメッセージを表示するまでの時間
    

        _countdownTimer.ForceEnd();
    }
}
