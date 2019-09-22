using TMPro;
using UnityEngine;
using XInputDotNetPure;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winnerText;

    [SerializeField] private Transform _highScoreUi;

    public Transform HighScoreUi => _highScoreUi;
    
    public TextMeshProUGUI WinnerText => _winnerText;

    private void Start()
    {
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
        GamePad.SetVibration(PlayerIndex.Two, 0, 0);
    }
}
