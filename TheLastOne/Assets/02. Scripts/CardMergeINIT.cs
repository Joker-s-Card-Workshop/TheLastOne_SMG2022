using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMergeData
{
    public  List <string> SourceCard;
    public  List <string> ResultCard;
}
public class CardMergeInit : Singleton<CardMergeInit>
{
    public List<CardMergeData> Data;
    public void Init()
    {
        Data = new List<CardMergeData>();
        List<Dictionary<string, object>> cardMergeDictionary = CSVReader.Read("Data/Card");
        for (var i = 0; i < cardMergeDictionary.Count; i++)
        {
            var newdata = new CardMergeData();
            newdata.SourceCard.Add(cardMergeDictionary[i]["CardA_Name"].ToString());
            newdata.SourceCard.Add(cardMergeDictionary[i]["CardB_Name"].ToString());
            string[] words = cardMergeDictionary[i]["CardB_Name"].ToString().Split('|');
            newdata.ResultCard.Add(words[0]);
            newdata.ResultCard.Add(words[1]);
            
        }
    }
}
