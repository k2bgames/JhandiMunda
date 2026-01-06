using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : Singleton<PlayerUIManager, Exisiting>
{
    public List<Button> Dices;
    public GameObject PlaceBetsLabel;
    public GameObject AmountSelectPanel;
    //public GameObject ShuffleButton;
    public TMP_Text AmountLabel;
    public Slider AmountSlider;

    public int MinBetAmount = 0;
    public int MaxBetAmount = 500;

    public UserPlayerController PlayerController;

    protected override void Awake()
    {
        base.Awake();

        EnableDices(false);
        _betAmount = MinBetAmount;
        AmountSlider.minValue = MinBetAmount;
        AmountSlider.maxValue = MaxBetAmount;
        AmountSlider.value = _betAmount;
        AmountLabel.text = _betAmount.ToString();
    }

    public void AssignPlayer(UserPlayerController player)
    {
        PlayerController = player;
        PlayerController.OnBettingStarted += MakePlayerBettableSituation;
    }

    private void MakePlayerBettableSituation()
    {
        MakeDicesSelectable(true);
    }

    private void EnableDices(bool enable = true)
    {
        foreach (var dice in Dices)
        {
            dice.interactable = enable;
        }
    }

    private void MakeDicesSelectable(bool selectable = true)
    {
        //ShuffleButton.SetActive(selectable);
        PlaceBetsLabel.SetActive(selectable);
        EnableDices(selectable);
    }

    private BetSymbol _selectedSymbol;
    private int _betAmount;
    public void ClickedSymbol(int symbol)
    {
        _selectedSymbol = (BetSymbol)symbol;
        AmountSelectPanel.SetActive(true);
    }

    public void OnSlidingAmountChanged()
    {
        _betAmount = (int)AmountSlider.value;
        AmountLabel.text = _betAmount.ToString();
    }

    public void ClickedAmount()
    {
        AmountSelectPanel.SetActive(false);
        MakeDicesSelectable(false);
        PlayerController.OnUserBetSubmission(_betAmount, _selectedSymbol);
    }

    // to make after multiple bets to be allowed by the player
    //public void Shuffle()
    //{
    //    MakeDicesSelectable(false);
    //    PlayerController.
    //}
}
