using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    

    public CardData mydata;
    Dictionary<string, Material> mt = new Dictionary<string, Material>();
    private void Start()
    {
        //foreach(Transform child in this.transform)
        //{
        //    mt[child.name] = child.GetComponent<Material>();
        //}
    }

    public void SetCardData(CardData cardData)
    {
        mydata = cardData;
    }

}
