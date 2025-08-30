using UnityEngine;

public class ChangeSizeManager : MonoBehaviour
{
    // 拡大したときのサイズ
    [SerializeField]
    Vector2 _bigSize;

    // 拡大縮小を管理する丸が入った配列
    [SerializeField]
    ChangeSize[] _changeSizes = new ChangeSize[7];

    void Start()
    {
        // 初期表示はお題
        _changeSizes[0].SizeUp(_bigSize);
    }

    // クリックを検知したら全ての丸を一度標準サイズにしてからクリックされた対象だけを拡大する
    public void DetctionClick(ChangeSize cz)
    {
        foreach (var changeSize in _changeSizes)
        {
            changeSize.SizeInitialize();
        }

        cz.SizeUp(_bigSize);
    }
}
