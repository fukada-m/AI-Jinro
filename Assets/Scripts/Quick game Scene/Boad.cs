using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Boad : MonoBehaviour
{
    public int CurrentIndex;
    [SerializeField] Circle[] _circles = new Circle[7];
    string[] _textArr = new string[7]; // ボードテキストの配列7個ある


    [SerializeField]
    // 現在のボード
    Text _board;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentIndex = 0; //初期インデックスは 0
        foreach (var circle in _circles)
        {
            circle.OnClicked
                .Subscribe(circle => CurrentIndex = circle.Index)
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

    // Update is called once per frame
    void Update()
    {

    }
    
    // ボードを表示するメソッド
}
