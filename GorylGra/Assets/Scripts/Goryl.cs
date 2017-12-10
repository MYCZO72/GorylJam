using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goryl : MonoBehaviour {

	private Collider gorylColider;
	private Rigidbody gorylRigid;

	public List<Sprite> textureList = new List<Sprite>();
	private SpriteRenderer textureRenderer;

	public GameObject kamerka;
	public GameManager gameManager;
	public MapGenerator mapGenerator;
	private Building currentBuilding;

	public int space;
	public float speed;
	public int ktorybudynek = 0;
	private int curH;
	private int score = 0;
	private bool czySkacze = false;
	private bool czySpada = false;
	public float plusfloat = 0.1f;
	public bool isDeadProp {  get; private set; }

	private IEnumerator afterDestroyProcedure()
	{
		yield return new WaitForSeconds(2.0f);
	}

	private float a, b, c;

	private float fun( float x )
	{
		return a * x * x + b * x + c;
	}

	IEnumerator JumpProcedure( Vector3 destination )
	{
		czySkacze = true;
		while( this.transform.position.x < destination.x )
		{
			float xD = this.transform.position.x + plusfloat;
			float yD = fun( xD );
			yD += 1.25f;
			yD = - yD * 0.5f;

			this.transform.position = new Vector3( xD, yD, 0f );
			yield return null;
		}
		czySkacze = false;
	}
	private void Jump( Vector3 destination )
	{
		float X = this.transform.position.x;
		float H = curH * 3;
		H = -H;
		a = 1f;
		b = ( H*2 - 2 * X * space - space * space ) / space;
		c = ( -X * X - b * X );
		StartCoroutine( JumpProcedure( destination ) );
	}

	void skonczonybudynek()
	{
		if( curH == 0 )
		{
			Vector3 teraz = new Vector3( transform.position.x + space, 0f, 0f );


			score++;  
			gameManager.verbsBank.level = score / 10 + 1;  //co 10 wzieksza sie poziom :)
			Building newBuilding = currentBuilding.nextBuilding;
			Destroy( currentBuilding );
			currentBuilding = newBuilding;
			ktorybudynek ++;
			curH = currentBuilding.height;
			Jump( teraz );
		}
	}

	IEnumerator JumpStraight(Vector3 destination){
		czySkacze = true;
		czySpada = true;
		while(this.transform.position.y >= destination.y + plusfloat*2){
			this.transform.Translate(0f, -plusfloat*2, 0f);
			yield return null;
		}
		this.transform.Translate(0f, -this.transform.position.y + destination.y, 0f);
		czySkacze = false;
		czySpada = false;
	}


	void rozpierdol()
	{
		Debug.Log( "rozpierdalanie ");
		GameObject ToDestroy = currentBuilding.pietra[ currentBuilding.pietra.Count - 1 ];
		currentBuilding.pietra.RemoveAt( currentBuilding.pietra.Count - 1 );
		Vector3 CEL = new Vector3 (this.transform.position.x, this.transform.position.y - 3f, this.transform.position.z); 
		StartCoroutine(JumpStraight (CEL) );
		//this.transform.Translate( 0f, -3f, 0f );
		curH --;
		Destroy( ToDestroy );
	}

	void CheckInput()
	{
		char actualchar = '-';
		if (gameManager.verbsBank.ActualVerb.Length > gameManager.verbsBank.actualCharNumber)
		{
			actualchar = gameManager.verbsBank.ActualVerb[gameManager.verbsBank.actualCharNumber]; 
		}
		else
		{
			gameManager.verbsBank.actualCharNumber = 0;
			gameManager.verbsBank.newWord();
			StartCoroutine(afterDestroyProcedure());
			rozpierdol();
		}

		if(!czySkacze)
		if (Input.GetKeyDown((KeyCode)actualchar))
		{
			textureRenderer.sprite = textureList[Random.Range(0, textureList.Count - 1)];
			gameManager.verbsBank.actualCharNumber++;
		}
	}

	IEnumerator startowy()
	{
		yield return new WaitForEndOfFrame();
		gorylColider = GetComponent<Collider>();
		gorylRigid = GetComponent<Rigidbody>();
		textureRenderer = GetComponent<SpriteRenderer>();
		textureRenderer.sprite = textureList[0];
		space = mapGenerator.space;

		Debug.Log( "to jest przed poczatkowym przesunieciem " + this.transform.position.x );
		this.transform.Translate( -space, 0f, 0f );
		Debug.Log( "to jest po przesunieciu " + this.transform.position.x );

		Building pierwszy = mapGenerator.BuildingList[0].GetComponent<Building>();
		currentBuilding = pierwszy;
		curH = pierwszy.height;
		Debug.Log( "pierwsza wysokosc " + curH );
		Jump( pierwszy.transform.position );
	}

	void Start () {
		StartCoroutine( startowy() );
	}

	private bool pierwszyraz = true;
	IEnumerator updatowanie()
	{
		if( pierwszyraz ) yield return new WaitForEndOfFrame();
		if( pierwszyraz ) yield return new WaitForEndOfFrame();
		Vector3 rak = this.transform.position;
		rak.z = rak.z - 1;
		kamerka.transform.position = rak;

		CheckInput();
		if(!czySpada) 
			skonczonybudynek();

		pierwszyraz = false;
	}

	void Update () {
		StartCoroutine( updatowanie() );

		/*float movex = Input.GetAxis("Horizontal");
		float movey = Input.GetAxis("Vertical");

		Vector3 ruch = new Vector3( movex, movey, 0f );
		ruch *= speed * Time.deltaTime;
		Vector3 newposition = this.transform.position;
		newposition = ruch + newposition;
		this.transform.position = newposition;*/
	}
}
