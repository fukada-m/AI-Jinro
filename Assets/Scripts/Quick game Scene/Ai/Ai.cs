using UnityEngine;
//敵プレイヤー
public class Ai
{
    // お題を元に回答を考える
    public string AnswerQuestion(string subject)
    {
        return "おしとやか";
    }

    // 誰に投票するか考えさせる
    public int ThinkVote()
    {
        return 2;
    }
}