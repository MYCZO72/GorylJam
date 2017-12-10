using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{

    Text scoree;
    // Use this for initialization
    void Start()
    {
        scoree = GetComponent<Text>();
        scoree.text = "Score: " + PlayerPrefs.GetInt("Score");
    }


}
