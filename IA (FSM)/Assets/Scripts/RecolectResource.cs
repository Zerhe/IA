using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolectResource : MonoBehaviour {

    private IA iA;
    [SerializeField]
    private Transform recolectTarget;
    [SerializeField]
    private Transform depositTarget;
    [SerializeField]
    private float velMov;
    private int cantResource;

    void Awake ()
    {
        iA = GetComponent<IA>();
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
        if(cantResource > 200)
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
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Resource")
            iA.sM.SendEvent(1);
        if (collision.gameObject.tag == "Deposit")
            iA.sM.SendEvent(3);
    }
}
