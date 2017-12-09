using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Building : MonoBehaviour
{
	public int height = 5;
	public int KtoryBudynek;

	private Transform buildingHolder = null;
	public GameObject[] floors;
	public GameObject[] parter;

	private List<GameObject> pietra = new List<GameObject>();

	public void Create( int customheight )
	{
		height = customheight;
		if( buildingHolder == null )
			buildingHolder = new GameObject("Building").transform;

		GameObject parterInstance = Instantiate( parter[ Random.Range( 0, parter.Length - 1 ) ], new Vector3( this.transform.position.x, 3f, 0f ), Quaternion.identity, buildingHolder );
		pietra.Add( parterInstance );

		for(int i = 2; i <= height; i++)
		{
			GameObject nowy = floors[ Random.Range( 0, floors.Length - 1 ) ];

			GameObject instance = Instantiate( nowy, new Vector3( this.transform.position.x, i * 3, 0f ), Quaternion.identity, buildingHolder );

			//instance.transform.SetParent( buildingHolder );

			pietra.Add( instance );
		}
	}

	public void DestroyTop()
	{
		GameObject top = pietra[ pietra.Count - 1 ];
		pietra.RemoveAt( pietra.Count - 1 );

		Destroy( top );
		height --;
	}

	public Transform GetTopTransform()
	{
		return pietra[ pietra.Count - 1 ].transform;
	}

	void Start()
	{
		//Create( 6 );
	}
}
