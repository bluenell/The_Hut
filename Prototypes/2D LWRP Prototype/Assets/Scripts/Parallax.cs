﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

	float length, startPos;
	public GameObject cam;
	public float parallaxStrength;


    void Start()
    {
		startPos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
        
    }

	void FixedUpdate()
	{
		float temp = cam.transform.position.x * (1 - parallaxStrength);
		float distance = cam.transform.position.x * parallaxStrength;
		transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

		if (temp > startPos + length)
		{
			startPos += length;
		}
		else if (temp < startPos - length)
		{
			startPos -= length;
		}
	}
}
