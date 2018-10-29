using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    [HideInInspector]
    public List<Boid> visibleBoids;
    [SerializeField]
    private FlockingManager fm;
    private LookAtPosition lp;
    Vector3 directionMov;
    Vector3 destinyPosition;
    [SerializeField]
    int velMov;
    bool mov = false;

    private void Awake()
    {
        lp = GetComponent<LookAtPosition>();
    }
    void Start ()
    {
        fm.boids.Add(this);
        directionMov = Vector3.zero;
    }
	
	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            destinyPosition = MouseInput.MousePosition();
            destinyPosition.y = transform.position.y;
            lp.SetTargetPosition(destinyPosition);

            directionMov = Direction.CalculateDirection(destinyPosition, transform.position);
            mov = true;
        }
        if (mov)
            transform.Translate((directionMov + fm.CalculateResultant(this)) * Time.deltaTime * velMov);
        if (Direction.CalculateDistance(destinyPosition, transform.position) < 1)
            mov = false;
    }
}
