using UnityEngine;
using UniRx;
using System;

public class Circle : MonoBehaviour
{
    public string Text; // 対応する掲示板のテキスト
    public string Title; // 対応する掲示板のタイトル
    RectTransform rect; // 自分の今のサイズ
    Vector2 initialSize;  // 初期サイズを取っておくのに使う

    // OnClicked イベント クリックされたら自分を渡す
    Subject<Circle> clicked = new Subject<Circle>();
    public IObservable<Circle> OnClicked => clicked;

    public Ai Ai;
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        // 現在のサイズを取得して初期サイズとする
        initialSize = rect.sizeDelta;
    }

    // クリックされたら通知先に自分を渡す
    public void OnClick()
    {
        clicked.OnNext(this);
    }

    // 送られてきたサイズに変更
    public void ChangeSize(Vector2 newSize)
    {
        rect.sizeDelta = newSize;
    }

    // 初期サイズに戻す
    public void SizeInitialize()
    {
        rect.sizeDelta = initialSize;
    }

    // このゲームオブジェクトを削除
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }

}
