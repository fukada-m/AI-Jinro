using UnityEngine;

// 結果発表中は投稿できなくなるステート
public class ResultState : MonoBehaviour, IMessageState
{
    protected Transform _canvasTransform; 
    [SerializeField] protected GameObject _flashMessagePrefab ;

    void Awake()
    {
        GameObject canvasOBJ = GameObject.Find("Canvas");
        _canvasTransform = canvasOBJ.transform;
        if (_canvasTransform == null) Debug.LogError("Canvasが見つかりませんでした");
    }
    public void SendMessage()
    {
        GameObject flashMessageOBJ = Instantiate(_flashMessagePrefab, _canvasTransform);
        FlashMessage flashMessage = flashMessageOBJ.GetComponent<FlashMessage>();
        flashMessage.ShowMessage("結果発表中はお静かに");
    }
}
