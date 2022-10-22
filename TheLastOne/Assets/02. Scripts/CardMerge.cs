using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardMerge
{ 
    public static List<CardData> FindCardData(List<string> result)
    {
        List<CardData> cardData = CardDataInit.Instance.Data;
        List<CardData> returndata = new List<CardData>();
        for (var i = 0; i < result.Count; i++)
        {
            for (int k = 0; k < cardData.Count; k++)
            {
                if (result[i] == cardData[k].CardName)
                    returndata.Add(cardData[k]);
            }
        }
        return returndata;
    }
    public static List<CardData> CardMergeGet(CardData cardA, CardData cardB)
    {
        List<CardMergeData> MergeData = CardMergeInit.Instance.Data;
        for (var i = 0; i < MergeData.Count; i++)
        {
            if (MergeData[i].SourceCard[0] == cardA.CardName)
            {
                if (MergeData[i].SourceCard[1] == cardB.CardName)
                {
                    return FindCardData(MergeData[i].ResultCard);
                }
            }
            else if (MergeData[i].SourceCard[0] == cardB.CardName)
            {
                if (MergeData[i].SourceCard[1] == cardA.CardName)
                {
                    return FindCardData(MergeData[i].ResultCard);
                }
            }
        }
        return null;
    }
}