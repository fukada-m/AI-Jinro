using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    // 遷移先シーン名をInspectorから指定可能にする
    public string sceneName;

    // ボタンから呼ぶメソッド
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
