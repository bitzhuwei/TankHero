﻿using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

    public Transform center;
    public float rotationSpeed = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(center.position, Vector3.up, rotationSpeed * Time.deltaTime); 
	
	}
}
