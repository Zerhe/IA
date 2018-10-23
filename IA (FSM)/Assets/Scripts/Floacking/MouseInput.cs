using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseInput 
{
    public static RaycastHit hit;
    public static float maxDistanceRay = 100;

    public static Vector3 MousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Camera.main.transform.position.y + maxDistanceRay))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
