using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CardAction : MonoBehaviour
{
    public float mouseDownDist = 0;
    public float mouseUpDist = 0;
    public float cardAniSpeed = 0;
    public int cardAniCount = 0;
    public float cardAniDuration = 0;
    public float cardAniZ = 0;
    public GameObject cardOb;
    private bool dragDrop = false;
    private bool isBattle = false;
    private bool isDrag = false;
    private bool isHit = false;
    private bool isCombinationable = false;
    private Transform hitT = null;
    private Transform cardTransform;
    // Start is called before the first frame update
    void Start()
    {
        cardTransform = cardOb.transform;
    }

    // Update is called once per frame
    void Update()
    {
        mouseInteraction();
    }

    private void mouseInteraction()
    {
        if (isBattle) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            if (!Physics.Raycast(ray, out hit)) return;
            if (hit.transform.gameObject.tag != "Card") return;
            hitT = hit.transform;
            isDrag = true;
        }
        if (Input.GetMouseButton(0) && isDrag)
        {
            ChangeCardPosToMousePos(hitT, mouseDownDist);
            
            //Ray dragRay = new Ray(new Vector3(hitT.position.x,hitT.position.y, hitT.position.z + 1), Vector3.forward);
            RaycastHit dragHit;
            if (Physics.BoxCast(hitT.transform.position, hitT.lossyScale / 1.5f, transform.forward, out dragHit))
            {
                
                int rZ = -20;
                if(dragHit.transform.gameObject.tag == "Card" /*TODO*/)
                {
                    isHit = true;
                    
                    if(true)//TODO
                    {
                        isCombinationable = true;
                    }
                    
                    if (hitT.transform.position.x <= dragHit.transform.position.x) rZ *= -1;
                    hitT.rotation = Quaternion.Euler(cardTransform.eulerAngles.x, cardTransform.eulerAngles.y, rZ);
                }
                else
                {
                    hitT.rotation = Quaternion.Euler(cardTransform.eulerAngles.x, cardTransform.eulerAngles.y, 0);
                    isHit = false;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!isHit)
            {
                ChangeCardPosToMousePos(hitT, mouseUpDist);
            }
            if (isCombinationable)
            {
                //Do combination
            }
            else
            {
                //Back to original the position
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
