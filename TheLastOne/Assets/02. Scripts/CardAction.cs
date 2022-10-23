using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
public class CardAction : Singleton<CardAction>
{
    public float mouseDownDist = 0;
    public float mouseUpDist = 0;
    public float cardAniSpeed = 0;
    public int cardAniCount = 0;
    public float cardAniDuration = 0;
    public float cardAniZ = 0;
    public GameObject cardOb;

    [SerializeField]
    private ParticleSystem cardDropPC;
    [SerializeField]
    private ParticleSystem cardMergePC;
    [SerializeField]
    private ParticleSystem bloodMergePCs;
    [SerializeField]
    private ParticleSystem[] levelMergePCs;


    private bool dragDrop = false;
    private bool isBattle = false;
    private bool isDrag = false;
    private bool isHit = false;
    private bool isCombinationable = false;
    private bool isWhileCombination = false;
    private Transform hitT = null;
    private Vector3 cardTransformOriginPos;
    private Transform combinateCard = null;

    private GameObject exFxGO;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        mouseInteraction();
    }

    private void mouseInteraction()
    {
        if (isBattle) return;
        if (isWhileCombination) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        List<CardData> mergedCard;

        if (Input.GetMouseButtonDown(0))
        {
            if (!Physics.Raycast(ray, out hit)) return;
            if (hit.transform.gameObject.tag != "Card") return;
            cardTransformOriginPos = hit.transform.position;
            hitT = hit.transform;
            isDrag = true;
        }
        if (Input.GetMouseButton(0) && isDrag)
        {
            ChangeCardPosToMousePos(hitT, mouseDownDist);

            //Ray dragRay = new Ray(new Vector3(hitT.position.x,hitT.position.y, hitT.position.z + 1), Vector3.forward);
            Vector3 cameraToObj = hitT.transform.position - Camera.main.transform.position;
            RaycastHit dragHit;
            if (Physics.BoxCast(hitT.transform.position, hitT.lossyScale + new Vector3(0, 0, 1), Vector3.forward * 3.0f, out dragHit)
                && dragHit.transform != hitT.transform)
            {
                int rotateZ = -20;
                if (dragHit.collider.transform.CompareTag("Card") /*TODO*/)
                {
                    isHit = true;
                    combinateCard = dragHit.transform;
                    if (true)//TODO
                    {
                        isCombinationable = true;
                    }
                    else goto EXIT;
                    if (hitT.transform.position.x >= dragHit.transform.position.x) rotateZ *= -1;
                    hitT.rotation = Quaternion.Euler(0, rotateZ, 0);
                }
                else
                {
                    hitT.rotation = Quaternion.Euler(0, 0, 0);
                    isHit = false;
                }
            }
            else combinateCard = null;
        }
    EXIT:
        if (Input.GetMouseButtonUp(0))
        {
            Transform target = hitT;

            if (isHit)
            {
                cardDropPC.transform.position = hitT.position + Vector3.back;
                cardDropPC.Play();
            }
            if (!isHit)
            {
                ChangeCardPosToMousePos(hitT, mouseUpDist);
            }
            else if (isCombinationable && combinateCard != null)
            {
                isWhileCombination = true;

                //Do combination
                Vector3 originPos = hitT.transform.position;

                Vector3 dirPos = Vector3.Normalize(combinateCard.position - hitT.transform.position);
                target.DORotate(new Vector3(0, hitT.rotation.eulerAngles.y < 180 ? 60 : -60, 0), 0.2f);
                target.DOMove(originPos - dirPos * 15, 0.23f).OnComplete(() => target.DOMove(combinateCard.position, 0.12f).OnComplete(() =>
                {
                    Camera.main.DOShakePosition(0.4f, 3, 10);
                    Camera.main.DOShakeRotation(0.4f, 5, 10);
                    cardMergePC.transform.position = combinateCard.position + Vector3.back;
                    cardMergePC.Play();
                    SoundManager.Instance.PlaySoundClip("SFX_CardMerge1", SoundType.SFX, 0.8f);
                    SoundManager.Instance.PlaySoundClip("SFX_CardMerge3", SoundType.SFX, 0.1f);
                    isWhileCombination = false;

                    var resultCardData = CardMerge.CardMergeGet(target.GetComponent<CardInfo>().mydata, combinateCard.GetComponent<CardInfo>().mydata);
                    var mergeData = CardMerge.GetMergeData(target.GetComponent<CardInfo>().mydata, combinateCard.GetComponent<CardInfo>().mydata);
                    if (resultCardData != null)
                    {
                        Debug.Log(resultCardData.Count);
                        for (var i = 0; i < resultCardData.Count; i++)
                        {
                            Debug.Log(i);
                            var newCardGO = Instantiate(resultCardData[i].CardPrefab);
                            Debug.Log(newCardGO);
                            newCardGO.GetComponent<CardInfo>().SetCardData(resultCardData[i]);
                            newCardGO.transform.position = target.transform.position;
                        }

                        GameObject.Destroy(exFxGO);
                        exFxGO = Instantiate(mergeData.MergeFX, target.position, Quaternion.identity);

                        Destroy(combinateCard.gameObject);
                        Destroy(target.gameObject);
                        StatusManager.Instance.StartCoroutine(StatusManager.Instance.ClearCheck());
                    }
                }));
            }
            else
            {
                //Back to original the position
                target.transform.DOMove(cardTransformOriginPos, 0.5f);
                target.transform.DORotate(new Vector3(0, 90, 0), 0.5f);
            }
            isHit = false;
            isCombinationable = false;
            isDrag = false;
            hitT = null;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!dragDrop) return;
        dragDrop = false;
        if (other.gameObject.tag != "Card") return;
        float width = other.GetComponent<Renderer>().bounds.size.x;
        Vector3 pos = other.transform.position;

        if (this.transform.position.x <= other.transform.position.x)
        {
            pos.x -= (width + width / 2.0f);
        }
        else
        {
            pos.x += (width + width / 2.0f);
        }
        this.transform.position = pos;

        StartCoroutine(CardUpAni(this.transform, other.transform));
    }

    /* public void OnMouseDown()
     {
         ChangeCardPosToMousePos(mouseDownDist);
     }
     public void OnMouseDrag()
     {
         ChangeCardPosToMousePos(mouseDownDist);
     }

     public void OnMouseUp()
     {
         ChangeCardPosToMousePos(mouseUpDist);
         dragDrop = true;
     }*/

    /// <summary>
    /// Change a card position to mouse position
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="dist"></param>
    private void ChangeCardPosToMousePos(Transform tr, float dist)
    {
        if (isBattle || !tr) return;
        float cameraZ = Camera.main.transform.position.z;
        dist -= cameraZ;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        tr.position = worldPosition;
    }

    /// <summary>
    /// Card Battle Animation Up
    /// </summary>
    /// <param name="t1"> Card1 Transform </param>
    /// <param name="t2"> Card2 Transform </param>
    /// <returns></returns>
    IEnumerator CardUpAni(Transform t1, Transform t2)
    {
        isBattle = true;
        var tween = t1.DOMoveZ(t1.position.z - cardAniZ, cardAniDuration).SetEase(Ease.Linear);
        t2.DOMoveZ(t2.position.z - cardAniZ, cardAniDuration).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        StartCoroutine(CardBattleAni(t1, t2));
        yield break;
    }

    /// <summary>
    /// Card Battle Animation Down
    /// </summary>
    /// <param name="t1"> Card1 Transform </param>
    /// <param name="t2"> Card2 Transform </param>
    /// <returns></returns>
    IEnumerator CardDownAni(Transform t1, Transform t2)
    {
        var tween = t1.DOMoveZ(t1.position.z + cardAniZ, cardAniDuration).SetEase(Ease.Linear);
        t2.DOMoveZ(t2.position.z + cardAniZ, cardAniDuration).SetEase(Ease.Linear);
        yield return tween.WaitForCompletion();
        isBattle = false;
        yield break;
    }

    /// <summary>
    /// Card Battle Animation Batttle
    /// </summary>
    /// <param name="t1"> Card1 Transform </param>
    /// <param name="t2"> Card2 Transform </param>
    /// <returns></returns>
    IEnumerator CardBattleAni(Transform t1, Transform t2)
    {
        //TODO
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(CardDownAni(t1, t2));
        yield break;
    }

    /*
    =======
    using Unity.VisualScripting;
    using UnityEngine;
    using DG.Tweening;
    using AmplifyShaderEditor;
    using UnityEditor;

    public class CardAction : MonoBehaviour
    {
        public float moveDuration = 1;

        [SerializeField]
        private Transform cardPos;
        private void Start()
        {
            CardAttack(cardPos.position);
        }
        private void Update()
        {
        }
        void CardAttack(Vector3 pos)
        {
            Vector3 originPos = transform.position;

            transform.DOMove(pos, moveDuration / 2).SetEase(Ease.InBack).OnComplete(() => { transform.DOMove(originPos, moveDuration / 2); });
        }
    */
}
