using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    [HideInInspector]
    public List<Boid> boids;
    [SerializeField]
    float distanceVisibleBoid;
    Vector3 cohesion, separation, alineation;
    [SerializeField]
    float wCohesion, wSeparation, wAlineation;

    private void Awake()
    {
        boids = new List<Boid>();
    }
    void Start()
    {
        cohesion = separation = alineation = Vector3.zero;
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
                    print("agregado");
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
        foreach (Boid boid in b.visibleBoids)
        {
            cohesion += boid.transform.position;
        }
        cohesion = cohesion / b.visibleBoids.Count;
        cohesion = Direction.CalculateDirection(cohesion, transform.position);
        return cohesion;
    }
    public Vector3 CalculateSeparation(Boid b)
    {
        return -cohesion;
    }
    public Vector3 CalculateAlineation(Boid b)
    {
        foreach (Boid boid in b.visibleBoids)
        {
            alineation += boid.transform.forward;
        }
        alineation = Direction.CalculateDirection(alineation, transform.position);
        return alineation;
    }
    public Vector3 CalculateResultant(Boid b)
    {
        Vector3 resultant = (CalculateCohesion(b) * wCohesion + CalculateSeparation(b) * wSeparation + CalculateAlineation(b) * wAlineation);
        return resultant;
    }
}
