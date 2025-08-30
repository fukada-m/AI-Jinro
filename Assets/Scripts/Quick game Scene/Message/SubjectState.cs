using System;
using UnityEngine;

public class SubjectState : MonoBehaviour, IMessageState
{
    public void SendMessage()
    {
        // TODO 処理を実装する
        Debug.Log("お題を回答");
    }
}
