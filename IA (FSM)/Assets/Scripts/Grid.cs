using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    Node[,] nodes;
    [SerializeField]
    float distanceNodes;
    [SerializeField]
    Vector2 gridWorldSize;
    int gridSizeX;
    int gridSizeY;

    void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / distanceNodes);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / distanceNodes);
        nodes = new Node[gridSizeX, gridSizeY];
    }
    void Start () 
    {
        for(int i = 0; i < nodes.GetLength(0); i++)
        {
            for(int j = 0; j < nodes.GetLength(1); j++)
            {

                nodes[i, j].cost = distanceNodes;
            }
        }
	}

	void Update ()
    {

		
	}
}
