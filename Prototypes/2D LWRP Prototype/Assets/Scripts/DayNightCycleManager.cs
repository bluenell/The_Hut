using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
	[Range(0, 9)]
	public int colourArrayIndex;

	UnityEngine.Experimental.Rendering.LWRP.Light2D globalLight;
	public Color[] colours;

	//public float transitionSpeed = 50f;

	float lerpMax = 0.5f;
		float lerpValue;
	bool isChanging;

	

    // Start is called before the first frame update
    void Start()
    {

		globalLight = GetComponent<UnityEngine.Experimental.Rendering.LWRP.Light2D>();
		globalLight.color = colours[colourArrayIndex];
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {

		if (Input.GetKeyDown("space") && !isChanging)
		{
			isChanging = true;
		}
		if (isChanging)
		{
			ChangeColour();
		}


		//globalLight.color = Color.Lerp(colours[colourArrayIndex], colours[colourArrayIndex + 1], transitionSpeed * Time.deltaTime);
		//colourArrayIndex++;

		/*
		for (int i = 0; i < colours.Length; i++)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				globalLight.color = Color.Lerp(colours[i], colours[i + 1], transitionSpeed * Time.deltaTime);

			}

			
		}
		*/
	    	
		//globalLight.color = colours[colourArrayIndex];
	}

	
	
	void ChangeColour()
	{
		Debug.Log("Changing");
		//globalLight.color = Color.Lerp(colours[colourArrayIndex], colours[colourArrayIndex + 1], transitionSpeed * Time.deltaTime);

		if (lerpValue >= 1f)
		{
			colourArrayIndex++;
			globalLight.color = colours[colourArrayIndex % colours.Length];
			isChanging = false;
			lerpValue = 0f;
		}
		else
		{
			lerpValue += lerpMax * Time.deltaTime;
			int firstColor = colourArrayIndex % colours.Length;
			int secondColor = (colourArrayIndex + 1) % colours.Length;
			globalLight.color = Color.Lerp(colours[firstColor], colours[secondColor], lerpValue);
		}

	}
	
}
