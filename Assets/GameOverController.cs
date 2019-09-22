﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(.5f);
        GameController.Instance.GameEnded = false;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        var inputHorizontal = Input.GetButton("AButton");
        if (inputHorizontal)
        {
            StartCoroutine(RestartGame());
        }
    }
}
