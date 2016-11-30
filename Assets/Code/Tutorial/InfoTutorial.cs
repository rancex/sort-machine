using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InfoTutorial : MonoBehaviour {

    private List<GameObject> tutorialImageObjects = new List<GameObject>();

    public List<Sprite> basicTutorialSprites = new List<Sprite>();
    public List<Sprite> heapSortImages = new List<Sprite>();
    public List<Sprite> mergeSortImages = new List<Sprite>();
    public List<Sprite> quickSortImages = new List<Sprite>();

    public List<Sprite> bubbleSortImages = new List<Sprite>();
    public List<Sprite> insertionSortImages = new List<Sprite>();
    public List<Sprite> selectionSortImages = new List<Sprite>();
    public List<Sprite> shellSortImages = new List<Sprite>();

    public GameObject tutorialContainer;

    public GameObject tutorialImageObject;

    public GameObject closeButton;

    public Image tutorialImage;
    
    public int sortType = 0;
    public int pageNum = 0;

    public int amountOfPages = 0;

    private bool isAtMainMenu = false;

    public Text pageNumText;

    public GameObject prevButton;
    public GameObject nextButton;

	// Use this for initialization
	void Start () {
        sortType = PlayerPrefs.GetInt("sorttype", 0);
        if (SceneManager.GetActiveScene().name == "mainmenu") {
            isAtMainMenu = true;
            this.gameObject.SetActive(false);
        }
        else {
            isAtMainMenu = false;
            if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
                amountOfPages = basicTutorialSprites.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
                amountOfPages = basicTutorialSprites.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
                amountOfPages = basicTutorialSprites.Count;
            }
            else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
                amountOfPages = basicTutorialSprites.Count;
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

            //instantiateImage();
            
        }
        this.gameObject.SetActive(false);
	}

    private Vector3 dragOrigin;
    private bool animationFinish = true;

    // Update is called once per frame
    void Update() {
        if (isAtMainMenu == false) {
            if (Input.GetMouseButtonDown(0)) {
                showNextPage();
                return;
            }
        }
        /*
        else {
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
        */
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

        sortType = PlayerPrefs.GetInt("sorttype", 0);

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            if(isAtMainMenu == true) {
                return bubbleSortImages[tutorialNum];
            }
            return basicTutorialSprites[tutorialNum];
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            if (isAtMainMenu == true) {
                return selectionSortImages[tutorialNum];
            }
            return basicTutorialSprites[tutorialNum];
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            if (isAtMainMenu == true) {
                return insertionSortImages[tutorialNum];
            }
            return basicTutorialSprites[tutorialNum];
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            if (isAtMainMenu == true) {
                return shellSortImages[tutorialNum];
            }
            return basicTutorialSprites[tutorialNum];
        }
        else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
            return heapSortImages[tutorialNum];
        }
        else if (sortType == KeyDictionary.SORTTYPE.MERGESORT) {
            return mergeSortImages[tutorialNum];
        }
        else if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            return quickSortImages[tutorialNum];
        }

        else return basicTutorialSprites[tutorialNum];
    }

    public void showInitialPage() {
        pageNum = 0;
        tutorialImage.sprite = returnTutorialSpriteByPage(pageNum);

        if (isAtMainMenu) {
            pageNumText.text = (pageNum + 1) + " / " + amountOfPages;
            prevButton.SetActive(false);
            nextButton.SetActive(true);
        }
        //tutorialContainer.transform.localPosition = new Vector3(0.0f, tutorialContainer.transform.localPosition.y, tutorialContainer.transform.localPosition.z);
        //tutorialImage.sprite = returnTutorialSpriteByPage(pageNum);
    }

    public void showNextPage() {
        pageNum++;
        if (isAtMainMenu) prevButton.SetActive(true);
        if (pageNum < amountOfPages) {
            tutorialImage.sprite = returnTutorialSpriteByPage(pageNum);
            if (isAtMainMenu) {
                pageNumText.text = (pageNum + 1) + " / " + amountOfPages;
                if(pageNum == amountOfPages - 1) {
                    nextButton.SetActive(false);
                }
            }
            //StartCoroutine(smoothMoveToPosition(1.0f, -1920.0f * pageNum));
        }
        else {
            pageNum = amountOfPages - 1;
            if (isAtMainMenu == false) {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().hideTutorialPanel();
            }
        }
        
    }

    public void showPrevPage() {
        pageNum--;
        if (isAtMainMenu) nextButton.SetActive(true);
        if (pageNum >= 0) {
            //StartCoroutine(smoothMoveToPosition(1.0f, -1920.0f * pageNum));
            tutorialImage.sprite = returnTutorialSpriteByPage(pageNum);
            if (isAtMainMenu) {
                pageNumText.text = (pageNum + 1) + " / " + amountOfPages;
                if(pageNum == 0) {
                    prevButton.SetActive(false);
                }
            }
        }
        else {
            pageNum = 0;
        }
    }

    public void instantiateImage() {

        List<Sprite> tempList = new List<Sprite>();

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
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
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            amountOfPages = basicTutorialSprites.Count;
            tempList = basicTutorialSprites;
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

    public void clickForNextPage() {
        
    }

    public void showTutorialMainMenu() {
        
        sortType = PlayerPrefs.GetInt("sorttype", 0);

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            amountOfPages = bubbleSortImages.Count;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            amountOfPages = selectionSortImages.Count;
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            amountOfPages = insertionSortImages.Count;
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            amountOfPages = shellSortImages.Count;
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

        showInitialPage();
    }
}
