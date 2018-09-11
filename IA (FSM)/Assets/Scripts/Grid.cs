using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
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

    void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / distanceNodes);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / distanceNodes);
        nodes = new Node[gridSizeX, gridSizeY];
    }
    void Start()
    {

        for(int i = 0; i < nodes.GetLength(0); i++)
        {
            for(int j = 0; j < nodes.GetLength(1); j++)
            {
                nodes[i, j] = new Node();
                nodes[i, j].position = transform.position;
                nodes[i, j].position.x += distanceNodes * (i+1);
                nodes[i, j].position.y = transform.position.y;
                nodes[i, j].position.z += distanceNodes * (j+1);
                nodes[i, j].cost = distanceNodes;                                                          //le asigno el costo de moverse basado en la distancia entre los nodos

                Instantiate(previewNode, nodes[i, j].position, transform.rotation);
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
    }
}
