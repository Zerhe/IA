using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    List<Node> adj;
    Node parent;
    float cost;
    float totalCost;
    bool open = false;
}
