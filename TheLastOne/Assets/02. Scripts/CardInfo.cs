using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _frameMeshRenderer;
    [SerializeField]
    private MeshRenderer _backMeshRenderer;

    public CardData mydata;

    public void SetCardData(CardData cardData)
    {
        mydata = cardData;

        //_frameMeshRenderer.material.mainTexture = cardData

    }

}
