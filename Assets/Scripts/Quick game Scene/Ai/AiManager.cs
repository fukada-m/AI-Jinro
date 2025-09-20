using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiManager : MonoBehaviour
{
    [SerializeField] Circle _SubjectCircle;
    [SerializeField] GameObject _messagePrefab;
    [SerializeField] Transform _content;
    [SerializeField] ScrollRect _scrollRect;      // ScrollView本体

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
                vote.AiVote($"プレイヤー{playerNum}");
                Debug.Log($"{circles[i].Name}はプレイヤー{playerNum}に投票しました");
            }
        }
    }

    // 仮でチャットに投稿する
    public void Chat(List<Circle> circles)
    {
        foreach (var circle in circles)
        {
            if (circle.Name == "お題" || circle.Name == "プレイヤー1")
            {

            }
            else
            {
                GameObject messageLineAI = Instantiate(_messagePrefab, _content);
                Text playerName = messageLineAI.GetComponentInChildren<Text>();
                playerName.text = circle.Name;
                Transform message = messageLineAI.transform.Find("Message");
                Text messageText = message.GetComponentInChildren<Text>();
                messageText.text = "パチパチはじけよう";
                Canvas.ForceUpdateCanvases();
                _scrollRect.verticalNormalizedPosition = 0f;
            }
        }
    }
}