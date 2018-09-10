using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node[] adj;
    public Node parent;
    public Vector3 position;
    public float cost;
    public float totalCost;
    public bool open = false;
    public bool close = false;
    public bool walkeable = true;

    public Node()
    {
        parent = this;
        if(Physics.CheckSphere(position, 0.1f, 9))
        {
            walkeable = false;
        }
    }
}
