using UnityEngine;

public class AiManager : MonoBehaviour
{
    [SerializeField] Board _board; //掲示板
    [SerializeField] Vote _vote; // 投票
    Ai[] _AIs = new Ai[6]; // クイックゲームだとAIは5人

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
        // 一人ずつ回答セットする
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
            Debug.Log($"プレイヤー{playerNum}に投票しました");
        }
    }

}