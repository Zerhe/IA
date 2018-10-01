using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoredResources : MonoBehaviour {

    [SerializeField]
    private int amountGold;
    [SerializeField]
    private Text goldText;

    void Start ()
    {
		
	}
	void Update ()
    {
        goldText.text = "" + amountGold;
	}
    public void SumGold(int amount)
    {
        amountGold += amount;
    }
}
