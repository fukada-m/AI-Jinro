using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;

    [SerializeField]
    ChangeSizeManager _changeSizeManager;

    Vector2 initialSize;

    void Awake()
    {
        // 現在のサイズを取得して初期サイズとする
        initialSize = rect.sizeDelta;
    }

    // クリックされたらクリックされたことをマネージャーに通知する
    public void OnClick()
    {
        _changeSizeManager.DetctionClick(this);
    }

    // マネージャーから送られてきた拡大サイズにサイズアップ
    public void SizeUp(Vector2 newSize)
    {
        rect.sizeDelta = newSize;
    }

    // 初期サイズに戻す
    public void SizeInitialize()
    {
        rect.sizeDelta = initialSize;
    }
}
