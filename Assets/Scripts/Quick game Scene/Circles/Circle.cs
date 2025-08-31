using UnityEngine;
using UniRx;
using System;

public class Circle : MonoBehaviour
{
    [SerializeField] RectTransform rect; // 自分の今のサイズ
    // [SerializeField] int index; // 自分の番号
    Vector2 initialSize;  // 初期サイズを取っておくのに使う
    Subject<Circle> clicked = new Subject<Circle>(); // クリックされたら数値を渡すイベント
    public IObservable<Circle> OnClicked => clicked; // イベントは IObservable として外部には公開

    void Awake()
    {
        // 現在のサイズを取得して初期サイズとする
        initialSize = rect.sizeDelta;
    }

    // クリックされたら自分の添え字を通知
    public void OnClick()
    {
        clicked.OnNext(this);
    }

    // 送られてきたサイズアップに変更
    public void ChangeSize(Vector2 newSize)
    {
        rect.sizeDelta = newSize;
    }

    // 初期サイズに戻す
    public void SizeInitialize()
    {
        rect.sizeDelta = initialSize;
    }
}
