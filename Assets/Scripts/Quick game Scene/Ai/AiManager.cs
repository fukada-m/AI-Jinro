using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    [SerializeField] Circle _SubjectCircle;
    
    void Awake()
    {
        if (_SubjectCircle == null) Debug.LogError("Circleクラスがありませんでした");
    }

    // お題に答える
    // 引数：掲示板に表示されてる丸たち
    public void CreateAnswer(List<Circle> circles)
    {
        var subject = _SubjectCircle.Text;
        foreach (var circle in circles)
        {
            if (circle.Ai != null) circle.Text = circle.Ai.AnswerQuestion(subject);
        }
    }

    // 投票する
    // 引数：掲示板に表示されてる丸たち、Voteクラス
    public void Vote(List<Circle> circles, Vote vote)
    {
        for (int i = 0; i < circles.Count; i++)
        {
            if (circles[i].Ai != null)
            {
                int playerNum = circles[i].Ai.ThinkVote();
                vote.AiVote(playerNum);
                Debug.Log($"{circles[i].Title}はプレイヤー{playerNum}に投票しました");
            }
        }
    }

}