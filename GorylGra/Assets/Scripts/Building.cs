using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Building : MonoBehaviour
{
	public int height = 5;

	private Transform buildingHolder = null;
	public GameObject[] floors;

	private List<GameObject> pietra = new List<GameObject>();

	void Create()
	{
		if( buildingHolder == null )
			buildingHolder = new GameObject("Building").transform;

		for(int i = 1; i <= height; i++)
		{
			GameObject nowy = floors[ Random.Range( 0, floors.Length - 1 ) ];

			GameObject instance = Instantiate( nowy, new Vector3( 0f, i * 3, 0f ), Quaternion.identity, buildingHolder );

			//instance.transform.SetParent( buildingHolder );

			pietra.Add( instance );
		}
	}

	void DestroyTop()
	{
		GameObject top = pietra[ pietra.Count - 1 ];
		pietra.RemoveAt( pietra.Count - 1 );

		Destroy( top );
		height --;
	}

	void Start()
	{
		Create();
	}
}
