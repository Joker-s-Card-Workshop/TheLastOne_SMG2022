using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMergeData
{
    public  List <string> CardData;
    public  List <string> ResultCard;
}
public class CardMergeInit : Singleton<CardMergeInit>
{
    public List<CardMergeData> Data;
    public void Init()
    {
        Data = new List<CardMergeData>();
        List<Dictionary<string, object>> cardMergeDictionary = CSVReader.Read("Data/Card");
        for (var i = 0; i < cardDictionary.Count; i++)
        {
    }
}
