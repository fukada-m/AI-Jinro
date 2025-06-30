using System.Collections;
using UnityEngine;
using TMPro;

public class TextSmoothBlink : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetText;
    [Tooltip("消える→出る ひとサイクルの時間（秒）")]
    [SerializeField] private float duration = 2f;

    private void Start()
    {
        if (targetText == null) targetText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeLoop());
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            // アルファ：1 → 0
            yield return StartCoroutine(Fade(1f, 0.1f, duration));
            // アルファ：0 → 1
            yield return StartCoroutine(Fade(0.1f, 1f, duration));
        }
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            targetText.alpha = alpha;
            yield return null;
        }
        // 最終値をしっかりセット
        targetText.alpha = to;
    }
}
