using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockCanoe : MonoBehaviour
{
	public bool canDock, isDocked;

	PlayerController playerController;

	GameObject canoe, dockPoint, player, playerPos;
	Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Player");
		playerController = player.GetComponent<PlayerController>();
		canoe = GameObject.Find("Canoe");
		rb = GetComponent<Rigidbody2D>();


	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Z))
		{

			if (canDock)
			{
				Dock(dockPoint.transform);
			}
			else
			{

				Debug.Log("Canoe cannot dock here");
			}

		}


		if (Input.GetKey(KeyCode.C))
		{
			if (isDocked)
			{
				DragCanoe();
			}
			else
			{
				Debug.Log("Cannot Drag Canoe");
			}
		}
	}


	public void OnTriggerEnter2D(Collider2D collision)
	{
		dockPoint = collision.gameObject.transform.GetChild(0).gameObject;
		playerPos = collision.gameObject.transform.GetChild(1).gameObject;
		if (collision.gameObject.tag == "Dock")
		{
			canDock = true;
			Debug.Log("Can Dock Here");
		}
		else
		{
			canDock = false;
			Debug.Log("Cannot Dock here");
		}
	}

	void Dock(Transform position)
	{
		isDocked = true;

		if (isDocked && !playerController.draggingCanoe)
		{
			Debug.Log("Canoe is being docked");
			playerController.inCanoe = false;

			canoe.transform.position = dockPoint.transform.position;
			player.transform.position = playerPos.transform.position;

			rb.simulated = false;

		}

		

	}

	void DragCanoe()
	{
			
		canoe.transform.position = player.transform.GetChild(3).transform.position;

		rb.simulated = true;

		Debug.Log("Dragging canoe");

	}

}


