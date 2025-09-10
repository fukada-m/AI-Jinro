using UniRx;
using UnityEngine;

public class CircleSelector : MonoBehaviour
{
    public int CurrentCircle{ get; private set; }
    [SerializeField] Vector2 _bigSize; // 拡大したときのサイズ
    [SerializeField] Circle[] _circles = new Circle[7]; // 拡大縮小を管理する丸が入った配列

    void Start()
    {
        // 初期値はお題
        CurrentCircle = 0;
        _circles[0].ChangeSize(_bigSize);

        // 7個のサークルのイベントを購読
        // イベントの通知を受け取ったら一度全ての丸を初期サイズに戻してからクリックされた丸だけ拡大する
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    Select(circle);
                    CurrentCircle = circle.Index;
                })
                .AddTo(this);
        }
    }

    // 次の丸を選択する
    public void Next()
    {
        // 6の次は無い
        if (CurrentCircle < 6) CurrentCircle++;
        Select(_circles[CurrentCircle]);

    }

    // ひとつ前の丸を選択する
    public void Back()
    {
        // 0より前は無い
        if (CurrentCircle > 0) CurrentCircle--;
        Select(_circles[CurrentCircle]);
    }

    // 一度全ての丸を初期サイズに戻してから選択された丸だけ拡大する
    void Select(Circle circle)
    {
        foreach (var c in _circles) c.SizeInitialize();
        circle.ChangeSize(_bigSize);
    }
}
