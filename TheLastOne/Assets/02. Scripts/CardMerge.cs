using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardMerge
{ 
    public static List<CardData> FindCardData(List<string> result)
    {
        List<CardData> cardData = CardDataINIT.Instance.Data;
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
        List<CardMergeData> MergeData = CardMergeINIT.Instance.Data;
        for (var i = 0; i < MergeData.Count; i++)
        {
            if (MergeData[i].SourceCard[0] == cardA.CardName)
            {
                if (MergeData[i].SourceCard[1] == cardB.CardName)
                {
                    return FindCardData(MergeData[i].ResultCard);
                }
            }

            if (MergeData[i].SourceCard[0] == cardB.CardName)
            {
                if (MergeData[i].SourceCard[1] == cardA.CardName)
                {
                    return FindCardData(MergeData[i].ResultCard);
                }
            }
        }
        return null;
    }
    public static CardMergeData GetMergeData(CardData cardA, CardData cardB)
    {
        List<CardMergeData> MergeData = CardMergeINIT.Instance.Data;
        for (var i = 0; i < MergeData.Count; i++)
        {
            if (MergeData[i].SourceCard[0] == cardA.CardName)
            {
                if (MergeData[i].SourceCard[1] == cardB.CardName)
                {
                    return MergeData[i];
                }
            }

            if (MergeData[i].SourceCard[0] == cardB.CardName)
            {
                if (MergeData[i].SourceCard[1] == cardA.CardName)
                {
                    return MergeData[i];
                }
            }
        }
        return null;
    }
}