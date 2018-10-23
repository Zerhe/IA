using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{

    List<Boid> boids;
    float distanceVisibleBoid;

    void Start()
    {

    }
    void Update()
    {
        foreach (Boid b in boids)
        {
            foreach (Boid b2 in boids)
            {
                if (b == b2)
                    continue;
                if (Direction.CalculateDistance(b2.transform.position, b.transform.position) < distanceVisibleBoid)
                {
                    b.visibleBoids.Add(b2);
                }
                else
                {
                    if (b.visibleBoids.Contains(b2))
                        b.visibleBoids.Remove(b2);
                }
            }
        }
    }

    Vector3 CalculateCohesion()
    {

    }
    Vector3 CalculateSeparation()
    {

    }
    Vector3 CalculateAlineation()
    {
        
    }
    Vector3 CalculateResultant()
    {
        Vector3 resultant = (CalculateCohesion() + CalculateSeparation() + CalculateAlineation()).normalized;
    }
}
