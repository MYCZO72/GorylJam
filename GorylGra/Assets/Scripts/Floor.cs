using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	Rigidbody2D rigidbody;
	private Transform pozycja;


	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
		pozycja = GetComponent<Transform>();
	}
}
