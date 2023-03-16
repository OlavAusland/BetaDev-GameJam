using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector2 MouseDirection(Transform origin)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)origin.position).normalized;
        return direction;
    }

    public static Vector3 Direction(Vector3 a, Vector3 b) { return (a - b).normalized; }
    
}
