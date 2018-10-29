using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPosition : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 targetPosition;

    private void Start()
    {
        if(target)
        targetPosition = target.position;

    }
    void Update()
    {
        Vector3 relativePos = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;

        //transform.LookAt(targetPosition);
    }
    public void SetTargetPosition(Vector3 newTragetPosition)
    {
        targetPosition = newTragetPosition;
    }
}
