using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public static class BotNames
{
    public static List<string> Names = new List<string>()
    {
        "RoboGambler",
        "AI_Wagerer",
        "BetBot3000",
        "CyberPunter",
        "DigitalHighRoller",
        "QuantumBettor",
        "PixelPlayer",
        "NanoGambler",
        "VirtualWager",
        "AlgorithmicAce"
    };
}

public class PlayerCreationManager : Singleton<PlayerCreationManager, Exisiting>
{
    public int TotalPlayers = 4;
    public List<IPlayer> Players;
    public PlayerUIManager PlayerUIManager;
    public Transform BotDetailsParent;
    public UIBetDetailDisplay UIBetDetailPrefab;

    private List<UIBetDetailDisplay> BetDisplayInfos = new List<UIBetDetailDisplay>();
    private List<string> _unusedNames = new List<string>(BotNames.Names);
    private List<string> _usedNames = new List<string>();

    public void CreatePlayers()
    {
        if (Players != null) return;

        Players = new List<IPlayer>();
        CreateBots();
        CreateUserPlayer();
    }

    private void CreateBots()
    {
        for (int i = 0; i < TotalPlayers; i++)
        {
            UIBetDetailDisplay betUI = Instantiate(UIBetDetailPrefab, BotDetailsParent);
            BetDisplayInfos.Add(betUI);
            betUI.gameObject.SetActive(true);

            BotPlayer newPlayer = new BotPlayer(GetUnusedBotName(), betUI);
            Players.Add(newPlayer);
        }
    }

    private void CreateUserPlayer()
    {
        UIBetDetailDisplay playerUI = Instantiate(UIBetDetailPrefab, BotDetailsParent);
        BetDisplayInfos.Add(playerUI);
        playerUI.gameObject.SetActive(true);

        UserPlayerController userPlayer = new UserPlayerController(playerUI);
        Players.Add(userPlayer);
        PlayerUIManager.AssignPlayer(userPlayer);
    }


    string tempNameVar;
    private string GetUnusedBotName()
    {
        tempNameVar = _unusedNames[Random.Range(0, _unusedNames.Count)];
        _unusedNames.Remove(tempNameVar);
        _usedNames.Add(tempNameVar);
        return tempNameVar;
    }
}
