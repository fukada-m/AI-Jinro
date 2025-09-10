using UniRx;
using UnityEngine;
using UnityEngine.UI;

// 掲示板を管理するクラス。クイックゲームは参加者が6人で固定
public class Board : MonoBehaviour
{
    public int CurrentIndex = 0; // 今どのボードを表示しているか管理する変数
    [SerializeField] Text _titleText; // ボードに表示されるタイトル
    [SerializeField] Text _mainText; // ボードに表示される本文
    [SerializeField] Circle[] _circles = new Circle[7]; // 丸の配列 7 個ある
    string[] _mainTextArr = new string[7]; // ボードに表示する本文の配列 7 個ある
    string[] _titleTextArr = new string[7]; // ボードに表示するタイトルの配列 7 個ある

    void Start()
    {

        // イベントを購読。丸がクリックされたら表示される内容を丸に対応するものに変更する処理
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle =>
                {
                    CurrentIndex = circle.Index;
                    Display(CurrentIndex);
                })
                .AddTo(this);
        }

        // ボードのタイトルに文字列をセットする
        for (int i = 0; i < _titleTextArr.Length; i++)
        {
            // 一個目の丸はお題、残りはプレイヤー名
            if (i == 0) _titleTextArr[i] = "お題";
            if (i > 0) _titleTextArr[i] = $"プレイヤー{i}";
        }
        Thinking(); // お題の答えを考える
    }

    // ボードに文字列をセットし表示するメソッド
    // 引数には表示するボードの番号と文字列を受け取る
    public void SetText(int i, string s)
    {
        _mainTextArr[i] = s;
        Display(i);
    }

    public void Next()
    {
        // 6の次は無い
        if (CurrentIndex < 6) CurrentIndex++;
        Display(CurrentIndex);
        _circles[CurrentIndex].OnClick();
    }

    public void Back()
    {
        // 0より小さくならない
        if (CurrentIndex > 0) CurrentIndex--;
        Display(CurrentIndex);
        _circles[CurrentIndex].OnClick();
    }

    // 本来はAIに考えさせてからテキストをセットするためStartより後のタイミングで実行する
    void Thinking()
    {
        // とりあえずお題は固定
        _mainTextArr[0] = "日本人の国民性";
        _mainText.text = _mainTextArr[CurrentIndex]; // 最初は[0]を表示 
    }

    // ボードに表示するメソッド
    // 引数には表示するボードの番号を受け取る
    public void Display(int i)
    {
        _mainText.text = _mainTextArr[i];
        _titleText.text = _titleTextArr[i];
    }
    public string GetSubject()
    {
        return _mainTextArr[0];
    }
}
