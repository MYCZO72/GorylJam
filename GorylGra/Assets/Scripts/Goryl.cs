using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goryl : MonoBehaviour {

	private Animator animator;

	public List<Sprite> textureList = new List<Sprite>();
	private SpriteRenderer textureRenderer;

	public GameObject kamerka;
	public GameManager gameManager;
	public MapGenerator mapGenerator;
	public Timer timer;
	private Building currentBuilding;

	public int space;
	private int ktoreuderzenie;
	public int ktorybudynek = 0;
	private int curH;
	public int score = 0;

    public float speed;
    private float a, b, c;
    public float plusfloat = 0.1f;
    public float kara = 1f;

    private bool czySkacze = false;
	private bool czySpada = false;
	public bool czyKoniecSlowa = false;
	public bool isDeadProp {  get; private set; }
    private bool trzebarozpierdolic;
    private bool pierwszyraz = true;

    public AudioClip wybuch;
	public AudioClip klik;
	public AudioClip skok;
	public AudioClip zle;

	private IEnumerator afterDestroyProcedure()
	{
		yield return new WaitForSeconds(2.0f);
	}

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
			if( xD > destination.x )
				xD = destination.x;
			float yD = fun( xD );
			yD += 1.25f;
			yD = - yD * 0.5f;

			if( yD < 0 )
				yD = 0;

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
		timer.napelnij();
	}

	void skonczonybudynek()
	{
		if( curH == 0 )
		{
			Vector3 teraz = new Vector3( transform.position.x + space, 0f, 0f );

			
			Building newBuilding = currentBuilding.nextBuilding;

			gameManager.verbsBank.level = newBuilding.styl + 1;  //co 10 wzieksza sie poziom :)

			Destroy( currentBuilding );
			currentBuilding = newBuilding;
			ktorybudynek ++;
			curH = currentBuilding.height;

			GetComponent<AudioSource>().clip=skok;
			GetComponent<AudioSource>().Play();
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
		czyKoniecSlowa = true;
        score++;
        trzebarozpierdolic = false;
		GetComponent<AudioSource>().clip=wybuch;
		GetComponent<AudioSource>().Play();
		GameObject ToDestroy = currentBuilding.pietra[ currentBuilding.pietra.Count - 1 ];
		currentBuilding.pietra.RemoveAt( currentBuilding.pietra.Count - 1 );
		Vector3 CEL = new Vector3 (this.transform.position.x, this.transform.position.y - 3f, this.transform.position.z);
        gameManager.canvas.GetComponent<ImageManager>().LoadNewWord(gameManager.verbsBank.ActualVerb);
        StartCoroutine(JumpStraight (CEL) );
		curH --;
		Destroy( ToDestroy );
	}

	void Ustawfalse()
	{
		animator.SetBool( "klikniete", false );
	}

	void probojrozpierdolic()
	{
		if( trzebarozpierdolic )
			rozpierdol();
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
			czyKoniecSlowa = false;
			gameManager.verbsBank.actualCharNumber = 0;
			gameManager.verbsBank.newWord();
			StartCoroutine(afterDestroyProcedure());
			
			trzebarozpierdolic = true;
			//koniec slowa
			timer.napelnij();
		}

		if(!czySkacze)
		if (Input.GetKeyDown((KeyCode)actualchar))
		{
			gameManager.canvas.GetComponent<ImageManager>().SetImageONCorrect();
			textureRenderer.sprite = textureList[Random.Range(0, textureList.Count - 1)];
			gameManager.verbsBank.actualCharNumber++;

			GetComponent<AudioSource>().clip=klik;
			GetComponent<AudioSource>().Play();
			animator.SetBool( "klikniete", true );
			ktoreuderzenie ++;
			//dobrze wpisane slowo
		}
		else
		{
			if(Input.anyKeyDown)
			{
				gameManager.canvas.GetComponent<ImageManager>().SetImageONIrcorrect();
				timer.transform.localScale = new Vector3(timer.transform.localScale.x-kara,timer.transform.localScale.y,timer.transform.localScale.z);
				GetComponent<AudioSource>().clip=zle;
				GetComponent<AudioSource>().Play();
			}
				// zle wpisane
		}
	}

	IEnumerator startowy()
	{
		yield return new WaitForEndOfFrame();
		animator = GetComponent<Animator>();
		textureRenderer = GetComponent<SpriteRenderer>();
		textureRenderer.sprite = textureList[0];
		space = mapGenerator.space;
		gameManager.canvas.GetComponent<ImageManager>().LoadNewWord(gameManager.verbsBank.ActualVerb);

		this.transform.Translate( -space, 0f, 0f );

		Building pierwszy = mapGenerator.BuildingList[0].GetComponent<Building>();
		currentBuilding = pierwszy;
		curH = pierwszy.height;
		Jump( pierwszy.transform.position );
	}

	void Start () {
		StartCoroutine( startowy() );
	}

	IEnumerator updatowanie()
	{
		if( pierwszyraz ) yield return new WaitForEndOfFrame();
		if( pierwszyraz ) yield return new WaitForEndOfFrame();
		Vector3 rak = this.transform.position;
		rak.z = rak.z - 1;
		rak.y -= 1.1f;
		kamerka.transform.position = rak;

		CheckInput();
		if(!czySpada) 
			skonczonybudynek();

		if( czySkacze )
			Ustawfalse();

		if( !czySkacze )
		{
			if( ktoreuderzenie % 2 == 0 )
			{
				animator.SetBool( "czylewa", true );
				animator.SetBool( "czyprawa", false );
			}
			else
			{
				animator.SetBool( "czyprawa", true );
				animator.SetBool( "czylewa", false );
			}
		}

		pierwszyraz = false;
	}

	void Update () {
		StartCoroutine( updatowanie() );
	}
}
