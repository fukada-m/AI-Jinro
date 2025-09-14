using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


// 掲示板を管理するクラス。クイックゲームは参加者が6人で固定
public class Board : MonoBehaviour
{
    public Circle CurrentCircle { get; private set; }
    [SerializeField] Text _titleText; // ボードに表示されるタイトル
    [SerializeField] Text _mainText; // ボードに表示される本文
    public List<Circle> Circles = new List<Circle>();

    void Start()
    {
        // イベントを購読。丸がクリックされたら表示される内容を丸に対応するものに変更する処理
        foreach (var circle in Circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    Display(circle.Title, circle.Text);
                    CurrentCircle = circle;
                })
                .AddTo(this);
        }
    }

    // 次のページを表示する
    public void OnNext()
    {
        int index = Circles.IndexOf(CurrentCircle);
        if (index < (Circles.Count - 1)) index++;
        CurrentCircle = Circles[index];
        CurrentCircle.OnClick();
    }

    // 前のページを表示する
    public void OnBack()
    {
        int index = Circles.IndexOf(CurrentCircle);
        if (index != 0) index--;
        CurrentCircle = Circles[index];
        CurrentCircle.OnClick();
    }
    // 丸を削除する
    public void Remove(int i)
    {
        Circles[i].DestroySelf();
        Circles.RemoveAt(i);
    }

    // ボードに表示するメソッド
    // 引数には表示するボードの番号を受け取る
    void Display(string title, string main)
    {
        _titleText.text = title;
        _mainText.text = main;
    }

    
}
