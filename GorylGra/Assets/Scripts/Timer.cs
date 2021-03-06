﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	public float timespeed = 0.000001f;
	public bool pelny;
	public bool ukryty;

    public Goryl gorryl; 

	public GameObject kamerka;

	public void napelnij()
	{
		ukryty = false;
		this.transform.localScale = new Vector3( 100f, 2f, 1f );
	}

	void Update () {
		if( !(this.transform.localScale.x <= 0f && !ukryty ))
		{
			this.transform.position = new Vector3( kamerka.transform.position.x - 3f, kamerka.transform.position.y - 2.5f, 0f );
			if( !ukryty ) this.transform.localScale = new Vector3( this.transform.localScale.x - timespeed, this.transform.localScale.y, this.transform.localScale.z );
		}
		else
		{
            PlayerPrefs.SetInt("Score", gorryl.score);
            if (gorryl.score > PlayerPrefs.GetInt("HighScore"))
                PlayerPrefs.SetInt("HighScore", gorryl.score);
            SceneManager.LoadScene(2);
		}
			
	}
}
