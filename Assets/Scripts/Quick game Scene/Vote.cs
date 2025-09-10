using UnityEngine;

public class Vote : MonoBehaviour
{
    [SerializeField] CircleSelector _circleSelector;
    [SerializeField] FlashMessage _flashMessage;
    bool isClicked = false;
    int[] _results = new int[7];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 全ての要素を0で初期化
        for (int i = 0; i < _results.Length; i++)
        {
            _results[i] = 0;
        }
    }

    // クリックされたプレイヤーに投票する
    public void OnClick()
    {
        if (isClicked)
        {
            _flashMessage.ShowMessage("投票は一度しか行えません");
        }
        else
        {
            int index = _circleSelector.CurrentCircle;
            _results[index]++;
            isClicked = true;
            _flashMessage.ShowMessage($"プレイヤー{index}に投票しました");
        }
    }
}
