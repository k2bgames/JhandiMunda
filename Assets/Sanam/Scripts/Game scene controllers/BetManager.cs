using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ResultDetail
{
    public BetSymbol Symbol;
    public int Count;
}

/// <summary>
/// It collects bet and multiplies or captures based on the game situation.
/// </summary>
public class BetManager : Singleton<BetManager, Exisiting>
{
    /// <summary>
    /// Bet symbol and player bet details.
    /// </summary>
    public Dictionary<BetSymbol, BetDetail> PlayerBets = new Dictionary<BetSymbol, BetDetail>();
    public int MinBetAmount = 5;
    public int MaxBetAmount = 100;


    public delegate void GameEvents();
    public GameEvents OnBettingRoundFinished;
    public delegate void GameEventsDetail(List<BetSymbol> detail);
    public GameEventsDetail OnBettingResultMade;

    private List<IPlayer> _betMakers = new List<IPlayer>();
    private BetSymbol[] _dicesRollResult = new BetSymbol[6];
    private Dictionary<BetSymbol, List<BetDetail>> _bettingMadeDetails = new Dictionary<BetSymbol, List<BetDetail>>()
    {
        { BetSymbol.Chidi, new List<BetDetail>() },
        { BetSymbol.Mukut, new List<BetDetail>() },
        { BetSymbol.Hukum, new List<BetDetail>() },
        { BetSymbol.Itti, new List<BetDetail>() },
        { BetSymbol.Jhandi, new List<BetDetail>() },
        { BetSymbol.Paan, new List<BetDetail>() },
    };

    private List<IPlayer> _pendingBetResponses = new List<IPlayer>();
    private Array _betSymbols = Enum.GetValues(typeof(BetSymbol));
    private System.Random _randomGenerator = new System.Random();

    protected override void Awake()
    {
        base.Awake();
        GameDetails.MaxBetAmount = MaxBetAmount;
        GameDetails.MinBetAmount = MinBetAmount;
    }

    public void CollectBets(List<IPlayer> players)
    {
        _betMakers = players;
        _pendingBetResponses = new List<IPlayer> (_betMakers);
        foreach (IPlayer player in _betMakers)
        {
            player.PlaceBet(OnAPlayerMadeDecision);
        }
    }

    private void OnAPlayerMadeDecision((BetDetail, BetSymbol) betDetail, IPlayer betMaker)
    {
        _pendingBetResponses.Remove(betMaker);
        _bettingMadeDetails[betDetail.Item2].Add(betDetail.Item1);
        if (_pendingBetResponses.Count == 0)
        {
            // All players have made their decisions
            OnAllBetsCollected();
        }
    }

    private void OnAllBetsCollected()
    {
        ShuffleDices();
        ShowResult();
    }

    public void ClearBettingMadeDetails()
    {
        foreach (BetSymbol symbol in _bettingMadeDetails.Keys)
        {
            _bettingMadeDetails[symbol].Clear();
        }

        foreach (IPlayer player in _betMakers)
        {
            player.ClearUIDetails();
        }
    }

    private void ShuffleDices()
    {
        for (int i = 0; i < _dicesRollResult.Length; i++)
        {
            _dicesRollResult[i] = (BetSymbol)_betSymbols.GetValue(_randomGenerator.Next(_betSymbols.Length));
        }

        OnBettingResultMade?.Invoke(_dicesRollResult.ToList());
    }

    private List<ResultDetail> _resultDetails = new List<ResultDetail>
    {
        new ResultDetail(){ Symbol = BetSymbol.Chidi, Count = 0 },
        new ResultDetail(){ Symbol = BetSymbol.Paan, Count = 0 },
        new ResultDetail(){ Symbol = BetSymbol.Hukum, Count = 0 },
        new ResultDetail(){ Symbol = BetSymbol.Mukut, Count = 0 },
        new ResultDetail(){ Symbol = BetSymbol.Jhandi, Count = 0 },
        new ResultDetail(){ Symbol = BetSymbol.Itti, Count = 0 },
    };
    private List<ResultDetail> _winnerSymbols = new List<ResultDetail>();

    private void ShowResult()
    {
        _resultDetails.ForEach(item => item.Count = 0);

        foreach (BetSymbol symbol in _dicesRollResult)
        {
            var resultDetail = _resultDetails.Find(item => item.Symbol == symbol);
            if (resultDetail != null)
            {
                resultDetail.Count++;
            }
        }

        _winnerSymbols.Clear();
        foreach (var resultDetail in _resultDetails)
        {
            if (resultDetail.Count > 1)
            {
                _winnerSymbols.Add(resultDetail);
            }
        }

        if (_winnerSymbols.Count > 0)
        {
            foreach (var resultDetail in _winnerSymbols)
            {
                var betDetails = _bettingMadeDetails[resultDetail.Symbol];
                foreach (var betDetail in betDetails)
                {
                    if (betDetail == null) continue;
                    int winnings = betDetail.BetAmount * (resultDetail.Count + 1);
                    betDetail.player.SetBetWonDetail(resultDetail.Count, betDetail.BetAmount);
                }
            }
        }
        else
        {
            Debug.Log("No winners this round.");
        }

        OnBettingRoundFinished?.Invoke();
    }
}
