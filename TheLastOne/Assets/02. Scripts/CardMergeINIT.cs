using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMergeData
{
    public  List <string> SourceCard;
    public  List <string> ResultCard;
    public GameObject MergeFX;
}
public class CardMergeINIT : Singleton<CardMergeINIT>
{
    public List<CardMergeData> Data;
    private void Awake()
    {
        this.Init();
        DontDestroyOnLoad(this);
    }
    public void Init()
    {
        Data = new List<CardMergeData>();
        List<Dictionary<string, object>> cardMergeDictionary = CSVReader.Read("Data/CardMerge");
        for (var i = 0; i < cardMergeDictionary.Count; i++)
        {
            var newdata = new CardMergeData();

            newdata.SourceCard = new List<string>();
            newdata.SourceCard.Add(cardMergeDictionary[i]["CardA_Name"].ToString());
            newdata.SourceCard.Add(cardMergeDictionary[i]["CardB_Name"].ToString());
            newdata.MergeFX = Resources.Load("Prefab/FX/" + cardMergeDictionary[i]["MergeEffect"].ToString()) as GameObject;

            string[] words = cardMergeDictionary[i]["ResultCard"].ToString().Split('|');

            newdata.ResultCard = new List<string>();
            for(var j = 0; j < words.Length; j++)
                newdata.ResultCard.Add(words[j]);

            Data.Add(newdata);
        }
    }
}
