using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public BetManager BetManager;
    public GameObject GameResultPanel;
    public GameObject DiceBoard;
    public List<Image> Dices;

    private void OnEnable()
    {
        BetManager.OnBettingResultMade += ShowDiceBoardResult;
    }

    public void ShowDiceBoardResult(List<BetSymbol> result)
    {
        DiceBoard.SetActive(true);
        for(int i = 0; i < Dices.Count; i++)
        {
            Dices[i].color = IconManager.Instance.KhorKhoreIconColorDict[result[i]];
        }
    }

    public void HideDiceDesult()
    {
        DiceBoard.SetActive(false);
    }
}
