using UnityEngine;

public class TimeController : MonoBehaviour
{
    CountdownTimer _countdownTimer;

    void Awake()
    {
        _countdownTimer = GetComponent<CountdownTimer>();
        if (_countdownTimer == null) Debug.LogError("クラスがGetCompomentできなかった");
    }

    public void Skip()
    {
        _countdownTimer.ForceEnd();
    }

    public void AddTime()
    {
        _countdownTimer.CurrentTime += 60f;
    }

}
