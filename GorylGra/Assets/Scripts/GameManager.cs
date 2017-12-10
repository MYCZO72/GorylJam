using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public VerbsBank verbsBank;
    public Goryl gorylPlayer;
    public Canvas canvas;
    public Text actualButtonWrite;
	void LateUpdate () {
        if (gorylPlayer.isDeadProp)
        {
            //zmien canvas dla smierci
        }
        if (verbsBank.ActualVerb.Length > verbsBank.actualCharNumber) 
            actualButtonWrite.text = verbsBank.ActualVerb[verbsBank.actualCharNumber].ToString();
	}
}
