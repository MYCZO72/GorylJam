using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {

    public List<Sprite> SpriteList = new List<Sprite>();
    public List <GameObject> wordMap = new List<GameObject>();
    public List<int> wordValuesMap = new List<int>();
    public List<bool> wordClickState = new List<bool>();
    public GameObject sprite;
    public string word;
    public GameObject WordsListPanel;
    int frameIterator = 0;
    int actualFrame = 0;

    IEnumerator animationProcess(int indexOfSign, GameObject spriteRenderer)
    {
        yield return new WaitForEndOfFrame();
        while (spriteRenderer!=null)
        {
            Debug.Log(wordClickState[wordValuesMap.IndexOf(indexOfSign)]);
            if (actualFrame == 1)
            {
                spriteRenderer.GetComponent<Image>().sprite = SpriteList[indexOfSign * 4 + actualFrame];
                frameIterator++;
            }
            else if(actualFrame == 0)
            {
                spriteRenderer.GetComponent<Image>().sprite = SpriteList[indexOfSign * 4 + actualFrame];
                frameIterator++;
            }

            if(word.Length != 0) if ( frameIterator % word.Length == 0)
            {
                if (actualFrame == 1)
                {
                    actualFrame = 0;
                    frameIterator = 0;
                }
                else
                {
                    actualFrame = 1;
                    frameIterator = 0;
                }
            }
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator greenButtonOnProces()
    {
        wordMap[0].GetComponent<Image>().sprite = SpriteList[wordValuesMap[0] * 4 + 3];
        yield return new WaitForSeconds(0.2f);
        Destroy(wordMap[0]);
        wordMap.RemoveAt(0);
        wordValuesMap.RemoveAt(0);
        wordClickState.RemoveAt(0);

    }

    IEnumerator redButtonOnProces()
    {
        wordMap[0].GetComponent<Image>().sprite = SpriteList[wordValuesMap[0] * 4 + 2];
        yield return new WaitForSeconds(0.2f);

    }

    public void SetImageON(int indexOfSign, GameObject spriteRenderer)
    {
        StartCoroutine(animationProcess(indexOfSign,spriteRenderer));
    }

    public void SetImageONCorrect()
    {
        StartCoroutine(greenButtonOnProces());
    }

    public void SetImageONIrcorrect()
    {
        StartCoroutine(redButtonOnProces());
    }

    public void LoadNewWord(string word)
    { 
		word = word;
        foreach(char sign in word)
        {
            wordMap.Add(Instantiate(sprite, WordsListPanel.transform));
            wordValuesMap.Add((int)sign - 97);
            wordClickState.Add(true);
        }

		int counter = 0;
		foreach (GameObject sign in wordMap)
		{
			SetImageON(wordValuesMap[counter], sign);
			counter++;
		}
    }

}
