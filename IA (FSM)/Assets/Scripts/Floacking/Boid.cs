using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    List<Boid> visiblesBoids;
    Vector3 directionMov;
    Vector3 destinyPosition;
    [SerializeField]
    int velMov;
    bool mov = false;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            destinyPosition = MouseInput.MousePosition();
            destinyPosition.y = transform.position.y;
            directionMov = Direction.CalculateDirection(destinyPosition, transform.position);
            mov = true;
        }
        if (mov)
            transform.Translate(directionMov * Time.deltaTime * velMov);
        if (Direction.CalculateDistance(destinyPosition, transform.position) < 1)
            mov = false;

    }
}
