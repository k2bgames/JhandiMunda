using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public static class GameDetails
{
    public static int MinBetAmount = 5;
    public static int MaxBetAmount = 100;
}

[Serializable]
public enum BetSymbol
{
    Chidi,
    Mukut,
    Hukum,
    Itti,
    Jhandi,
    Paan,
}

[Serializable]
public class KhorKhoreIcon
{
    public BetSymbol Symbol;
    public Color IconColor;
}

public class KhorKhoreGameManager : Singleton<KhorKhoreGameManager, Exisiting>
{
    public PlayerCreationManager PlayerCreationManager;
    public BetManager BetManager;
    public IconManager IconManager;
    public GameLoadingTimer GameLoadingTimerForDelay;
    public UIManager UIManager;

    private void OnEnable()
    {
        BetManager.OnBettingRoundFinished += StartRound;
    }

    private void OnDisable()
    {
        BetManager.OnBettingRoundFinished -= StartRound;
    }

    private void Start()
    {
        IconManager.InitializedButtons();
        PlayerCreationManager.CreatePlayers();
        StartRound();
    }

    private float _roundDelayTimer = 5f;
    private IEnumerator DelayStartNextRound()
    {
        GameLoadingTimerForDelay.StartTimer(_roundDelayTimer);
        yield return new WaitForSeconds(_roundDelayTimer);
        OnPlayerClosedInfoPanel();
        BetManager.CollectBets(PlayerCreationManager.Players);
    }

    private void StartRound()
    {
        StartCoroutine(DelayStartNextRound());
    }
    
    public void OnPlayerClosedInfoPanel()
    {
        GameLoadingTimerForDelay.Deactivate();
        BetManager.ClearBettingMadeDetails();
        UIManager.HideDiceDesult();
    }
}
