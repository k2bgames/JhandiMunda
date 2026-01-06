using UnityEngine;

public class BotPlayer : IPlayer
{
    public string PlayerName { private set; get; }
    public UIBetDetailDisplay DisplayInfo;

    public BotPlayer(string name, UIBetDetailDisplay info)
    {
        PlayerName = name;
        DisplayInfo = info;
        DisplayInfo.SetBotName(PlayerName);
    }


    bool noMood;
    BetDetail betDetail;
    (BetDetail, BetSymbol) betDetailAndSymbol;
    private (BetDetail, BetSymbol) GenerateBet()
    {
        noMood = Random.Range(0, 10) < 2; // 20% chance of no mood to bet

        if (!noMood)
        {
            if (betDetail == null)
                betDetail = new BetDetail() { PlayerName = this.PlayerName, player = this };
            betDetail.BetAmount = (int)(Random.Range(GameDetails.MinBetAmount, GameDetails.MaxBetAmount + 1) / 5) * 5; // rounding to lowest of multiple of 5
            betDetailAndSymbol = (betDetail, (BetSymbol)Random.Range(0, System.Enum.GetValues(typeof(BetSymbol)).Length));
            DisplayInfo.SetBetDetail(betDetail.BetAmount, IconManager.Instance.KhorKhoreIconColorDict[betDetailAndSymbol.Item2]);
            return betDetailAndSymbol;
        }
        else
        {
            DisplayInfo.SetBetDetail(0, Color.white);
            return (null, 0);
        }
    }


    public void PlaceBet(System.Action<(BetDetail, BetSymbol), IPlayer> decisionCallback)
    {
        decisionCallback.Invoke(GenerateBet(), this);
    }

    public void ClearUIDetails()
    {
        DisplayInfo.ResetDetail();
    }

    public void SetBetWonDetail(int multipliedBy, int totalAmount)
    {
        DisplayInfo.SetBetWonDetail(multipliedBy, totalAmount);
    }
}
