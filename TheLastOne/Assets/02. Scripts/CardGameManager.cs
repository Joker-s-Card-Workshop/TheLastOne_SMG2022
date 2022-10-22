using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cardOriginPrefab;
    [SerializeField]
    private CardAction cardAction;

    List<GameObject> cardList = new List<GameObject>();
    
    private const int testCnt = 5;
    Vector3 size;
    float ydelta;
    private void Start()
    {
        size = this.GetComponent<BoxCollider>().bounds.size;
        ydelta = this.GetComponent<BoxCollider>().bounds.center.y;
        size.y += ydelta;
        MakeBoundary();
        StartGame(null);
    }
    public void StartGame(List<string> cardNameList)
    {
        StartCoroutine(PrepareGame(cardNameList));
    }

    IEnumerator PrepareGame(List<string> cardNameList)
    {
        yield return null;

        cardAction.enabled = false;
        for(var i = 0; i < testCnt; i++)
        {
            cardList.Add(Instantiate(cardOriginPrefab));
            cardList[cardList.Count - 1].transform.position = new Vector3(
                Random.Range(-size.x/2, size.x/2), Random.Range(-size.y/2, size.y/2), this.transform.position.z - 10);
        }

        //StartCoroutine(CardSetting());
    }

    private void Update()
    {
        //MakeBoundary();
    }

    void MakeBoundary()
    {
        Vector3 center = this.transform.position;
        Vector3 size = this.GetComponent<BoxCollider>().bounds.size;
        float ydelta = this.GetComponent<BoxCollider>().bounds.center.y;
        Vector3 A, B, C, D;

        A = center + this.transform.right * size.x / 2 - this.transform.forward * size.y / 2;
        B = center - this.transform.right * size.x / 2 - this.transform.forward * size.y / 2;
        C = center - this.transform.right * size.x / 2 + this.transform.forward * size.y / 2;
        D = center + this.transform.right * size.x / 2 + this.transform.forward * size.y / 2;
        A.y += ydelta;
        B.y += ydelta;
        C.y += ydelta;
        D.y += ydelta;

        Debug.DrawLine(A, B, Color.red);
        Debug.DrawLine(B, C, Color.red);
        Debug.DrawLine(C, D, Color.red);
        Debug.DrawLine(D, A, Color.red);

       /* t1 = getAreaOfTriangle(point, A, B);
        t2 = getAreaOfTriangle(point, A, C);
        t3 = getAreaOfTriangle(point, B, D);
        t4 = getAreaOfTriangle(point, C, D);*/
    }

    float getAreaOfTriangle(Vector3 dot1, Vector3 dot2, Vector3 dot3)
    {
        Vector3 a = dot2 - dot1;
        Vector3 b = dot3 - dot1;
        Vector3 cross = Vector3.Cross(a, b);

        return cross.magnitude / 2.0f;
    }
}
