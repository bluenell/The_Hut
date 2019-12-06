using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	Rigidbody2D playerRb;
	Rigidbody2D canoeRb;


	public GameObject canoeSeat;
	public GameObject player;

	public bool inCanoe;

	[Header("Settings")]
	public int walkSpeed;
	public int canoeSpeed;


	void Start()
	{
		inCanoe = true;
		playerRb = GetComponent<Rigidbody2D>();
		canoeRb = GameObject.Find("Canoe").GetComponent<Rigidbody2D>();
	}


	void FixedUpdate()
	{


		if (inCanoe)
		{
			transform.position = canoeSeat.transform.position;
			Move(canoeSpeed, canoeRb);
		}
		else
		{
			Move(walkSpeed, playerRb);
		}

		

	}

	void Move(int speed, Rigidbody2D rb)
	{
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime, rb.velocity.y);
	}
}

	
