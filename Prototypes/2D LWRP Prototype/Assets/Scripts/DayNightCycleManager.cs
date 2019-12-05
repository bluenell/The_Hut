using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
	[Range(0, 9)]
	public int colourArrayIndex;

	UnityEngine.Experimental.Rendering.LWRP.Light2D globalLight;
	public Color[] colours;

	

	

    // Start is called before the first frame update
    void Start()
    {

		globalLight = GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();

		
    }

    // Update is called once per frame
    void Update()
    {
		globalLight.color = colours[colourArrayIndex];
	}
}
