using UnityEngine;

public class AiManager : MonoBehaviour
{
    Board _board; //掲示板
    Vote _vote; // 投票
    Ai[] _AIs = new Ai[6]; // クイックゲームだとAIは5人

    void Awake()
    {
        _board = GetComponent<Board>();
        if (_board == null) Debug.LogError("BoardクラスがGetComponentできませんでした");
        _vote = GetComponent<Vote>();
        if (_vote == null) Debug.LogError("VoteクラスがGetComponentできませんでした");
    }
    void Start()
    {
        // AIを5体作成
        for (int i = 0; i < _AIs.Length; i++)
        {
            _AIs[i] = new Ai();
        }
    }

    // お題に答える
    public void CreateAnswer()
    {
        var subject = _board.GetSubject();

        int index = 2; // AIは掲示板の2番目から使う
        // 一人ずつ回答をセットする
        foreach (var ai in _AIs)
        {
            _board.SetText(index, ai.AnswerQuestion(subject));
            index++;
        }
    }

    // 投票する
    public void Vote()
    {
        for (int i = 0; i < _AIs.Length; i++)
        {
            int playerNum = _AIs[i].ThinkVote();
            _vote.AiVote(playerNum);
            Debug.Log($"プレイヤー{i+2}はプレイヤー{playerNum}に投票しました");
        }
    }

}