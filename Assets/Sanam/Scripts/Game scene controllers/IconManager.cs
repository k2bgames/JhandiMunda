using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "KhorKhoreIconList",
    menuName = "Menu/KhorKhoreIconList"
)]
public class KhorKhoreIconsList : ScriptableObject
{
    public List<KhorKhoreIcon> KhorKhoreIcons = new List<KhorKhoreIcon>()
         {
             new KhorKhoreIcon(){ Symbol = BetSymbol.Chidi, IconColor = Color.grey },
             new KhorKhoreIcon(){ Symbol = BetSymbol.Mukut, IconColor = Color.yellowGreen },
             new KhorKhoreIcon(){ Symbol = BetSymbol.Hukum, IconColor = Color.black },
             new KhorKhoreIcon(){ Symbol = BetSymbol.Itti, IconColor = Color.red },
             new KhorKhoreIcon(){ Symbol = BetSymbol.Jhandi, IconColor = Color.violetRed },
             new KhorKhoreIcon(){ Symbol = BetSymbol.Paan, IconColor = Color.darkRed },
         };
}


public class IconManager : Singleton<IconManager, Exisiting>
{
    public KhorKhoreIconsList KhorKhoreIconsSO;
    public List<KhorKhoreIconButtons> KhorKhoreIconsButtons;
    public Dictionary<BetSymbol, Color> KhorKhoreIconColorDict = new Dictionary<BetSymbol, Color>();

    public void InitializedButtons()
    {
        for (int i = 0; i < KhorKhoreIconsButtons.Count; i++)
        {
            KhorKhoreIconsButtons[i].Initialize(KhorKhoreIconsSO.KhorKhoreIcons[i]);
            KhorKhoreIconColorDict.Add(
                KhorKhoreIconsSO.KhorKhoreIcons[i].Symbol,
                KhorKhoreIconsSO.KhorKhoreIcons[i].IconColor
            );
        }
    }
}