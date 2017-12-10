using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChmura : GenerateChmura {
	private float direction = 1f;

	void Start()
	{
		int ran = Random.Range( 0, 1 );
		if( ran == 1 )
			direction = -1f;
	}

	void Update () 
	{
		this.transform.Translate( direction * speed, 0f, 0f );
	}
}
