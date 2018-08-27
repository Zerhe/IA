using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecolectResource : MonoBehaviour {

    private IA iA;
    private Text resourceText;
    [SerializeField]
    private Transform recolectTarget;
    [SerializeField]
    private Transform depositTarget;
    [SerializeField]
    private float velMov;
    private int cantResource;
    private string tipeResource;

    void Awake ()
    {
        iA = GetComponent<IA>();
        resourceText = GameObject.Find("GoldText").GetComponent<Text>();
	}
    void Start()
    {
        iA.sM.SendEvent(0);
    }
    void Update ()
    {
        print(cantResource);
		switch(iA.sM.GetState())
        {
            case 1:
                MovToRecolect();
                break;
            case 2:
                Recolect();
                break;
            case 3:
                MovToDeposit();
                break;
            case 4:
                Deposit();
                break;
            default:
                break;
        }
        if(cantResource == 200)
            iA.sM.SendEvent(2);
        if(cantResource < 0)
        {
            cantResource = 0;
            iA.sM.SendEvent(0);
        }

    }
    void MovToRecolect()
    {
        transform.Translate(Direction.CalculateDirection(recolectTarget.position, transform.position) * Time.deltaTime * velMov);
    }
    void Recolect()
    {
        cantResource++;
    }
    void MovToDeposit()
    {
        transform.Translate(Direction.CalculateDirection(depositTarget.position, transform.position) * Time.deltaTime * velMov * 0.5f);
    }
    void Deposit()
    {
        cantResource--;
        resourceText.text = "" + (199 - cantResource);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)     // 8 = Resource
        {
            iA.sM.SendEvent(1);
            tipeResource = collision.gameObject.tag;
        }
        if (collision.gameObject.tag == "Deposit")
            iA.sM.SendEvent(3);
    }
}
