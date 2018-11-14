using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    [HideInInspector]
    public List<Boid> boids;
    [SerializeField]
    float distanceVisibleBoid;
    [SerializeField]
    float wCohesion, wSeparation, wAlineation;

    private void Awake()
    {
        boids = new List<Boid>();
    }
    void Start()
    {

    }
    void Update()
    {
        foreach (Boid b in boids)
        {
            foreach (Boid b2 in boids)
            {
                //print("asd");
                if (b == b2)
                    continue;
                if (Direction.CalculateDistance(b2.transform.position, b.transform.position) < distanceVisibleBoid)
                {
                    b.visibleBoids.Add(b2);
                    //print("agregado");
                }
                else
                {
                    if (b.visibleBoids.Contains(b2))
                        b.visibleBoids.Remove(b2);
                }
            }
        }
    }

    public Vector3 CalculateCohesion(Boid b)
    {
        Vector3 cohesion = Vector3.zero;
        foreach (Boid boid in b.visibleBoids)
        {
            cohesion += boid.transform.position;
            if (Direction.CalculateDistance(boid.transform.position, b.transform.position) > 10)
            {
                wCohesion = 1.5f;
                wSeparation = 1;
            }
            else if (Direction.CalculateDistance(boid.transform.position, b.transform.position) < 5)
            {
                wSeparation = 1.5f;
                wCohesion = 1;
            }
        }
        cohesion = cohesion / b.visibleBoids.Count;
        cohesion = Direction.CalculateDirection(cohesion, b.transform.position);
        return cohesion;
    }
    public Vector3 CalculateAlineation(Boid b)
    {
        Vector3 alineation = Vector3.zero;

        foreach (Boid boid in b.visibleBoids)
        {
            alineation += boid.transform.forward;
        }
        alineation = alineation / b.visibleBoids.Count;
        //alineation = Direction.CalculateDirection(alineation, b.transform.position);
        return alineation;
    }
    public Vector3 CalculateResultant(Boid b)
    {
        Vector3 resultant = (CalculateCohesion(b) * wCohesion + -CalculateCohesion(b) * wSeparation + CalculateAlineation(b) * wAlineation).normalized;
        return resultant;
    }
}
