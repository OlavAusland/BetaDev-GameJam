using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public int lifetime;

    private void Start() { Destroy(this.gameObject, lifetime); }
}
