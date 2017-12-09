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
<<<<<<< HEAD
=======
        czySkacze = true;
		//Debug.Log("skakanie destination " + destination.x);
		//Debug.Log("skakanie pozycja " + this.transform.position.x);
>>>>>>> 632ec097b7c768b521f1b6c78d4b498bd6e70061
		while( this.transform.position.x < destination.x )
		{
			float xD = this.transform.position.x + plusfloat;
			float yD = fun( xD );
			yD = - yD * 0.5f;

			this.transform.position = new Vector3( xD, yD, 0f );
			yield return null;
		}
        czySkacze = false;
	}
	private void Jump( Vector3 destination )
	{
		float X = this.transform.position.x;
		float H = curH * 3 - 0.625f;
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
<<<<<<< HEAD
			Vector3 teraz = new Vector3( transform.position.x + space, 0f, 0f ); //new Transform( new Vector3( this.transform.position.x, this.transform.position.y, this.transform.position.z ) );

			Building newBuilding = currentBuilding.nextBuilding;
=======
			//Debug.Log( "przed zmiana: " + transform.position.x );
			Vector3 teraz = new Vector3( transform.position.x + space, 0f, 0f ); //new Transform( new Vector3( this.transform.position.x, this.transform.position.y, this.transform.position.z ) );

			//Debug.Log( "po zmianie: " + transform.position.x );
            score++;  
            gameManager.verbsBank.level = score / 10 + 1;  //co 10 wzieksza sie poziom :)
            Building newBuilding = currentBuilding.nextBuilding;
>>>>>>> 632ec097b7c768b521f1b6c78d4b498bd6e70061
			Destroy( currentBuilding );
			currentBuilding = newBuilding;
			ktorybudynek ++;
			curH = currentBuilding.height;
<<<<<<< HEAD
=======
			//Debug.Log( "teraz h nowe w ifie: " + curH );
>>>>>>> 632ec097b7c768b521f1b6c78d4b498bd6e70061
			Jump( teraz );
		}
	}

	void rozpierdol()
	{
		GameObject ToDestroy = currentBuilding.pietra[ currentBuilding.pietra.Count - 1 ];
		currentBuilding.pietra.RemoveAt( currentBuilding.pietra.Count - 1 );

		this.transform.Translate( 0f, -3f, 0f );
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
			//rozpierdol
			rozpierdol();
		}
		//Debug.Log("ActualChar " + actualchar);

		if(!czySkacze)
        if (Input.GetKeyDown((KeyCode)actualchar))
		{
			textureRenderer.sprite = textureList[Random.Range(0, textureList.Count - 1)];
			gameManager.verbsBank.actualCharNumber++;
			//Debug.Log("Next");
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

		this.transform.Translate( -space, 0f, 0f );

		Building pierwszy = mapGenerator.BuildingList[0].GetComponent<Building>();
		currentBuilding = pierwszy;
		curH = pierwszy.height;
<<<<<<< HEAD
=======
		//Debug.Log( "budynek pierwszy wysokosc" + pierwszy.height );
		//Debug.Log( "pierwsze h na poczatku: " + curH );
>>>>>>> 632ec097b7c768b521f1b6c78d4b498bd6e70061
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
