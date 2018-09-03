using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    List<Node> Opened;
    List<Node> Closed;
    Node nodeActual;

    List<Vector3> GetPath(Node[,] nodes)
    {
        OpenNode(nodes[0, 0]);                       //Nodo de origen

        while(Opened.Count > 0)
        {
            nodeActual = SelectionNodeOpenedList();
            //if (nodeActual = destino)
                return CalculePath();
            //else
                OpenAdjacent(nodeActual);
                ClosedNode(nodeActual);
        }
        return CalculePath();                                  //No puede ir a ese nodo
    }
    void OpenNode(Node node)
    {
        Opened.Add(node);
    }
    void ClosedNode(Node node)
    {
        Opened.Remove(node);
        Closed.Add(node);
    }
    Node SelectionNodeOpenedList()                   //De que parte de la lista de nodos abiertos saca el nodo
    {
        Node node = Opened[0];            
        return node;
    }
    List<Vector3> CalculePath()
    {
        List<Vector3> path = new List<Vector3>();
        Node node = nodeActual;

        while(node.parent != node)
        {
            path.Add(node.position);
            node = node.parent;
        }
        return path;
    }
    void OpenAdjacent(Node node)
    {
        for(int i = 0; i < node.adj.Length; i++)
        {
            if(!node.open && !node.close)
            {
                OpenNode(node.adj[i]);
                node.adj[i].parent = node;
            }
        }
    }
}
