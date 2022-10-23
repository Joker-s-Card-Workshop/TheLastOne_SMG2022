using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StageData
{
    public List<string> cardNames = new List<string>();
}
public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardOriginPrefab;
    [SerializeField]
    private CardAction cardAction;
    [SerializeField]
    private ParticleSystem dropPS;

    [SerializeField]
    private StageData[] stageData = new StageData[6];
    [SerializeField]
    private Material[] stageBackGround;

    List<GameObject> cardList = new List<GameObject>();
    List<CardData> cardDatas;
    private const int testCnt = 10;
    Vector3 size;
    float duration = 1;
    private void Start()
    {
        dropPS.Stop();
        size = MakeBoundary();
        cardDatas = CardDataINIT.Instance.Data;

        if (StatusManager.Instance != null)
        {
            GetComponent<MeshRenderer>().material = stageBackGround[StatusManager.Instance.stageIndex];
            StartGame(stageData[StatusManager.Instance.stageIndex].cardNames);
        }
        else
            StartGame(stageData[1].cardNames);
    }
    public void StartGame(List<string> cardNameList)
    {
        List<GameObject> cardObj = getCardObject(cardNameList);
        StartCoroutine(PrepareGame(cardObj));
    }
    IEnumerator PrepareGame(List<GameObject> cardObj)
    {
        yield return null;

        cardAction.enabled = false;
        for (var i = 0; i < cardObj.Count; i++)
        {
            cardList.Add(cardObj[i]);
            cardList[cardList.Count - 1].transform.position = new Vector3(0, 0, this.transform.position.z - i);
            cardList[cardList.Count - 1].transform.rotation = Quaternion.Euler(0, 180, 0);
            cardList[cardList.Count - 1].GetComponent<Rigidbody>().isKinematic = true;
        }
        StartCoroutine(MoveCard());
    }

    IEnumerator MoveCard()
    {
        for (int i = 0; i < cardList.Count - 1; i++)
        {
            Vector3 v = new Vector3(
                Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), size.z);
            var tween = cardList[i].transform.DOMove(v, duration).SetEase(Ease.Linear);
            var tween2 = cardList[i].transform.DORotate(new Vector3(0, 0, 0), duration);
            yield return tween.WaitForCompletion();
            yield return tween2.WaitForCompletion();
            cardList[i].GetComponent<Rigidbody>().isKinematic = false;
        }
        var tween3 = cardList[cardList.Count - 1].transform.DOMove(new Vector3(0, 0, this.transform.position.z - 10), duration).SetEase(Ease.Linear);
        var tween4 = cardList[cardList.Count - 1].transform.DORotate(new Vector3(0, 0, 0), duration);
        yield return tween3.WaitForCompletion();
        yield return tween4.WaitForCompletion();
        cardList[cardList.Count - 1].GetComponent<Rigidbody>().isKinematic = false;
        cardAction.enabled = true;
        yield break;

    }

    Vector3 MakeBoundary()
    {
        Vector3 center = this.transform.position;
        Vector3 size = this.GetComponent<BoxCollider>().bounds.size;
        float ydelta = this.GetComponent<BoxCollider>().bounds.center.y;
        Vector3 cardSize = cardOriginPrefab.GetComponent<BoxCollider>().size;
        Vector3 A, B, C, D;

        A = center + this.transform.right * (size.x / 2 - cardSize.x / 2) - this.transform.forward * (size.y / 2 - cardSize.y);
        B = center - this.transform.right * (size.x / 2 - cardSize.x / 2) - this.transform.forward * (size.y / 2 - cardSize.y);
        C = center - this.transform.right * (size.x / 2 - cardSize.x / 2) + this.transform.forward * (size.y / 2 - cardSize.y / 2);
        D = center + this.transform.right * (size.x / 2 - cardSize.x / 2) + this.transform.forward * (size.y / 2 - cardSize.y / 2);
        A.y += ydelta;
        B.y += ydelta;
        C.y += ydelta;
        D.y += ydelta;

        return new Vector3(A.x * 2, C.y * 2, this.transform.position.z - 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        dropPS.transform.position = collision.transform.position;
        dropPS.Play();
        SoundManager.Instance.PlaySoundClip("SFX_Card_Drop", SoundType.SFX, 0.8f);
    }

    List<GameObject> getCardObject(List<string> cards)
    {
        CardData card;
        GameObject newCard;
        List<GameObject> cardObj = new List<GameObject>();
        for (int i = 0; i < cards.Count; i++)
        {
            card = cardDatas.Find(str => str.CardName.CompareTo(cards[i]) == 0);
            if (card == null) continue;
            newCard = Instantiate(card.CardPrefab);
            newCard.GetComponent<CardInfo>().SetCardData(card);
            cardObj.Add(newCard);
        }
        return cardObj;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Main");
        MainSceneUI.instance.OpenSelectStageScreen();
    }

}
