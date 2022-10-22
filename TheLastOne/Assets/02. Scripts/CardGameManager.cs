using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardOriginPrefab;
    [SerializeField]
    private CardAction cardAction;

    public void StartGame(List<string> cardNameList)
    {
        StartCoroutine(PrepareGame(cardNameList));
    }

    IEnumerator PrepareGame(List<string> cardNameList)
    {
        yield return null;

        for(var i = 0; i < cardNameList.Count; i++)
        {
        }
    }
}
