using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
	UnityEngine.Experimental.Rendering.LWRP.Light2D globalLight;

    // Start is called before the first frame update
    void Start()
    {
		globalLight = GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();

		globalLight.color = Color.blue;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
