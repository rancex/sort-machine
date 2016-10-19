using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InfoTutorial : MonoBehaviour {

    private List<GameObject> tutorialImageObjects = new List<GameObject>();

    public List<Sprite> tutorialSprites = new List<Sprite>();
    public List<Sprite> heapSortImages = new List<Sprite>();
    public List<Sprite> mergeSortImages = new List<Sprite>();
    public List<Sprite> quickSortImages = new List<Sprite>();

    public GameObject tutorialContainer;

    public GameObject tutorialImageObject;

    public GameObject closeButton;

    public Image tutorialImage;
    
    public int sortType = 0;
    public int pageNum = 0;

    public int amountOfPages = 0;

	// Use this for initialization
	void Start () {
        sortType = PlayerPrefs.GetInt("sorttype", 0);
        if (SceneManager.GetActiveScene().name == "mainmenu") {

        }
        else {

            if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
                amountOfPages = tutorialSprites.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
                amountOfPages = tutorialSprites.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
                amountOfPages = tutorialSprites.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
                amountOfPages = tutorialSprites.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
                amountOfPages = heapSortImages.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.MERGESORT) {
                amountOfPages = mergeSortImages.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
                amountOfPages = quickSortImages.Count;
            }

            instantiateImage();
            
        }
        this.gameObject.SetActive(false);
	}

    private Vector3 dragOrigin;
    private bool animationFinish = true;

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        if (Input.mousePosition.x - dragOrigin.x < 0) {
            if (animationFinish == true) {
                showNextPage();
                
            }
        }
        if (Input.mousePosition.x - dragOrigin.x > 0) {
            if (animationFinish == true) {
                showPrevPage();
                
            }
        }
    }

    IEnumerator smoothMoveToPosition(float time, float targetPos) {
        float elapsedTime = 0;

        animationFinish = false;

        closeButton.SetActive(false);

        while (elapsedTime < time) {
            tutorialContainer.transform.localPosition = new Vector3(Mathf.Lerp(tutorialContainer.transform.localPosition.x, targetPos, (elapsedTime / time)),
                                                  tutorialContainer.transform.localPosition.y,
                                                  0.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        closeButton.SetActive(true);

        animationFinish = true;
    }

    /*
    public Sprite returnTutorialSprite(int tutorialNum) {

    }
    */

    public Sprite returnTutorialSpriteByPage(int tutorialNum) {
        return tutorialSprites[tutorialNum];
    }

    public void showInitialPage() {
        pageNum = 0;
        tutorialContainer.transform.localPosition = new Vector3(0.0f, tutorialContainer.transform.localPosition.y, tutorialContainer.transform.localPosition.z);
        //tutorialImage.sprite = returnTutorialSpriteByPage(pageNum);
    }

    public void showNextPage() {
        pageNum++;
        if(pageNum < amountOfPages) {
            //tutorialImage.sprite = returnTutorialSpriteByPage(pageNum);
            StartCoroutine(smoothMoveToPosition(1.0f, -1920.0f * pageNum));
        }
        else {
            pageNum = amountOfPages - 1;
        }
        
    }

    public void showPrevPage() {
        pageNum--;
        if (pageNum >= 0) {
            StartCoroutine(smoothMoveToPosition(1.0f, -1920.0f * pageNum));
            //tutorialImage.sprite = returnTutorialSpriteByPage(pageNum);
        }
        else {
            pageNum = 0;
        }
    }

    public void instantiateImage() {

        List<Sprite> tempList = new List<Sprite>();

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
            amountOfPages = heapSortImages.Count;
            tempList = heapSortImages;
        }
        else if (sortType == KeyDictionary.SORTTYPE.MERGESORT) {
            amountOfPages = mergeSortImages.Count;
            tempList = mergeSortImages;
        }
        else if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            amountOfPages = quickSortImages.Count;
            tempList = quickSortImages;
        }

        for (int i = 0;i < amountOfPages; i++) {
            GameObject g = Instantiate(tutorialImageObject, new Vector3(1920.0f * i, 14.5f,0.0f), Quaternion.identity) as GameObject;
            g.GetComponent<Image>().sprite = tempList[i];
            g.transform.SetParent(tutorialContainer.transform, false);
            g.SetActive(true);
        }
        closeButton.transform.SetAsLastSibling();
    }

    public void modifyImage() {
        sortType = PlayerPrefs.GetInt("sorttype", 0);

        List<Sprite> tempList = new List<Sprite>();

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            amountOfPages = tutorialSprites.Count;
            tempList = tutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
            amountOfPages = heapSortImages.Count;
            tempList = heapSortImages;
        }
        else if (sortType == KeyDictionary.SORTTYPE.MERGESORT) {
            amountOfPages = mergeSortImages.Count;
            tempList = mergeSortImages;
        }
        else if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            amountOfPages = quickSortImages.Count;
            tempList = quickSortImages;
        }

        if (tutorialImageObjects.Count == 0) {
            for (int i = 0; i < 5; i++) {
                GameObject g = Instantiate(tutorialImageObject, new Vector3(1920.0f * i, 14.5f, 0.0f), Quaternion.identity) as GameObject;

                if(i < amountOfPages) g.GetComponent<Image>().sprite = tempList[i];

                g.transform.SetParent(tutorialContainer.transform, false);
                g.SetActive(true);
                tutorialImageObjects.Add(g);
            }
        }
        else {
            for (int i = 0; i < amountOfPages; i++) {
                tutorialImageObjects[i].GetComponent<Image>().sprite = tempList[i];
            }
        }
        closeButton.transform.SetAsLastSibling();
    }
}
