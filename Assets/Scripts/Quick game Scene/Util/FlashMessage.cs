using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//フラッシュメッセージの挙動を管理するクラス
public class FlashMessage : MonoBehaviour
{
    [SerializeField] Text messageText;   // 表示するメッセージ
    [SerializeField] float moveDistance;  // 下に移動する距離
    [SerializeField] float moveSpeed;     // 移動スピード(px/sec)

    RectTransform parentRect; // 親オブジェクトの位置
    Vector2 startPos; // スタート位置
    Coroutine currentCoroutine;

    void Awake()
    {
        // 初期位置を保存
        parentRect = messageText.transform.parent.GetComponent<RectTransform>();
        startPos = parentRect.anchoredPosition;
    }

    // メッセージを表示してコルーチンを起動
    public void ShowMessage(string message)
    {
        messageText.text = message;

        // 既にメッセージ表示されている場合はストップして元の位置に戻す
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            parentRect.anchoredPosition = startPos; // 元の位置に戻す
        }

        currentCoroutine = StartCoroutine(ShowAndMove());
    }

    // 下に動かすコルーチン。スタート地点(画面外)に戻す
    private IEnumerator ShowAndMove()
    {
        Vector3 targetPos = startPos - new Vector2(0, moveDistance); // ゴール地点を作成
        float elapsed = 0f;

        // 下へ移動
        while (Vector3.Distance(parentRect.anchoredPosition, targetPos) > 0.1f)
        {
            parentRect.anchoredPosition = Vector3.MoveTowards(parentRect.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
