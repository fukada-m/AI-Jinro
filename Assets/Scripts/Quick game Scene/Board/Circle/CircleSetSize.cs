using UniRx;
using UnityEngine;

// 丸のサイズを制御するクラス
public class CircleSetSize : MonoBehaviour
{
    [SerializeField] Vector2 _bigSize; // 拡大したときのサイズ
    Board _board;

    void Awake()
    {
        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetCOmponentできませんでした");
    }
    
    void Start()
    {
        _board.Circles[0].ChangeSize(_bigSize);

        // 7個のサークルのイベントを購読
        // イベントの通知を受け取ったら一度全ての丸を初期サイズに戻してからクリックされた丸だけ拡大する
        foreach (var circle in _board.Circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    Select(circle);
                })
                .AddTo(this);
        }
    }

    // 一度全ての丸を初期サイズに戻してから選択された丸だけ拡大する
    void Select(Circle circle)
    {
        foreach (var c in _board.Circles) c.SizeInitialize();
        circle.ChangeSize(_bigSize);
    }
}
