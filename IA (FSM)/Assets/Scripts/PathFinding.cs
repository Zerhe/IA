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

        while(Opened.Count > 0)
        {
            //Debug.Log("Open : " + Opened.Count);
            nodeActual = SelectionNodeOpenedList();
            Debug.Log(nodeActual.totalCost);
            if (nodeActual == nodeDestination)
            {
                Debug.Log("EncontreElDestino");
                CleanLists();               
                return CalculePath(posDestination);
            }
            else
            {
                OpenAdjacent(nodeActual);
                ClosedNode(nodeActual);
            }
            Debug.Log("Open : " + Opened.Count);
        }
        //nodeActual = SelectionNodeOpenedList();                              //esta linea no va
        //ClosedNode(nodeActual);                                              //esta tampoco xd
        Debug.Log("NoEncontreElDestino");
        CleanLists();
        return CalculePath(posDestination); //sacar que valla al destino      //sino encuentro el nodo final devuelvo un camino hasta el ultimo nodo que busque
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
        node.SetOpen(true);
    }
    public static void ClosedNode(Node node)
    {
        Opened.Remove(node);
        node.SetOpen(false);
        Closed.Add(node);
        node.SetClose(true);
    }
    public static Node SelectionNodeOpenedList()                   //De que parte de la lista de nodos abiertos saca el nodo
    {
        Node nodeMinCost = new Node();
        bool firstSearch = true;

        foreach(Node node in Opened)
        {
            if (firstSearch)
            {
                nodeMinCost = node;
                firstSearch = false;
            }
            else if (node.totalCost <= nodeMinCost.totalCost)
                nodeMinCost = node;
        }
                return nodeMinCost;
        //Node node = Opened[0];            
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
        //Debug.Log("ultimoNodo");
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
                adj.totalCost += node.totalCost;
                adj.parent = node;
            }
        }
    }
    public static void CleanLists()
    {
        while (Opened.Count > 0)
        {
            Opened[0].SetOpen(false);
            Opened.RemoveAt(0);
        }
        while (Closed.Count > 0)
        {
            Closed[0].SetClose(false);
            Closed.RemoveAt(0);
        }
    }
    public static void MovPath(List<Vector3> path, Transform transform, float velMov)
    {
        if(path.Count > 0)
        {
            if(Mathf.RoundToInt(Direction.CalculateDistance( path[path.Count - 1], transform.position)) == 0)
            {
                path.RemoveAt(path.Count - 1);
            }
            transform.Translate(Direction.CalculateDirection(path[path.Count-1], transform.position) * Time.deltaTime * velMov);
            //print(Direction.CalculateDirection(path[path.Count - 1], transform.position));
            //Debug.Log(path[path.Count - 1]);
        }
        //Debug.Log("asdasd");
    }
}
