﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecolectResource : MonoBehaviour {

    private IA iA;
    [SerializeField]
    private StoredResources storedResources;
    [SerializeField]
    private Transform recolectTarget;
    [SerializeField]
    private Transform depositTarget;
    [SerializeField]
    private float velMov;
    [SerializeField]
    private int maxAmountResource;
    private int cantResource;
    private string tipeResource;
    private GridNodes grid;
    private bool searchPath = true;
    private List<Vector3> path;

    void Awake ()
    {
        iA = GetComponent<IA>();
        grid = GameObject.Find("GridObject").GetComponent<GridNodes>();
        path = new List<Vector3>();
	}
    void Start()
    {
        iA.sM.SendEvent(0);
    }
    void Update ()
    {
        //print(cantResource);
		switch(iA.sM.GetState())
        {
            case 1:
                MovToRecolect();
                break;
            case 2:
                searchPath = true;
                Recolect();
                break;
            case 3:
                MovToDeposit();
                break;
            case 4:
                searchPath = true;     
                Deposit();
                break;
            default:
                break;
        }
        if(cantResource == maxAmountResource)
            iA.sM.SendEvent(2);
        if(cantResource < 1)
        {
            cantResource = 0;
            iA.sM.SendEvent(0);
        }

    }
    void MovToRecolect()
    {
        //transform.Translate(Direction.CalculateDirection(recolectTarget.position, transform.position) * Time.deltaTime * velMov);
        if(searchPath)
        {
            path = PathFinding.GetPath(grid.GetNodes(), transform.position, recolectTarget.position);
            searchPath = false;
        }
        //print(path.Count);
        PathFinding.MovPath(path, transform, velMov);
    }
    void Recolect()
    {
        cantResource++;
    }
    void MovToDeposit()
    {
        //transform.Translate(Direction.CalculateDirection(depositTarget.position, transform.position) * Time.deltaTime * velMov * 0.5f);
        /*if (searchPath)
        {
            path = PathFinding.GetPath(grid.GetNodes(), transform.position, depositTarget.position);
            searchPath = false;
        }
        //print(path.Count);
        PathFinding.MovPath(path, transform, velMov);*/
    }
    void Deposit()
    {
        cantResource--;
        storedResources.SumGold(1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Resource")     
        {
            iA.sM.SendEvent(1);
            //tipeResource = collision.gameObject.GetComponent<Resource>().tipeResource;
        }
        if (collision.gameObject.tag == "Deposit")
            iA.sM.SendEvent(3);
    }
    /*void MovPath(List<Vector3> pathh)
    {
        if(path.Count > 0)
        {
            if(Mathf.RoundToInt(Direction.CalculateDistance( path[path.Count - 1], transform.position)) == 0)
            {
                path.RemoveAt(path.Count - 1);
            }
            transform.Translate(Direction.CalculateDirection(path[path.Count-1], transform.position) * Time.deltaTime * velMov);
            //print(Direction.CalculateDirection(path[path.Count - 1], transform.position));
            print(path[path.Count - 1]);
        }
        print("asdasd");
    }*/
}
