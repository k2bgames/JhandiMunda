using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KhorKhoreIconButtons : MonoBehaviour
{
    public TextMeshProUGUI KhorIconText;
    public Image IconImage;
    public BetSymbol BetSymbol;
    public Color DisplayColor;

    public void Initialize(KhorKhoreIcon iconDetail)
    {
        KhorIconText = GetComponentInChildren<TextMeshProUGUI>();
        IconImage = GetComponent<Image>();
        BetSymbol = iconDetail.Symbol;
        KhorIconText.text = BetSymbol.ToString();
        DisplayColor = iconDetail.IconColor;
        IconImage.color = DisplayColor;
    }

    public void OnClick()
    {
        PlayerUIManager.Instance.ClickedSymbol((int)BetSymbol);
    }
}
