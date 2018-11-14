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
    float inicialYPosition;
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
        inicialYPosition = transform.position.y;
    }
	
	void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            destinyPosition = MouseInput.MousePosition();
            destinyPosition.y = transform.position.y;
            lp.SetTargetPosition(destinyPosition);
            //print(gameObject.name + transform.forward);
            directionMov = Direction.CalculateDirection(destinyPosition, transform.position);
            mov = true;
        }
        if (mov)
        {
            directionMov = ((directionMov + fm.CalculateResultant(this)) * Time.deltaTime * velMov);
            directionMov.y = 0;
            transform.position += directionMov;
            if (Direction.CalculateDistance(destinyPosition, transform.position) < 2)
            {
                print("paroo");
                mov = false;
                foreach (Boid b in visibleBoids)
                {
                    b.SetMov(false);
                }
            }
        }
    }
    public void SetMov(bool mov)
    {
        this.mov = mov;
    }
}
