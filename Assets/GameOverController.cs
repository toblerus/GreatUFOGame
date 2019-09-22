using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button _resetButton;
    [SerializeField] private TextMeshProUGUI _winnerText;

    public TextMeshProUGUI WinnerText => _winnerText;

    // Start is called before the first frame update
    void Start()
    {
        _resetButton.onClick.AddListener(() 
            => StartCoroutine(RestartGame()));   
    }

    private IEnumerator RestartGame()
    {
        _resetButton.interactable = false;
        yield return new WaitForSeconds(.5f);
        GameController.Instance.GameEnded = false;
        SceneManager.LoadScene(1);
    }
}
