using UnityEngine;

public class AiManager : MonoBehaviour
{
    Board _board; //掲示板
    Ai[] _AIs = new Ai[6]; // クイックゲームだとAIは5人

    void Awake()
    {
        _board = GetComponent<Board>();
    }
    void Start()
    {
        // AIを5体作成
        for (int i = 0; i < _AIs.Length; i++)
        {
            _AIs[i] = new Ai();
        }
        
        int index = 2; // AI掲示板の2番目から使う
        var subject = _board.GetSubject();

        // 一人ずつ回答セットして最後はお題を表示する
        foreach (var ai in _AIs)
        {
            _board.SetText(index, ai.AnswerQuestion(subject));
            index++;
            _board.Display(0);
        }

    }

}
