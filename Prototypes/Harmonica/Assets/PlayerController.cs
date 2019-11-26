using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public int walkSpeed;
	GameObject player;
	Rigidbody2D rb;
	GAManager harmonicaManager;

	public bool canPlay;
	
    // Start is called before the first frame update
    void Start()
    {
		harmonicaManager = GameObject.Find("GAManager").GetComponent<GAManager>();
		canPlay = false;
		player = GameObject.Find("Player");
		rb = player.GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
		if (!harmonicaManager.playing)
		{
			Walk();

		}
		

		
    }

	void Walk()
	{
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime, rb.velocity.y);

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Rest")
		{
			canPlay = true;
		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag=="Rest")
		{
			canPlay = false;
		}
	}

}