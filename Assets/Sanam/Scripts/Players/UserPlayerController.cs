using System;
using UnityEngine;

public class UserPlayerController : IPlayer
{
    public delegate void UserEvent();
    public UserEvent OnBettingStarted;

    private string PlayerName = "Your";
    private UIBetDetailDisplay UIBetDetailDisplay;
    public UserPlayerController(UIBetDetailDisplay displayInfo)
    {
        displayInfo.SetBotName(PlayerName);
        UIBetDetailDisplay = displayInfo;
    }

    Action<(BetDetail, BetSymbol), IPlayer> _callbackToMake;
    public void PlaceBet(Action<(BetDetail, BetSymbol), IPlayer> callbackAfterDecision)
    {
        _callbackToMake = callbackAfterDecision;
        OnBettingStarted?.Invoke();
    }

    public void OnUserBetSubmission(int amount, BetSymbol symbol)
    {
        UIBetDetailDisplay.SetBetDetail(amount, IconManager.Instance.KhorKhoreIconColorDict[symbol]);

        _callbackToMake.Invoke((new BetDetail()
        {
            PlayerName = "User ok",
            BetAmount = amount,
            player = this
        }, symbol), this);
    }

    public void ClearUIDetails()
    {
        UIBetDetailDisplay.ResetDetail();
    }
    public void SetBetWonDetail(int multipliedBy, int totalAmount)
    {
        UIBetDetailDisplay.SetBetWonDetail(multipliedBy, totalAmount);
    }
}
