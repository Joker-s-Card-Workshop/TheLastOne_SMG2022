using System.Collections;
using System.Collections.Generic;
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
}
