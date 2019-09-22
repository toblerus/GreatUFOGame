﻿using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private bool _playersWon;
    public bool GameEnded = false;
    
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);

        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        
        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (GameEnded)
        {
            return;
        }
        
        var player1Hp = PlayerService.Instance.Players[0].PlayerHealth.CurrentHealth;
        var player2Hp = PlayerService.Instance.Players[1].PlayerHealth.CurrentHealth;

        var ufoHp = UfoService.Instance.Ufo.Health.CurrentHealth;

        if (player1Hp <= 0 && player2Hp <= 0)
        {
            StartCoroutine(SetGameOver(false));
            return;
        }

        if (ufoHp <= 0)
        {
            StartCoroutine(SetGameOver(true));
        }
    }

    private IEnumerator SetGameOver(bool didPlayersWin)
    {
        GameEnded = true;
        
        _playersWon = didPlayersWin;
        SceneManager.LoadScene(2);

        yield return new WaitForSeconds(.2f);
        
        var winnerText = FindObjectOfType<GameOverController>().WinnerText;
        winnerText.text = _playersWon
            ? "Players were victorious!"
            : "The Aliens have won!";
    }
}
