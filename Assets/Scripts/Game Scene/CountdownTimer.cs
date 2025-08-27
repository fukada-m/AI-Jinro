using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    Text countdownText;
    public float startTime;

    private float currentTime;
    private bool isCounting = false;

    public void StartCountdown()
    {
        isCounting = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0 && isCounting)
        {
            currentTime -= Time.deltaTime; // 毎フレーム減らす
            string timeString = Mathf.CeilToInt(currentTime).ToString();
            countdownText.text = $"あと{timeString}秒";
        }
        else
        {
            countdownText.text = "終了";
            // ここで「時間切れ処理」を追加できる
        }
    }
}
