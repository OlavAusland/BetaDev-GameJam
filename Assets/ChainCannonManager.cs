using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChainCannonManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public ChainCannon chainCannon;
    public Transform left;
    public Transform right;
    [SerializeField] private LineRenderer lr;

    public float rotationSpeed = 3f;

    private void Update()
    {
        HandleLineRenderer();
    }
    void FixedUpdate()
    {
        Rotate();
    }

    private void HandleLineRenderer()
    {
        lr.SetPosition(0, left.position);
        lr.SetPosition(lr.positionCount-1, right.position);
    }

    private void Rotate()
    {
        transform.eulerAngles += new Vector3(0, 0, 1) * rotationSpeed;
    }
}
