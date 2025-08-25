using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CountFieldController : MonoBehaviour
{
    [Header("UI 参照")]
    [SerializeField] private Button minusButton;
    [SerializeField] private Button plusButton;
    [SerializeField] private TMP_Text countLabel;

    [Header("設定")]
    [SerializeField] private int minCount = 1;
    [SerializeField] private int maxCount = 10;

    private int currentCount;

    void Start()
    {
        // 初期値
        currentCount = minCount;
        UpdateUI();

        // ボタン登録
        minusButton.onClick.AddListener(OnMinus);
        plusButton.onClick.AddListener(OnPlus);
    }

    private void OnMinus()
    {
        if (currentCount <= minCount) return;
        currentCount--;
        UpdateUI();
    }

    private void OnPlus()
    {
        if (currentCount >= maxCount) return;
        currentCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        // ラベル更新
        countLabel.text = $"{currentCount}";
    }
}
