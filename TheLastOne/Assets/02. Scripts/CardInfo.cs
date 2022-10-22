using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CardInfo : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _frameMeshRenderer;
    [SerializeField]
    private MeshRenderer _backMeshRenderer;
    [SerializeField]
    private Transform _illustParentTR;

    public float gravityScale = 98;

    public CardData mydata;

    private Rigidbody rigidbody;
    private Dictionary<string, Material> mt = new Dictionary<string, Material>();
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.forward * gravityScale * Time.deltaTime, ForceMode.Impulse);
    }
    public void SetCardData(CardData cardData)
    {
        mydata = cardData;

        _frameMeshRenderer.material.SetTexture("_MainTex", cardData.frameTexture);
        _backMeshRenderer.material.SetTexture("_MainTex", cardData.backTexture);

        var illustGO = Instantiate(cardData.IllustPrefab, _illustParentTR);
        illustGO.transform.localPosition = Vector3.zero;
    }

}
