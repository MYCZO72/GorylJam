using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	public float timespeed = 0.000001f;
	public bool pelny;
	public bool ukryty;

	public GameObject kamerka;

	//SpriteRenderer xd = GetComponent<SpriteRenderer>();
	public void napelnij()
	{
		Debug.Log( "pojawiam" );
		ukryty = false;
		this.transform.localScale = new Vector3( 100f, 2f, 1f );
		//GetComponent<SpriteRenderer>().enabled = true;
		//xd.enabled = !xd.enabled;
	}

	public void ukryj()
	{
		Debug.Log( "ukrywam" );
		ukryty = true;
		this.transform.localScale = new Vector3( 0f, 0f, 0f );
		//GetComponent<SpriteRenderer>().enabled = false;
		//xd.enabled = !xd.enabled;
	}

	void Update () {
		if( !(this.transform.localScale.x <= 0f && !ukryty ))
		{
		this.transform.position = new Vector3( kamerka.transform.position.x - 3f, kamerka.transform.position.y - 2.5f, 0f );

		if( !ukryty ) this.transform.localScale = new Vector3( this.transform.localScale.x - timespeed, this.transform.localScale.y, this.transform.localScale.z );
		}
		else
		{
			Debug.Log( "przegrales" );
		}
			
	}
}
