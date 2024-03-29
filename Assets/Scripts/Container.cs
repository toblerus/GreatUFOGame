﻿using UnityEngine;

public class Container : MonoBehaviour
{
    public static Container Instance { get; private set; }
    private void Awake() => Instance = this;

    public PlayerController Player1 => _player1;
    public PlayerHealth Player1Health => _player1Health;
    public ManualPlayerControl Player1ManualControls => _player1ManualControls;
    public PlayerAgent Player1Agent => _player1Agent;
    
    public PlayerController Player2 => _player2;
    public PlayerHealth Player2Health => _player2Health;
    public ManualPlayerControl Player2ManualControls => _player2ManualControls;
    public PlayerAgent Player2Agent => _player2Agent;

    public UfoController Ufo => _ufo;
    public BossHealth UfoHealth => _ufoHealth;

    public AlienBoss AlienBoss => _alienBoss;
    public BossHealth AlienBossHealth => _alienBossHealth;
    public AlienBossAgent AlienBossAgent => _alienBossAgent;
    
    [Header("Player1")]
    [SerializeField] private PlayerController _player1;
    [SerializeField] private PlayerHealth _player1Health;
    [SerializeField] private ManualPlayerControl _player1ManualControls;
    [SerializeField] private PlayerAgent _player1Agent;

    [Header("Player1")]
    [SerializeField] private PlayerController _player2;
    [SerializeField] private PlayerHealth _player2Health;
    [SerializeField] private ManualPlayerControl _player2ManualControls;
    [SerializeField] private PlayerAgent _player2Agent;

    [Header("Ufo")]
    [SerializeField] private UfoController _ufo;
    [SerializeField] private BossHealth _ufoHealth;

    [Header("Alien Boss")]
    [SerializeField] private AlienBoss _alienBoss;
    [SerializeField] private BossHealth _alienBossHealth;
    [SerializeField] private AlienBossAgent _alienBossAgent;

}
