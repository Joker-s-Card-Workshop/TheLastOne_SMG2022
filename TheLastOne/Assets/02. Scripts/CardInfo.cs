using JetBrains.Annotations;
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
    //�߷°��ӵ� 9.8�� �ٻ�ġ�� 10
    public float gravityScale = 98;

    public CardData mydata;

    private Rigidbody rigidbody;
    private Dictionary<string, Material> mt = new Dictionary<string, Material>();
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //foreach(Transform child in this.transform)
        //{
        //    mt[child.name] = child.GetComponent<Material>();
        //}
    }
    public void FixedUpdate()
    {
        rigidbody.AddForce(Vector3.forward * gravityScale * Time.deltaTime, ForceMode.Impulse);
    }
    public void SetCardData(CardData cardData)
    {
        mydata = cardData;

        //_frameMeshRenderer.material.mainTexture = cardData

    }

}
