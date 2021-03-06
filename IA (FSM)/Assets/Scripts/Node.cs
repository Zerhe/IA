﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> adj;
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
        adj = new List<Node>();

    }
    public void CheckObstaculeInThisNode(float heightRay, int layerObstacle)
    {
        Vector3 originRay = position;
        originRay.y += heightRay;
        float maxDistanceRay = originRay.y - position.y;

        RaycastHit hit;
        if(Physics.Raycast(originRay, Vector3.down, out hit, maxDistanceRay))
        {
            if(hit.collider.gameObject.layer == layerObstacle)
            {
                walkeable = false;
            }
            else
                walkeable = true;
        }
        //Debug.Log(walkeable);
        //Debug.DrawRay(originRay, Vector3.down * maxDistanceRay, Color.red);
    }
    public void SetOpen(bool val)
    {
        open = val;
    }
    public void SetClose(bool val)
    {
        close = val;
    }
}
