using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateChmura : MonoBehaviour {

	public int LiczbaChmur;
	public int koniec = 300;
	public float speed = 0.5f;
	public int maxH;
	public GameObject VAPE;

	private Transform chmuryHolder = null;

	void Start () {
		if( chmuryHolder == null )
			chmuryHolder = new GameObject("ChmuroTrzymacz").transform;
		for(int i = 0; i < LiczbaChmur; i++)
		{
			Instantiate( VAPE, new Vector3( Random.Range( -300, koniec ), Random.Range( 3, maxH ), 0f ), Quaternion.identity, chmuryHolder );
		}
	}
}
