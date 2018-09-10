using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    List<Node> Opened;
    List<Node> Closed;
    Node nodeOrigin;
    Node nodeDestination;
    Node nodeActual;

    List<Vector3> GetPath(Node[,] nodes, Vector3 posOrigin, Vector3 posDestination)
    {
        nodeOrigin = SearchNodeByPosition(nodes, posOrigin);
        nodeDestination = SearchNodeByPosition(nodes, posDestination);

        OpenNode(nodeOrigin);                                   //Agrego a la lista de nodos abiertos el nodo d eorigen

        while(Opened.Count > 0)
        {
            nodeActual = SelectionNodeOpenedList();
            if (nodeActual == nodeDestination)
                return CalculePath();
            else
                OpenAdjacent(nodeActual);
                ClosedNode(nodeActual);
        }
        return CalculePath();                                  //retorno una lista de posiciones, las cuaes forman un camino
    }
    Node SearchNodeByPosition(Node[,] nodes, Vector3 pos)
    {
        Node node = new Node();
        float minDistance = -1;
        float distance;

        for(int i = 0; i < nodes.GetLength(0); i++)
        {
            for(int j = 0; j < nodes.GetLength(1); j++)
            {
                if (minDistance != -1)
                {
                    distance = Direction.CalculateDistance(nodes[i, j].position, pos);

                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        node = nodes[i, j];
                    }

                }
                else
                    minDistance = Direction.CalculateDistance(nodes[i, j].position, pos);
            }
        }
        return node;
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
            if(!node.open && !node.close && node.walkeable)
            {
                OpenNode(node.adj[i]);
                node.adj[i].parent = node;
            }
        }
    }
}
