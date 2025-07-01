using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFadeTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f; // フェード時間(秒)

    public void FadeAndChangeScene(string sceneName)
    {
        StartCoroutine(FadeInAndLoadScene(sceneName));
    }

    private IEnumerator FadeInAndLoadScene(string sceneName)
    {
        Debug.Log("FadeIn Started");
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
        SceneManager.LoadScene(sceneName);
    }
}
