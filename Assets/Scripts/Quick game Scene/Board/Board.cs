using UniRx;
using UnityEngine;
using UnityEngine.UI;

// 掲示板を管理するクラス。クイックゲームは参加者が6人で固定
public class Board : MonoBehaviour
{
    public int CurrentIndex = 0; // 今どのボードを表示しているか管理する変数
    [SerializeField] Text _titleText; // ボードに表示されるタイトル
    [SerializeField] Text _mainText; // ボードに表示される本文
    [SerializeField] Circle[] _circles = new Circle[8]; // 丸の配列 8 個ある
    string[] _mainTextArr = new string[8]; // ボードに表示する本文の配列 8 個ある
    string[] _titleTextArr = new string[8]; // ボードに表示するタイトルの配列 8 個ある

    void Start()
    {

        Thinking();
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

        // ボードにタイトルをセット
        for (int i = 0; i < _titleTextArr.Length; i++)
        {
            // 1ページ目はお題、残りはプレイヤー名の回答
            if (i == 0) _titleTextArr[i] = "お題";
            if (i > 0) _titleTextArr[i] = $"プレイヤー{i}の回答";
        }
    }

    // ボードの配列に文字列をセットするメソッド
    // 引数には表示するボードの番号と文字列を受け取る
    public void SetText(int i, string s)
    {
        _mainTextArr[i] = s;
    }

    // 次のページを表示する
    public void OnNext()
    {
        // 6の次は無い
        if (CurrentIndex < 7) CurrentIndex++;
        Display(CurrentIndex);
        _circles[CurrentIndex].OnClick();
    }

    // 前のページを表示する
    public void OnBack()
    {
        // 0より前はない
        if (CurrentIndex > 0) CurrentIndex--;
        Display(CurrentIndex);
        _circles[CurrentIndex].OnClick();
    }

    // 本来はAIに考えさせてからテキストをセットする
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

    // お題を返す
    public string GetSubject()
    {
        return _mainTextArr[0];
    }
}
