using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNodes : MonoBehaviour
{
    Node[,] nodes;
    [SerializeField]
    float distanceNodes;
    [SerializeField]
    Vector2 gridWorldSize;
    int gridSizeX;
    int gridSizeY;
    [SerializeField]
    GameObject previewNode;
    [SerializeField]
    int layerObstacle;
    [SerializeField]
    float heightObstacleDetection;
    [SerializeField]
    bool visualizeNodes;
    List<GameObject> viewNodes; 

    void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / distanceNodes);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / distanceNodes);
        nodes = new Node[gridSizeX, gridSizeY];
        viewNodes = new List<GameObject>();
    }
    void Start()
    {

        for(int i = 0; i < nodes.GetLength(0); i++)                                                        //Inicialzo los nodos, les agrego una posicion y un costo
        {
            for(int j = 0; j < nodes.GetLength(1); j++)
            {
                nodes[i, j] = new Node();
                nodes[i, j].position = transform.position;
                nodes[i, j].position.x += distanceNodes * (i+1);
                nodes[i, j].position.y = transform.position.y;
                nodes[i, j].position.z += distanceNodes * (j+1);
                nodes[i, j].cost = distanceNodes;                                                          //le asigno el costo de moverse basado en la distancia entre los nodos

                viewNodes.Add(Instantiate(previewNode, nodes[i, j].position, transform.rotation));         //Instancio objetos en las posiciones de los nodos para tener una referencia
            }
        }
        for (int i = 0; i < nodes.GetLength(0); i++)                                                       //Checkeo los adjacentes de cada nodo                                 
        {
            for (int j = 0; j < nodes.GetLength(1); j++)
            {
                CheckAdjacents(nodes[i, j], i, j);
            }
        }
    }

    void Update()
    {
        for(int i = 0; i < nodes.GetLength(0); i++)
        {
            for(int j = 0; j < nodes.GetLength(1); j++)
            {
                nodes[i, j].CheckObstaculeInThisNode(heightObstacleDetection, layerObstacle);
            }
        }
        if(visualizeNodes)                                                                                //Si quiero visualizarlos activo los objetos de referencia
        {
            foreach(GameObject viewNode in viewNodes)
            {
                viewNode.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject viewNode in viewNodes)
            {
                viewNode.SetActive(false);
            }
        }
    }
    public void CheckAdjacents(Node node, int gridPosX, int gridPosY)
    {
        for (int i = 0; i < nodes.GetLength(0); i++)
        {
            for (int j = 0; j < nodes.GetLength(1); j++)
            {
                if(i >= gridPosX - 1 && i <= gridPosX + 1 &&
                   j >= gridPosY - 1 && j <= gridPosY + 1 && 
                   !(i == gridPosX && j == gridPosY))
                {
                    node.adj.Add(nodes[i, j]);
                    //print("node" + "(" + gridPosX + "," + gridPosY + ") :" + "(" + i + "," + j + ")");
                }
            }
        }
    }
    public Node[,] GetNodes()
    {
        return nodes;
    }
}
