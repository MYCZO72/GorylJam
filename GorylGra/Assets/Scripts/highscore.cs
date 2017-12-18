using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscore : MonoBehaviour {

    Text hiscore;
	// Use this for initialization
	void Start () {
        hiscore = GetComponent<Text>();
        hiscore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
