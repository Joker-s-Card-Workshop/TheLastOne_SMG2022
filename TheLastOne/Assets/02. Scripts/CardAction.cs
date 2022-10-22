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
    private bool dragDrop = false;
    private bool isBattle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
        isBattle = true;
        StartCoroutine(CardUpAni(this.transform, other.transform));
    }

    public void OnMouseDown()
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
    }

    /// <summary>
    /// Change a card position to mouse position
    /// </summary>
    /// <param name="dist">mouse Z postion</param>
    private void ChangeCardPosToMousePos(float dist)
    {
        if (isBattle) return;
        float cameraZ = Camera.main.transform.position.z;
        dist -= cameraZ;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        this.transform.position = worldPosition;
    }

    /// <summary>
    /// Card Battle Animation Up
    /// </summary>
    /// <param name="t1"> Card1 Transform </param>
    /// <param name="t2"> Card2 Transform </param>
    /// <returns></returns>
    IEnumerator CardUpAni(Transform t1, Transform t2)
    {
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
