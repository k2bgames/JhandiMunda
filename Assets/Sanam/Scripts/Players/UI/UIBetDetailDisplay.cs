using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBetDetailDisplay : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI playerBetText;
    public Image betIconSelected;
    public GameObject betMulitplierInfo;
    public TextMeshProUGUI betWinMultiplierText;

    public void SetBotName(string playerName)
    {
        playerNameText.text = playerName;
        ResetDetail();
    }

    private Color defaultColor = Color.white;
    private Color selectedColor;
    public void SetBetDetail(int betAmount, Color icon)
    {
        selectedColor = icon != defaultColor? icon : defaultColor;
        if(icon == defaultColor)
            selectedColor.a = 0;
        betIconSelected.color = selectedColor;
        playerBetText.text = betAmount > 0 ? betAmount.ToString() : "-";
    }

    public void ResetDetail()
    {
        selectedColor = defaultColor;
        selectedColor.a = 0;
        betIconSelected.color = selectedColor;
        playerBetText.text = "-";
        betMulitplierInfo.SetActive(false);
    }

    public void SetBetWonDetail(int multipliedBy, int totalAmount)
    {
        betMulitplierInfo.SetActive(true);
        betWinMultiplierText.text = "X" + multipliedBy.ToString();
        playerBetText.text = totalAmount.ToString() + " -> " + (totalAmount * (multipliedBy + 1)).ToString();
    }
}
