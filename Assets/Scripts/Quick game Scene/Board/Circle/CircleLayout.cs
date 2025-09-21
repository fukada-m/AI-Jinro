using UnityEngine;

// 丸の横並びを制御するクラス
public class CircleLayout : MonoBehaviour
{
    [SerializeField] RectTransform _circlesRT;

    // 丸が削除されると左に寄っちゃうからその分を右に寄せる
    public void ReHorizontalLayout()
    {
        _circlesRT.anchoredPosition = new Vector2(_circlesRT.anchoredPosition.x + 80, _circlesRT.anchoredPosition.y);
    }
}
