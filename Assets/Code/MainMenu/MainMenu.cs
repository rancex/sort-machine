using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    private Vector3 dragOrigin;
    public Score scoreManager;

    public bool animationFinish = true;
    public Text sortName;
    public string sortNameString = "";

    public GameObject tutorialPanel;

    //1 = main menu
    //2 = is Choosing Sort
    //3 = Look at high score + play
    //4 = Is looking how to play
    public int state = 1;

    public bool isChoosingSort = false;

    public GameObject targetObject;

    void Start() {
        int returnFromGame = PlayerPrefs.GetInt("returnFromGame", 0);

        if(returnFromGame == 1) {
            isChoosingSort = true;
            targetObject.transform.localPosition = new Vector3(-1920.0f, targetObject.transform.position.y, targetObject.transform.position.z);
            state = 2;
            PlayerPrefs.SetInt("returnFromGame", 0);
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (state == 1) {

            if (Input.GetMouseButtonDown(0)) {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            if (Input.mousePosition.x - dragOrigin.x < 0) {
                isChoosingSort = true;
                StartCoroutine(smoothMoveToPosition(1.0f, -1920.0f));
                state = 2;
            }
        }
    }

    public void startBubbleSort() {
        if (animationFinish == true) {
            PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.BUBBLESORT);
            sortNameString = "BUBBLE\r\nSORT";
            showPlayMenu();
        }
    }

    public void startSelectionSort() {
        if (animationFinish == true) {
            PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SELECTIONSORT);
            sortNameString = "SELECTION\r\nSORT";
            showPlayMenu();
        }
    }

    public void startInsertionSort() {
        if (animationFinish == true) {
            PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.INSERTIONSORT);
            sortNameString = "INSERTION\r\nSORT";
            showPlayMenu();
        }
    }

    public void startShellSort() {
        if (animationFinish == true) {
            PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SHELLSORT);
            sortNameString = "SHELL\r\nSORT";
            showPlayMenu();
        }
    }

    public void startHeapSort() {
        if (animationFinish == true) {
            PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.HEAPSORT);
            sortNameString = "HEAP\r\nSORT";
            showPlayMenu();
        }
    }

    public void startMergeSort() {
        if (animationFinish == true) {
            PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.MERGESORT);
            sortNameString = "MERGE\r\nSORT";
            showPlayMenu();
        }
    }

    public void startQuickSort() {
        if (animationFinish == true) {
            PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.QUICKSORT);
            sortNameString = "QUICK\r\nSORT";
            showPlayMenu();
        }
    }

    public void showPlayMenu() {
        scoreManager.setSortType();
        scoreManager.showLeaderboard();
        sortName.text = sortNameString;
        StartCoroutine(smoothMoveToPosition(1.0f, -3840.0f));
        state = 3;
    }

    public void btnPlayPressed() {

        targetObject.transform.localPosition = new Vector3(-5760.0f, targetObject.transform.localPosition.y,targetObject.transform.localPosition.z);

        int sortType = PlayerPrefs.GetInt("sorttype");

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            SceneChooser.startBubbleSort();
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            SceneChooser.startSelectionSort();
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            SceneChooser.startInsertionSort();
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            SceneChooser.startShellSort();
        }
        else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
            SceneChooser.startHeapSort();
        }
        else if (sortType == KeyDictionary.SORTTYPE.MERGESORT) {
            SceneChooser.startMergeSort();
        }
        else if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            SceneChooser.startQuickSort();
        }
    }

    IEnumerator smoothMoveToPosition(float time, float targetPos) {
        float elapsedTime = 0;

        animationFinish = false;

        while (elapsedTime < time) {
            targetObject.transform.localPosition = new Vector3(Mathf.Lerp(targetObject.transform.localPosition.x, targetPos, (elapsedTime / time)),
                                                  targetObject.transform.localPosition.y,
                                                  0.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        animationFinish = true;
    }

    public void btnBackPressed() {
        isChoosingSort = true;
        StartCoroutine(smoothMoveToPosition(1.0f, -1920.0f));
        state = 2;
    }

    public void showTutorial() {
        tutorialPanel.SetActive(true);
        tutorialPanel.GetComponent<InfoTutorial>().showInitialPage();
        tutorialPanel.GetComponent<InfoTutorial>().modifyImage();
    }

    public void hideTutorial() {
        tutorialPanel.SetActive(false);
    }
}
