using UniRx;
using UnityEngine;
using UnityEngine.UI;

// 掲示板を管理するクラス
public class Boad : MonoBehaviour
{
    public int CurrentIndex; // 今どのボードを表示しているか管理する
    [SerializeField] Text _board; //ボードに表示する文字列
    [SerializeField] Circle[] _circles = new Circle[7]; // 丸の配列 7 個ある
    string[] _textArr = new string[7]; // ボードに表示するテキストの配列 7 個ある

    void Start()
    {
        CurrentIndex = 0; //初期インデックスは 0
        // イベントを購読。丸がクリックされたら表示される内容を丸に対応するものに変更
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
    }

    // 本来はAIに考えさせてからテキストをセットするためStartより後のタイミングで実行する
    public void Initialize()
    {
        // とりあえずお題は固定
        _textArr[0] = "日本人の国民性";
        _board.text = _textArr[CurrentIndex]; // 最初は[0]を表示 
    }

    // ボードに文字列をセットし表示するメソッド
    public void SetText(int i, string s)
    {
        _textArr[i] = s;
        Display(i);
    }

    // ボードに表示するメソッド
    public void Display(int i)
    {
        _board.text = _textArr[i];
    }
}
