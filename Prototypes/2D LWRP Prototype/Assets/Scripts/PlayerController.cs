using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Settings")]
	public int canoeSpeed, walkSpeed;


	public bool inCanoe, inWater, draggingCanoe;

	GameObject player;
	GameObject canoe;

	Rigidbody2D canoeRb, playerRb;

	DockCanoe dockScript;

	void Start()
	{
		player = GameObject.Find("Player");
		playerRb = player.GetComponent<Rigidbody2D>();

		canoe = GameObject.Find("Canoe").transform.GetChild(0).gameObject;
		canoeRb = GameObject.Find("Canoe").GetComponent<Rigidbody2D>();

		dockScript = GameObject.Find("Canoe").GetComponent<DockCanoe>();
		inCanoe = true;
	}

	void Update()
	{

		if (inCanoe)
		{
			player.transform.position = canoe.transform.position;

			ControlCanoe();

		}
		else
		{
			ControlPlayer();

		}


		if (Input.GetKeyDown(KeyCode.X))
		{
			if (inCanoe)
			{
				inCanoe = false;
			}
			else
			{
				inCanoe = true;
			}
		}

		Debug.Log("In Canoe: " + inCanoe);
		Debug.Log("In Water: " + inWater);

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Water")
		{
			inWater = true;
		}
		else
		{
			inWater = false;
		}
	}

	void ControlCanoe()
	{
		canoeRb.velocity = new Vector2(Input.GetAxis("Horizontal") * canoeSpeed * Time.deltaTime, canoeRb.velocity.y);
	}

	void ControlPlayer()
	{
		playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime, playerRb.velocity.y);

	}
}
