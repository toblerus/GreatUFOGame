using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private bool _playersWon;
    public bool PlayersWon => _playersWon;
    public bool GameEnded;
    public float _timer;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("AButton"))
        {
            SceneManager.LoadScene(0);
            GameEnded = false;
        }

        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (GameEnded)
        {
            return;
        }

        if (PlayerService.Instance.Players.Length == 0)
        {
            return;
        }
        
        var player1Hp = PlayerService.Instance.Players[0].PlayerHealth.CurrentHealth;
        var player2Hp = PlayerService.Instance.Players[1].PlayerHealth.CurrentHealth;

        var ufoHp = UfoService.Instance.Ufo.Health.CurrentHealth;
        var alienHp = UfoService.Instance.Alien.Health.CurrentHealth;

        if (player1Hp <= 0 && player2Hp <= 0 && !GameEnded)
        {
            StartCoroutine(SetGameOver(false));
            return;
        }

        if (ufoHp <= 0 && !GameEnded)
        {
            UfoService.Instance.Ufo.gameObject.SetActive(false);
            UfoService.Instance.Alien.gameObject.SetActive(true);
        }

        if (alienHp <= 0)
        {
            StartCoroutine(SetGameOver(true));
        }
    }

    private IEnumerator SetGameOver(bool didPlayersWin)
    {
        GameEnded = true;

        _playersWon = didPlayersWin;
        SceneManager.LoadScene(2);

        PlayerService.Instance.Players[0].PlayerHealth.CurrentHealth = 1;
        PlayerService.Instance.Players[1].PlayerHealth.CurrentHealth = 1;
        
        yield return new WaitForSeconds(.1f);

        var gameOverController = FindObjectOfType<GameOverController>();
        var winnerText = gameOverController.WinnerText;
        winnerText.text = _playersWon
            ? "Players were victorious!"
            : "The Aliens have won!";

        gameOverController.HighScoreUi.gameObject.SetActive(_playersWon);
    }
}
