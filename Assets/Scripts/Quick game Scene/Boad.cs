using UnityEngine;
using UnityEngine.UI;

public class Boad : MonoBehaviour
{
    // ボードテキストの配列7個ある
    string[] _textArr = new string[7];

    int _currentIndex;

    [SerializeField]
    // 現在のボード
    Text _board;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentIndex = 0;
    }

    // 本来はAIに考えさせてからテキストをセットするためStartより後のタイミングで実行する
    public void Initialize()
    {
        // とりあえずお題は固定
        _textArr[0] = "日本人の国民性";
        // 最初は[0]を表示 
        _board.text = _textArr[_currentIndex];
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // ボードを表示するメソッド
}
