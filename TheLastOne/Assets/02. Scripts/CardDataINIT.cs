using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Xml.Schema;

public class CardData
{
    public string CardName;
    public string CardNameKor;

    public enum Season
    {
        Male,
        Female,
        Thing
    }
    public Season type;
    public GameObject IllustPrefab;
    public GameObject CardPrefab;

    public Texture2D frameTexture;
    public Texture2D backTexture;
}
public class CardDataINIT : Singleton<CardDataINIT>
{
    public List<CardData> Data;
    //Start is called before the first frame update
    private void Awake()
    {
        this.Init();
        DontDestroyOnLoad(this);
    }
    public void Init()
    {
        Data = new List<CardData>();
        List<Dictionary<string, object>> cardDictionary = CSVReader.Read("Data/Card");
        for (var i = 0; i < cardDictionary.Count; i++)
        {
            var newdata = new CardData();
            newdata.CardName = cardDictionary[i]["CardName"].ToString();
            newdata.CardNameKor = cardDictionary[i]["CardName_Kor"].ToString();
            newdata.IllustPrefab = Resources.Load ( "Prefab/Illust/" + cardDictionary[i]["IllustPrefab"].ToString()) as GameObject;
            newdata.CardPrefab = Resources.Load ("Prefab/Card") as GameObject;
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
            newdata.frameTexture = Resources.Load("Sprites/Illusts/" + cardDictionary[i]["CardFrameSprite"].ToString()) as Texture2D;
            newdata.backTexture = Resources.Load("Sprites/Illusts/" + cardDictionary[i]["CardBackSprite"].ToString()) as Texture2D;
            Data.Add(newdata);
        }
    }
}
