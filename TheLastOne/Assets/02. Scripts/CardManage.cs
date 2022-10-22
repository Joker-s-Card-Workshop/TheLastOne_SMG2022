using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using AmplifyShaderEditor;

public class CardData
{
    public string CardName;

    public enum Season
    {
        Male,
        Female,
        Thing
    }
    public Season type;
    public GameObject IllustPrefab;
    public GameObject CardPrefab;
}
public class CardManage : Singleton<CardManage>
{
    public List<CardData> Data;
    //Start is called before the first frame update
    public void Init()
    {
        Data = new List<CardData>();
        List<Dictionary<string, object>> cardDictionary = CSVReader.Read("Data/Card");
        for (var i = 0; i < cardDictionary.Count; i++)
        {
            var newdata = new CardData();
            newdata.CardName = cardDictionary[i]["CardName"].ToString();
            newdata.IllustPrefab = Resources.Load ( "Prefab/Illust" + cardDictionary[i]["IllustPrefab"].ToString()) as GameObject;
            newdata.CardPrefab = Resources.Load ( "Prefab/Card/CardPrefab") as GameObject;
            switch (cardDictionary[i]["CharacterType"].ToString())
            {
                case "Male":
                    newdata.type = CardData.Season.Male;
                    break;
                case "Female":
                    newdata.type = CardData.Season.Female;
                    break;
                case "Thing":
                    newdata.type = CardData.Season.Thing;
                    break; 
            } 
            Debug.Log("Prefab/Illust" + cardDictionary[i]["IllustPrefab"].ToString());
            Data.Add(newdata);
        }
    }
}
