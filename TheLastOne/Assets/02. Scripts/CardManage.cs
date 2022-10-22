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
    public GameObject CardFramePrefab;    
    public GameObject IllustPrefab;
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
            Data.Add(newdata);
            Debug.Log(Data[i].CardName);
        }
    }
}
