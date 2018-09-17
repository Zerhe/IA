using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFinding 
{
    public static List<Node> Opened = new List<Node>();
    public static List<Node> Closed = new List<Node>();
    public static Node nodeOrigin;
    public static Node nodeDestination;
    public static Node nodeActual;

    public static List<Vector3> GetPath(Node[,] nodes, Vector3 posOrigin, Vector3 posDestination)
    {
        nodeOrigin = SearchNodeByPosition(nodes, posOrigin);
        nodeDestination = SearchNodeByPosition(nodes, posDestination);

        OpenNode(nodeOrigin);                                   //Agrego a la lista de nodos abiertos el nodo d eorigen

        /*while(Opened.Count > 0)
        {
            nodeActual = SelectionNodeOpenedList();
            if (nodeActual == nodeDestination)
                return CalculePath(posDestination);
            else
                OpenAdjacent(nodeActual);
                ClosedNode(nodeActual);
        }*/
        nodeActual = SelectionNodeOpenedList();
        return CalculePath(posDestination);                                  //retorno una lista de posiciones, las cuaes forman un camino
    }
    public static Node SearchNodeByPosition(Node[,] nodes, Vector3 pos)
    {
        Node node = new Node();
        float minDistance = -1;                                //le asigno menos uno por default
        float distance;

        for(int i = 0; i < nodes.GetLength(0); i++)
        {
            for(int j = 0; j < nodes.GetLength(1); j++)
            {
                if (minDistance != -1)                                                          //Si iene con quien compararse, se calcula la menor distancia   
                {
                    distance = Direction.CalculateDistance(nodes[i, j].position, pos);

                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        node = nodes[i, j];
                    }

                }
                else                                                                             //Si es -1 quiere decir que no tien con quien comparase asi que se asigna si o si
                    minDistance = Direction.CalculateDistance(nodes[i, j].position, pos);
            }
        }
        return node;
    }
    public static void OpenNode(Node node)
    {
        Opened.Add(node);
    }
    public static void ClosedNode(Node node)
    {
        Opened.Remove(node);
        Closed.Add(node);
    }
    public static Node SelectionNodeOpenedList()                   //De que parte de la lista de nodos abiertos saca el nodo
    {
        Node node = Opened[0];            
        return node;
    }
    public static List<Vector3> CalculePath(Vector3 posDestination)
    {
        List<Vector3> path = new List<Vector3>();
        Node node = nodeActual;

        path.Add(posDestination);
        while(node.parent != node)
        {
            path.Add(node.position);
            node = node.parent;
        }
        path.Add(node.position);
        return path;
    }
    public static void OpenAdjacent(Node node)
    {
        foreach(Node adj in node.adj)
        {
            if(!adj.open && !adj.close && adj.walkeable)
            {
                OpenNode(adj);
                adj.parent = node;
            }
        }
    }
}
