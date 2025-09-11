using UniRx;
using UnityEngine;

public class CircleSelector : MonoBehaviour
{
    [SerializeField] Vector2 _bigSize; // 拡大したときのサイズ
    [SerializeField] Circle[] _circles = new Circle[8]; // 拡大縮小を管理する丸が入った配列
    [SerializeField] Board _board;

    void Start()
    {
        _circles[0].ChangeSize(_bigSize);

        // 7個のサークルのイベントを購読
        // イベントの通知を受け取ったら一度全ての丸を初期サイズに戻してからクリックされた丸だけ拡大する
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    Select(circle);
                })
                .AddTo(this);
        }
    }

    // 次の丸を選択する
    public void Next()
    {
        Select(_circles[_board.CurrentIndex]);
    }

    // ひとつ前の丸を選択する
    public void Back()
    {
        Select(_circles[_board.CurrentIndex]);
    }

    // 一度全ての丸を初期サイズに戻してから選択された丸だけ拡大する
    void Select(Circle circle)
    {
        foreach (var c in _circles) c.SizeInitialize();
        circle.ChangeSize(_bigSize);
    }
}
