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

    public bool isDeadProp {  get; private set; }

	void Start () {
        gorylColider = GetComponent<Collider>();
        gorylRigid = GetComponent<Rigidbody>();
        textureRenderer = GetComponent<SpriteRenderer>();
        textureRenderer.sprite = textureList[0];
	}

    private IEnumerator afterDestroyProcedure()
    {
        yield return new WaitForSeconds(2.0f);
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
        }
        Debug.Log("ActualChar " + actualchar);

        if (Input.GetKey((KeyCode)actualchar))
        {
            textureRenderer.sprite = textureList[Random.Range(0, textureList.Count - 1)];
            gameManager.verbsBank.actualCharNumber++;
            Debug.Log("Next");
        }
    }

	public int speed;

    void Update () {
		Vector3 rak = this.transform.position;
		rak.z = rak.z - 1;
		kamerka.transform.position = rak;

        CheckInput();

		float movex = Input.GetAxis("Horizontal");
		float movey = Input.GetAxis("Vertical");

		Vector3 ruch = new Vector3( movex, movey, 0f );
		ruch *= speed * Time.deltaTime;
		Vector3 newposition = this.transform.position;
		newposition = ruch + newposition;
		this.transform.position = newposition;
	}
}
