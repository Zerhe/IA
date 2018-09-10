using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public static Vector3 heading;
    public static float distance;
    public static Vector3 direction;

    public static Vector3 CalculateDirection(Vector3 targetPosition, Vector3 startPosition)
    {
        heading = targetPosition - startPosition;
        distance = heading.magnitude;
        direction = heading / distance;
        return direction;
    }
    public static float CalculateDistance(Vector3 targetPosition, Vector3 startPosition)
    {
        heading = targetPosition - startPosition;
        distance = heading.magnitude;
        return distance;
    }
}
