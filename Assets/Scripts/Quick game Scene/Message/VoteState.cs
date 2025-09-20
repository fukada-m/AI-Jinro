using UnityEngine;

// 投票中は投稿できなくするステート
public class VoteState : MonoBehaviour, IMessageState
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
        flashMessage.ShowMessage("投票中はメッセージを投稿できません");
    }
}
