using System;
using UnityEngine;

public class BetDetail
{
    public IPlayer player;
    public string PlayerName;
    public int BetAmount;
}

public interface IBetMaker
{
    public void PlaceBet(Action<(BetDetail, BetSymbol), IPlayer> callbackAfterDecision);
}

public interface IPlayer : IBetMaker
{
    public void ClearUIDetails();

    public void SetBetWonDetail(int multipliedBy, int totalAmount);
}
