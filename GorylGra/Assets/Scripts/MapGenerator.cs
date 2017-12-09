using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour {

	public int BuildingCount;
	public int styl;
	public GameObject budynek;
	public int space = 10;
	public int MinimalHeight;
	public int MaximalHeight;

	private Transform boardHolder = null;

	public List<GameObject> BuildingList = new List<GameObject>();

	void Generate()
	{
		if( boardHolder == null )
			boardHolder = new GameObject("Plansza").transform;
		
		for(int i = 0; i < BuildingCount; i++)
		{
			GameObject nowybudynek = Instantiate( budynek, new Vector3( i * space, 0f, 0f ), Quaternion.identity, boardHolder );

			Building budskrypt = nowybudynek.GetComponent<Building>();

			budskrypt.Create( Random.Range( MinimalHeight, MaximalHeight ), styl );

			budskrypt.KtoryBudynek = i;

			BuildingList.Add( nowybudynek );

			if( i > 0 )
			{
				Building popbud = BuildingList[i - 1].GetComponent<Building>();

				popbud.nextBuilding = budskrypt;
			}
		}
	}

	void Start()
	{
		Generate();
	}
}
