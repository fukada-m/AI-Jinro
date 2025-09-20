using System.Linq;
using UnityEngine;
//敵プレイヤー
public class Ai : MonoBehaviour
{
    [SerializeField] Board _board;
    // お題を元に回答を考える
    public string AnswerQuestion(string subject)
    {
        return "おしとやか";
    }

    // 誰に投票するか考えさせるプレイヤーは1～7
    // TODO 今はランダムに選んでいるがAIに考えさせる
    public int ThinkVote()
    {
        // Circlesのタイトルから「プレイヤー番号」だけを抽出
        var candidates = _board.Circles
                            .Where(c => c.Name.StartsWith("プレイヤー"))
                            .Select(c => int.Parse(c.Name.Replace("プレイヤー", "")))
                            .ToList();

        // 候補が無い場合はエラー扱い
        if (candidates.Count == 0)
        {
            Debug.LogError("プレイヤーが存在しません");
            return -1;
        }

        // ランダムに1つ選んで返す
        int index = Random.Range(0, candidates.Count);
        return candidates[index];
    }
}