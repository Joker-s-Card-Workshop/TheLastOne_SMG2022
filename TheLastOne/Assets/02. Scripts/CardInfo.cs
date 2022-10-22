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

        Debug.Log(cardData.frameTexture);
        _frameMeshRenderer.material.SetTexture("_MainTex", cardData.frameTexture);
        _backMeshRenderer.material.SetTexture("_MainTex", cardData.backTexture);
    }

}
