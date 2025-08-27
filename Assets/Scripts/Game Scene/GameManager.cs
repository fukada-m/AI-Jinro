using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text _themeText;
    [SerializeField] CountdownTimer _countdownTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // お題を仮表示。本来はAIにその都度考えてもらう
        SetTheme("パチパチはじけるもの");
        _countdownTimer.StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetTheme(string theme)
    {
        _themeText.text = theme;
    }
}
