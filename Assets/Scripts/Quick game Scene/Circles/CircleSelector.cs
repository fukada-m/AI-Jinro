using UniRx;
using UnityEngine;

public class CircleSelector : MonoBehaviour
{
    public int CurrentCircle;
    [SerializeField] Vector2 _bigSize; // 拡大したときのサイズ
    [SerializeField] Circle[] _circles = new Circle[7]; // 拡大縮小を管理する丸が入った配列

    void Start()
    {
        CurrentCircle = 0;
        _circles[0].ChangeSize(_bigSize); // 初期表示時はお題の丸を拡大

        // 7個のサークルのイベントを購読
        // イベントの通知を受け取ったら一度全ての丸を初期サイズに戻してからクリックされた丸だけ拡大する
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    foreach (var c in _circles) c.SizeInitialize();
                    circle.ChangeSize(_bigSize);
                    CurrentCircle = circle.Index;
                })
                .AddTo(this);
        }
    }

    public void Next()
    {
        if (CurrentCircle < 6) CurrentCircle++;
        foreach (var c in _circles)
        {
            c.SizeInitialize();
        }
        _circles[CurrentCircle].ChangeSize(_bigSize);
    }

    public void Back()
    {
        if (CurrentCircle > 0) CurrentCircle--;
        foreach (var c in _circles)
        {
            c.SizeInitialize();
        }
        _circles[CurrentCircle].ChangeSize(_bigSize);
    }

}
