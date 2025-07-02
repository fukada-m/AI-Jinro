using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void FadeAndChangeScene(string sceneName)
    {
        StartCoroutine(FadeInAndLoadScene(sceneName));
    }

    private IEnumerator FadeInAndLoadScene(string sceneName)
    {
        // フェードイン（透明→黒）
        float elapsed = 0f;
        Color color = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // シーン遷移
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void Start()
    {
        // シーン遷移後に黒→透明へフェードアウト
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        // 初期状態を黒にしておく
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        // フェードアウト（黒→透明）
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
