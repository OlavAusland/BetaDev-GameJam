using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{ 
    [SerializeField] private LineRenderer lr;

    private void Start()
    {
        lr.SetPosition(0, transform.position);
    }

    private void Update()
    {
        transform.up = -Utilities.Direction(lr.GetPosition(0), lr.GetPosition(1));
        lr.SetPosition(1, transform.position - new Vector3(0, 0, 0));
    }
}
