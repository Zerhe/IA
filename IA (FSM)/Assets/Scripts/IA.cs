using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {

    public FSM sM;
    [SerializeField]
    private int cantStates;
    [SerializeField]
    private int cantEvents;

    void Awake()
    {
        sM = new FSM(cantStates, cantEvents);
     /*
      * States
      * 0 = Idle
      * 1 = MovToRecolect
      * 2 = Recolect
      * 3 = MovToDeposit
      * 4 = Deposit
      * 
      * Events
      * 0 = InvetEmpty
      * 1 = ReadyToRecolect
      * 2 = InvetFull
      * 3 = ReadyToDeposit
     */
        sM.SetRelation(0, 0, 1);
        sM.SetRelation(1, 1, 2);
        sM.SetRelation(2, 2, 3);
        sM.SetRelation(3, 3, 4);
        sM.SetRelation(4, 0, 1);
    }

    void Start ()
    {

    }

    void Update () {

    }
}
