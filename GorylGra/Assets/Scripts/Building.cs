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
	public GameObject[] sufity;

	private List<GameObject> pietra = new List<GameObject>();

	public void Create( int customheight, int style )
	{
		height = customheight;
		if( buildingHolder == null )
			buildingHolder = new GameObject("Building").transform;

		GameObject parterInstance = Instantiate( parter[ style ], new Vector3( this.transform.position.x, 3f, 0f ), Quaternion.identity, buildingHolder );
		pietra.Add( parterInstance );

		for(int i = 2; i < height; i++)
		{
			GameObject nowy = floors[ style ];

			GameObject instance = Instantiate( nowy, new Vector3( this.transform.position.x, i * 3, 0f ), Quaternion.identity, buildingHolder );

			//instance.transform.SetParent( buildingHolder );

			pietra.Add( instance );
		}

		GameObject sufitInstance = Instantiate( sufity[ style ], new Vector3( this.transform.position.x, 3 * height, 0f ), Quaternion.identity, buildingHolder );
		pietra.Add( sufitInstance );
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
