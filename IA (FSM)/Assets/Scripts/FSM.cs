using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

    int[,] fsm;
    int state;
    
    public FSM(int cantStates, int cantEvents)         // filas, columnas
    {
        fsm = new int[cantStates, cantEvents];
        state = 0;

        for(int i =0; i < cantStates; i++)
        {
            for (int j = 0; j < cantEvents; j++)
            {
                fsm[i, j] = -1;
            }
        }
    }

    public void SetRelation(int stateOrigin, int evt, int stateDst)
    {
	    fsm[stateOrigin, evt] = stateDst;
    }

    public int GetState()
    {
        return state;
    }
    public void SendEvent(int evt )
    {
        if (fsm[state, evt] != -1)
            state = fsm[state, evt];
    }
}
