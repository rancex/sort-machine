using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //determines the length of the gap between objects
    public float objectGap = 2.5f;

    //bubble sort-----------------------------
    public int lastHighestNumber;
    //----------------------------------------

    //Selection sort--------------------------
    public bool waitForSwitch = false;
    //----------------------------------------

    //Insertion sort--------------------------
    public bool waitForInsert = false;
    //check if object behind border is lower than inside border
    public bool biggerInsert = false;
    //----------------------------------------

    //Shell sort------------------------------
    //Determines the size of the initial gap.
    public int gapLength = 0;
    //----------------------------------------

    //Quick Sort------------------------------
    //Stores the chosen object in quicksort
    public GameObject chosenQuickObject;
    public GameObject switchTrigger;
    public List<GameObject> triggerList = new List<GameObject>();
    public GameObject pivotPoint;
    public bool waitingForPivot = true;
    //----------------------------------------

    //Heap Sort-------------------------------
    public List<GameObject> heapList;
    public GameObject heapSwitchTrigger;
    public List<GameObject> heapTriggerList = new List<GameObject>();
    public GameObject chosenHeapObject = null;
    //----------------------------------------

    public int sortType;

    public CraneManager craneManagerScript;

    public ProgrammableMove programMove;

    //The Prefab For Car
    public GameObject carObject;

    //List of prefabs
    public List<GameObject> carList = new List<GameObject>();
    //List of original Prefabs
    public List<int> lastCorrectCarNumberList = new List<int>();
    

    //The Prefab for Documents
    public GameObject docObject;
    public List<GameObject> docList = new List<GameObject>();

    //The prefab for Crane
    public GameObject cranePrefab;


    public float startXpos = -6.5f;
    public float startYpos = -13.0f;

    //Decides the amount of objects in the scene
    public int objectAmount = 6;

    public GameObject TVMask;

    // Use this for initialization
    void Start() {

        //PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.BUBBLESORT);
        //PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SELECTIONSORT);
        //PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.INSERTIONSORT);
        //PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SHELLSORT);
        //PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.QUICKSORT);
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.HEAPSORT);
        //PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.MERGESORT);

        sortType = PlayerPrefs.GetInt("sorttype");

        if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Quick Sort");
            startXpos = -6f;
        }

        if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
            generateHeap();
            sortType = KeyDictionary.SORTTYPE.HEAPSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Heap Sort");
        }
        else {
            generateCars();
        }

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Bubble Sort");


            craneManagerScript.craneOne = Instantiate(cranePrefab, new Vector3(carList[0].transform.position.x, startYpos + 3f, -4), Quaternion.identity) as GameObject;
            craneManagerScript.craneTwo = Instantiate(cranePrefab, new Vector3(carList[1].transform.position.x, startYpos + 3f, -4), Quaternion.identity) as GameObject;

            craneManagerScript.highlightCrane(1, 2);
            craneManagerScript.highlightCrane(2, 2);

            craneManagerScript.maxCranePosition = carList.Count - 2;
        }
        if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            sortType = KeyDictionary.SORTTYPE.SELECTIONSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Selection Sort");

            craneManagerScript.craneOne = Instantiate(cranePrefab, new Vector3(carList[0].transform.position.x, startYpos + 3f, -4), Quaternion.identity) as GameObject;
            craneManagerScript.craneTwo = Instantiate(cranePrefab, new Vector3(carList[1].transform.position.x, startYpos + 3f, -4), Quaternion.identity) as GameObject;

            craneManagerScript.highlightCrane(1, 1);
            craneManagerScript.highlightCrane(2, 2);

            craneManagerScript.maxCranePosition = carList.Count - 1;
        }
        if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            sortType = KeyDictionary.SORTTYPE.INSERTIONSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Insertion Sort");

            craneManagerScript.craneOne = Instantiate(cranePrefab, new Vector3(carList[0].transform.position.x, startYpos + 3f, -4), Quaternion.identity) as GameObject;
            craneManagerScript.craneTwo = Instantiate(cranePrefab, new Vector3(carList[1].transform.position.x, startYpos + 3f, -4), Quaternion.identity) as GameObject;

            craneManagerScript.highlightCrane(1, 1);
            craneManagerScript.highlightCrane(2, 2);

            craneManagerScript.craneOne.GetComponent<ClawScript>().changeSpriteToSpotlight();

            craneManagerScript.maxCranePosition = carList.Count - 1;
        }
        if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            sortType = KeyDictionary.SORTTYPE.SHELLSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Shell Sort");

            /*
            craneManagerScript.craneOne = Instantiate(cranePrefab, new Vector3(carList[0].transform.position.x, 2.5f, -4), Quaternion.identity) as GameObject;
            craneManagerScript.craneTwo = Instantiate(cranePrefab, new Vector3(carList[(carList.Count-1)].transform.position.x, 2.5f, -4), Quaternion.identity) as GameObject;
            */

            craneManagerScript.craneOne.transform.position = new Vector3(carList[0].transform.position.x, 2.5f, -4);
            craneManagerScript.craneTwo.transform.position = new Vector3(carList[(carList.Count - 1)].transform.position.x, 2.5f, -4);

            craneManagerScript.highlightCrane(1, 1);
            craneManagerScript.highlightCrane(2, 2);

            craneManagerScript.maxCranePosition = carList.Count - 1;
            initShell();
            //craneManagerScript.returnCrane(false);
        }


        /*
        if (SceneManager.GetActiveScene().name == KeyDictionary.SCENES.BUBBLESORT) {
            sortType = KeyDictionary.SORTTYPE.BUBBLESORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Bubble Sort");
            craneManagerScript.maxCranePosition = carList.Count - 2;
        }
        if (SceneManager.GetActiveScene().name == KeyDictionary.SCENES.SELECTIONSORT) {
            sortType = KeyDictionary.SORTTYPE.SELECTIONSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Selection Sort");
            craneManagerScript.maxCranePosition = carList.Count - 1;
        }
        if (SceneManager.GetActiveScene().name == KeyDictionary.SCENES.INSERTIONSORT) {
            sortType = KeyDictionary.SORTTYPE.INSERTIONSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Insertion Sort");
            craneManagerScript.maxCranePosition = carList.Count - 1;
        }
        if (SceneManager.GetActiveScene().name == KeyDictionary.SCENES.SHELLSORT) {
            sortType = KeyDictionary.SORTTYPE.SHELLSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Shell Sort");

            craneManagerScript.maxCranePosition = carList.Count - 1;
            initShell();
            craneManagerScript.returnCrane();
        }
        if (SceneManager.GetActiveScene().name == KeyDictionary.SCENES.QUICKSORT) {
            sortType = KeyDictionary.SORTTYPE.QUICKSORT;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Quick Sort");    
        }
        */

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT ||
            sortType == KeyDictionary.SORTTYPE.SELECTIONSORT ||
            sortType == KeyDictionary.SORTTYPE.INSERTIONSORT ||
            sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            GameObject.Find("IdealSolver").GetComponent<IdealSolutionAuto>().findIdealSolution();
        }
    }

    void generateCars() {

        List<int> numberList = Shuffler.generateRandomList(objectAmount);

        int highestNumber = 0;

        for (int i = 0; i < objectAmount; i++) {
            GameObject car = Instantiate(carObject, new Vector3(startXpos + i * objectGap, startYpos, 0), Quaternion.identity) as GameObject;

            #region quicksort
            if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
                //car.GetComponent<BoxClick>().enabled = false;
                car.GetComponent<BoxCollider2D>().enabled = true;

                GameObject switchTriggerObject = Instantiate(switchTrigger,
                                                         new Vector3(car.transform.position.x,
                                                                     car.transform.position.y,
                                                                     -1.0f),
                                                         Quaternion.identity) as GameObject;
                switchTriggerObject.GetComponent<SwitchTriggerScript>().insertIndex(i);
                triggerList.Add(switchTriggerObject);
            }
            #endregion

            car.GetComponent<CarMainScript>().insertNumber(numberList[i]);
            car.GetComponent<CarMainScript>().insertIndex(i);

            car.SetActive(true);

            lastCorrectCarNumberList.Add(numberList[i]);

            if (highestNumber < numberList[i]) highestNumber = numberList[i];

            carList.Add(car);
        }

        lastHighestNumber = highestNumber;

        programMove.setAvailableMoves();
    }

    public void restartCarPositions() {
        for(int i = 0;i< carList.Count; i++) {
            carList[i].GetComponent<CarMainScript>().changeNumber(lastCorrectCarNumberList[i]);
        }
        craneManagerScript.returnCrane(true);

        switch (sortType) {
            case KeyDictionary.SORTTYPE.SELECTIONSORT:
                {
                    markObject(-1);
                    break;
                }
        }
    }

    void generateHeap() {

        int treeIdx;

        GameObject lastParent = null;

        List<int> numberList = Shuffler.generateRandomList(7);

        for (treeIdx = 0; treeIdx < 7; treeIdx++) {

            GameObject node = Instantiate(docObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

            node.GetComponent<DocumentScript>().insertId(treeIdx);

            if (lastParent == null) {
                lastParent = node;
            }
            else {
                if (lastParent.GetComponent<DocumentScript>().leftChild == null) {
                    lastParent.GetComponent<DocumentScript>().leftChild = node;
                    node.GetComponent<DocumentScript>().parent = lastParent;
                    node.GetComponent<DocumentScript>().childDirection = KeyDictionary.TREECHILDDIRECTION.LEFT;
                }
                else if (lastParent.GetComponent<DocumentScript>().rightChild == null) {
                    lastParent.GetComponent<DocumentScript>().rightChild = node;
                    node.GetComponent<DocumentScript>().parent = lastParent;
                    node.GetComponent<DocumentScript>().childDirection = KeyDictionary.TREECHILDDIRECTION.RIGHT;
                }
                else{
                    int id = lastParent.GetComponent<DocumentScript>().id;
                    lastParent = heapList[id + 1];
                    lastParent.GetComponent<DocumentScript>().leftChild = node;
                    node.GetComponent<DocumentScript>().parent = lastParent;
                    node.GetComponent<DocumentScript>().childDirection = KeyDictionary.TREECHILDDIRECTION.LEFT;
                }
            }

            node.GetComponent<DocumentScript>().insertNumber(numberList[treeIdx]);

            node.GetComponent<DocumentScript>().decidePosition();

            node.GetComponent<DocumentScript>().originalScale = node.transform.localScale;

            //heapify-----------------
            node.GetComponent<DocumentScript>().heapifySelf();
            //------------------------

            heapList.Add(node);

            GameObject trigger = Instantiate(heapSwitchTrigger, node.transform.position, Quaternion.identity) as GameObject;
            trigger.transform.localScale = new Vector2(node.transform.localScale.x * 4, node.transform.localScale.y * 3);
            heapTriggerList.Add(trigger);
        }
    }

    private Vector3 mousePosition;
    private float moveSpeed = 5.0f;
    private float scaleSpeed = 0.1f;
    private Vector3 originalScale = new Vector3(0.9f,0.9f,1.0f);
    private Vector3 targetScale = new Vector3(1.1f, 1.1f,1.0f);

    private Coroutine cor = null;

    private bool isScalingUp = false;
    private bool isScalingDown = false;

    public GameObject animationTarget = null;

    //quicksort only
    public bool isAutoSolvingQuick;

    void Update() {

        #region quicksort
        if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            if (Input.GetMouseButton(0)) {
                isScalingDown = false;

                if (chosenQuickObject != null) {
                    chosenQuickObject.GetComponent<CarMainScript>().stopCoroutineImmediately();
                    moveObjectWhenTouching();

                    if (isScalingUp == false) {
                        isScalingUp = true;
                        cor = StartCoroutine(LerpScale(scaleSpeed, targetScale, chosenQuickObject));
                    }
                }
            }
            else {
                isScalingUp = false;
                if (chosenQuickObject != null) {
                    if (isScalingDown == false) {
                        isScalingDown = true;
                        cor = StartCoroutine(LerpScale(scaleSpeed, originalScale, chosenQuickObject));
                    }
                    chosenQuickObject.GetComponent<CarMainScript>().moveToTriggerPosition(3.0f);
                    chosenQuickObject.GetComponent<CarMainScript>().canMove = true;

                    if(isAutoSolvingQuick == false)
                    chosenQuickObject = null;
                }
            }
        }
        #endregion

        #region heapsort
        else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
            if (Input.GetMouseButton(0)) {
                isScalingDown = false;

                if (chosenHeapObject != null) {
                    //chosenHeapObject.GetComponent<CarMainScript>().stopCoroutineImmediately();
                    moveObjectWhenTouching();

                    if (isScalingUp == false) {
                        isScalingUp = true;
                        targetScale = chosenHeapObject.GetComponent<DocumentScript>().originalScale * 1.2f;
                        cor = StartCoroutine(LerpScale(scaleSpeed, targetScale, chosenHeapObject));
                    }
                }
            }
            else {
                isScalingUp = false;
                if (chosenHeapObject != null) {
                    if (isScalingDown == false) {
                        isScalingDown = true;
                        cor = StartCoroutine(LerpScale(scaleSpeed, chosenHeapObject.GetComponent<DocumentScript>().originalScale, chosenHeapObject));
                    }
                    //chosenHeapObject.GetComponent<CarMainScript>().moveToTriggerPosition(3.0f);
                    //chosenHeapObject.GetComponent<CarMainScript>().canMove = true;
                    //chosenHeapObject = null;
                }
            }
        }
        #endregion
    }

    private IEnumerator LerpScale(float time, Vector3 targetScaleVector,GameObject targetObject) {

        Vector2 originalScaleVector = targetObject.transform.localScale;
        float elapsedTime = 0;

        while (elapsedTime < time) {
            targetObject.transform.localScale = new Vector3(Mathf.Lerp(originalScaleVector.x, targetScaleVector.x, (elapsedTime / time)),
                                                            Mathf.Lerp(originalScaleVector.y, targetScaleVector.y, (elapsedTime / time)),
                                                            1.0f);
            
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    /// <summary>
    /// for quicksort, when touching the screen box position will follow
    /// for heapsort, when touching the screen doc position will follow
    /// </summary>
    void moveObjectWhenTouching() {

        /*
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        targetPos = Camera.main.ScreenToWorldPoint(targetPos);

        Vector3 followXonly = new Vector3(targetPos.x, transform.position.y, transform.position.z);
        chosenQuickObject.transform.position = Vector3.Lerp(transform.position, followXonly, 2.0f * Time.deltaTime);
        
        */
        if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            if (chosenQuickObject.GetComponent<CarMainScript>().canMove == true) {
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                if (mousePosition.x > 6.3f) mousePosition.x = 6.3f;
                if (mousePosition.x < -6.3f) mousePosition.x = -6.3f;

                chosenQuickObject.transform.position = new Vector3(Mathf.Lerp(chosenQuickObject.transform.position.x, mousePosition.x, moveSpeed * Time.deltaTime),
                                                                   chosenQuickObject.transform.position.y,
                                                                   -1.0f);

            }
        }
        else if(sortType == KeyDictionary.SORTTYPE.HEAPSORT){
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            chosenHeapObject.transform.position = new Vector3(Mathf.Lerp(chosenHeapObject.transform.position.x, mousePosition.x, moveSpeed * Time.deltaTime),
                                                               Mathf.Lerp(chosenHeapObject.transform.position.y,mousePosition.y,moveSpeed * Time.deltaTime),
                                                               -1.0f);
        }

           // Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    //Initialize the value of initial gap
    public void initShell() {
        gapLength = (carList.Count - 1);
        craneManagerScript.cranePosition2 = carList.Count - 1;
    }

    public void highlightCar(int carNumber) {
        if(sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {

        }
    }

    public bool switchObject() {

        bool canSwitch = true;

        #region bubblesort
        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) { 
            Debug.Log('w');
            int position = craneManagerScript.cranePosition;
            int maxPosition = craneManagerScript.maxCranePosition;

            int tempValue = carList[position].GetComponent<CarMainScript>().carNumber;
            float tempXPos = carList[position].transform.position.x;

            /*
            carList[position].transform.position = new Vector3( carList[position + 1].transform.position.x,
                                                                carList[position].transform.position.y,
                                                                carList[position].transform.position.z);

            carList[position+1].transform.position = new Vector3(tempXPos,
                                                                carList[position].transform.position.y,
                                                                carList[position].transform.position.z);
            */
            //carList[position].GetComponent<CarMainScript>().changeNumber(carList[position + 1].GetComponent<CarMainScript>().carNumber);

            //carList[position + 1].GetComponent<CarMainScript>().changeNumber(tempValue);

            craneManagerScript.craneOne.GetComponent<ClawScript>().pinchItem(position, position + 1);
            craneManagerScript.craneTwo.GetComponent<ClawScript>().pinchItem(position + 1, position);
            /*
            if(position == maxPosition) {
                Debug.Log("wtf");
                if (checkIfEndHighest() == true) {
                    programMove.prepareNextLoop();
                    craneManagerScript.cranePosition++;
                }
            }
            */
        }
        #endregion

        #region selectionsort
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            
            //if (waitForSwitch == true) {
            int position1 = craneManagerScript.cranePosition;
            int position2 = craneManagerScript.markedPosition;

            if (position2 != -1) {

                int tempValue = carList[position1].GetComponent<CarMainScript>().carNumber;
                float tempXPos = carList[position1].transform.position.x;

                craneManagerScript.craneTwo.transform.position = new Vector3(carList[position2].transform.position.x,
                                                                             craneManagerScript.craneTwo.transform.position.y,
                                                                             craneManagerScript.craneTwo.transform.position.z);

                craneManagerScript.craneOne.GetComponent<ClawScript>().pinchItem(position1, position2);
                craneManagerScript.craneTwo.GetComponent<ClawScript>().pinchItem(position2, position1);

                //carList[position1].GetComponent<CarMainScript>().changeNumber(carList[position2].GetComponent<CarMainScript>().carNumber);

                //carList[position2].GetComponent<CarMainScript>().changeNumber(tempValue);

                //craneManagerScript.returnCrane(false);
                //waitForSwitch = false;
                //cleanObjects();

                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Objects are switched.");
                //}
            }
            else {
                canSwitch = false;
            }
        }
        #endregion

        #region insertionsort
        //WELL IT IS ACTUALLY INSERTING THEN PUSHING BUT WHATEVER
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            int cranePositionRed = craneManagerScript.cranePosition;
            int cranePositionGreen = craneManagerScript.cranePosition2;

            int tempValue = carList[cranePositionGreen].GetComponent<CarMainScript>().carNumber;

            /*
            for (int i = cranePositionGreen; i > cranePositionRed; i--) {
                //carList[i].GetComponent<CarMainScript>().changeNumber(carList[i-1].GetComponent<CarMainScript>().carNumber);

            }
            */

            //carList[cranePositionRed].GetComponent<CarMainScript>().changeNumber(tempValue);

            craneManagerScript.craneTwo.GetComponent<ClawScript>().pinchItem(cranePositionGreen, cranePositionRed);

            //int tempValue = carList[targetPosition].GetComponent<CarMainScript>().carNumber;

            /*
            if (cranePosition >= 0) {
                for (int i = targetPosition; i > cranePosition; i--) {
                    carList[i].GetComponent<CarMainScript>().changeNumber(carList[i - 1].GetComponent<CarMainScript>().carNumber);
                }
                carList[cranePosition + 1].GetComponent<CarMainScript>().changeNumber(tempValue);
            }
            else {
                for (int i = targetPosition; i > cranePosition + 1; i--) {
                    carList[i].GetComponent<CarMainScript>().changeNumber(carList[i - 1].GetComponent<CarMainScript>().carNumber);
                }
                carList[0].GetComponent<CarMainScript>().changeNumber(tempValue);
            }
            */

            /*
            cleanObjects();
            markObject();
            */
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Objects are switched.");
        }

        
        #endregion

        #region shellsort
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            int position1 = craneManagerScript.cranePosition;
            int position2 = craneManagerScript.cranePosition2;

            craneManagerScript.craneOne.GetComponent<ClawScript>().pinchItem(position1, position2);
            craneManagerScript.craneTwo.GetComponent<ClawScript>().pinchItem(position2, position1);

            /*
            int tempValue = carList[position1].GetComponent<CarMainScript>().carNumber;
            float tempXPos = carList[position1].transform.position.x;

            
            carList[position].transform.position = new Vector3( carList[position + 1].transform.position.x,
                                                                carList[position].transform.position.y,
                                                                carList[position].transform.position.z);

            carList[position+1].transform.position = new Vector3(tempXPos,
                                                                carList[position].transform.position.y,
                                                                carList[position].transform.position.z);
            
            carList[position1].GetComponent<CarMainScript>().changeNumber(carList[position2].GetComponent<CarMainScript>().carNumber);

            carList[position2].GetComponent<CarMainScript>().changeNumber(tempValue);
            */
        }
        #endregion

        #region heapsort
        else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {

        }
        #endregion

        #region mergesort
        else if (sortType == KeyDictionary.SORTTYPE.MERGESORT) {

        }
        #endregion

        return canSwitch;
    }

    public void moveItems() {

        int cranePositionRed = craneManagerScript.cranePosition;
        int cranePositionGreen = craneManagerScript.cranePosition2;

        for (int i = cranePositionRed; i < cranePositionGreen; i++) {
            carList[i].GetComponent<CarMainScript>().moveToPosition(carList[i].transform.position.x + objectGap);
        }
    }

    bool craneOneFinished;
    bool craneTwoFinished;

    int targetObjNumOne = 0;
    int targetObjNumTwo = 0;

    public void afterSwitch(GameObject craneObj, int posNumber) {

        if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            afterSwitchInsertion();
        }

        else {
            if (craneManagerScript.craneOne == craneObj) {
                craneOneFinished = true;
                targetObjNumOne = posNumber;
            }
            else if (craneManagerScript.craneTwo == craneObj) {
                craneTwoFinished = true;
                targetObjNumTwo = posNumber;
            }

            if (craneOneFinished == true && craneTwoFinished == true) {
                craneOneFinished = false;
                craneTwoFinished = false;

                craneManagerScript.reverseCrane();

                Vector3 tempPos = carList[targetObjNumOne].transform.position;

                carList[targetObjNumOne].transform.position = carList[targetObjNumTwo].transform.position;
                carList[targetObjNumTwo].transform.position = tempPos;

                int tempNumber = carList[targetObjNumOne].GetComponent<CarMainScript>().carNumber;

                carList[targetObjNumOne].GetComponent<CarMainScript>().changeNumber(carList[targetObjNumTwo].GetComponent<CarMainScript>().carNumber);

                carList[targetObjNumTwo].GetComponent<CarMainScript>().changeNumber(tempNumber);

                programMove.GetComponent<ProgrammableMove>().checkAnimation();
            }
        }
    }

    public void afterSwitchInsertion() {

        int cranePositionRed = craneManagerScript.cranePosition;
        int cranePositionGreen = craneManagerScript.cranePosition2;

        for (int i = cranePositionRed; i <= cranePositionGreen; i++) {
            //carList[i].GetComponent<CarMainScript>().changeNumber(carList[i-1].GetComponent<CarMainScript>().carNumber);
            carList[i].transform.position = new Vector3(startXpos + i * objectGap, carList[i].transform.position.y, carList[i].transform.position.z);
        }

        int tempValue = carList[cranePositionGreen].GetComponent<CarMainScript>().carNumber;

        for (int i = cranePositionGreen; i > cranePositionRed; i--) {
            carList[i].GetComponent<CarMainScript>().changeNumber(carList[i-1].GetComponent<CarMainScript>().carNumber);
        }
        
        carList[cranePositionRed].GetComponent<CarMainScript>().changeNumber(tempValue);

        int cranePosition2 = GameObject.Find("CraneObject").GetComponent<CraneManager>().cranePosition2;
        float craneXPosition = carList[cranePosition2].transform.position.x;

        craneManagerScript.craneTwo.transform.position = new Vector3(craneXPosition,
                                                                     craneManagerScript.craneTwo.transform.position.y, 
                                                                     craneManagerScript.craneTwo.transform.position.z);
        programMove.GetComponent<ProgrammableMove>().checkAnimation();
    }

    public void switchObjectHeap(GameObject parent,GameObject child) {
        int idxParent = parent.GetComponent<DocumentScript>().id;
        int idxChild= child.GetComponent<DocumentScript>().id;

        var Temp = heapList[idxParent];
        heapList[idxParent] = heapList[idxChild];
        heapList[idxChild] = Temp;
    }

    public int switchObjectQuick(int originalId, int targetId) {

        
        int tempIdx = carList[originalId].GetComponent<CarMainScript>().triggerIdx;
        carList[originalId].GetComponent<CarMainScript>().insertIndex(carList[targetId].GetComponent<CarMainScript>().triggerIdx);
        carList[targetId].GetComponent<CarMainScript>().insertIndex(tempIdx);
        

        //Debug.Log(carList[originalId].GetComponent<CarMainScript>().carNumber);

        var Temp = carList[targetId];
        carList[targetId] = carList[originalId];
        carList[originalId] = Temp;

        //Debug.Log(carList[originalId].GetComponent<CarMainScript>().carNumber);

        /*
        int tempValue = carList[originalId].GetComponent<CarMainScript>().carNumber;
        int tempIdx = carList[originalId].GetComponent<CarMainScript>().triggerIdx;

        /*
        Vector2 tempTransform = new Vector2(carList[originalId].GetComponent<CarMainScript>().triggerPosition.x,
                                            carList[originalId].GetComponent<CarMainScript>().triggerPosition.y);
        

        carList[originalId].GetComponent<CarMainScript>().changeNumber(carList[targetId].GetComponent<CarMainScript>().carNumber);
        carList[originalId].GetComponent<CarMainScript>().insertIndex(carList[targetId].GetComponent<CarMainScript>().triggerIdx);

        carList[targetId].GetComponent<CarMainScript>().changeNumber(tempValue);
        carList[targetId].GetComponent<CarMainScript>().insertIndex(tempIdx);
        */

        printList();

        return Temp.GetComponent<CarMainScript>().triggerIdx;
    }

    public void printList() {
        string str = "";
        string str2 = "";
        string str3 = "";
        foreach(GameObject g in carList) {
            str += (g.GetComponent<CarMainScript>().carNumber).ToString();
            str += " ";

            str2 += (g.GetComponent<CarMainScript>().triggerIdx).ToString();
            str2 += " ";

            //str3 += g.GetComponent<CarMainScript>().triggerPosition.x.ToString();
            str3 += " ";
        }
        //Debug.Log(str);
       // Debug.Log(str2);
        //Debug.Log(str3);
    }

    public bool checkIfSorted() {

        bool sorted = true;

        int front;

        int back = carList[0].GetComponent<CarMainScript>().carNumber;

        for (int i = 1; i < carList.Count; i++) {
            front = carList[i].GetComponent<CarMainScript>().carNumber;
            if (front < back) sorted = false;
            back = front;
        }

        return sorted;
    }

    //check if last number in position is the highest
    public bool checkIfEndHighest() {


        //Debug.Log(carList[craneManagerScript.maxCranePosition+1].GetComponent<CarMainScript>().carNumber);
        #region bubblesort
        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            if (carList[craneManagerScript.maxCranePosition + 1].GetComponent<CarMainScript>().carNumber == lastHighestNumber) {
                return true;
            }
            else return false;
        }
        #endregion

        #region selectionsort
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            if(carList[craneManagerScript.cranePosition].GetComponent<CarMainScript>().carNumber == lastHighestNumber) {
                return true;
            }
            return false;
        }
        #endregion

        #region insertionsort
        //check if the highest number at the end of sorted list is higher than the crane position
        else if(sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            //Value of the object in green crane
            int valuePosition = carList[craneManagerScript.cranePosition2].GetComponent<CarMainScript>().carNumber;
            //Value of the object in the rightmost of sorted list
            int valueTarget = carList[craneManagerScript.targetPosition].GetComponent<CarMainScript>().carNumber;

            Debug.Log("Position : " + valuePosition);
            Debug.Log("Target : " + valueTarget);

            if (valueTarget >= valuePosition) {
                return true;
            }
            else return false;
        }
        #endregion
        else return false;


    }

    //check if the value of the left side is higher than the right side
    public bool checkValue() {
        int position = craneManagerScript.cranePosition;
        int position2 = craneManagerScript.cranePosition2;

        #region bubblesort
        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            if (carList[position].GetComponent<CarMainScript>().carNumber > carList[position + 1].GetComponent<CarMainScript>().carNumber) {
                return false;
            }
            else return true;
        }
        #endregion

        #region selectionsort
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            int markedPos = craneManagerScript.markedPosition;

            if (carList[markedPos].GetComponent<CarMainScript>().carNumber >= carList[position2].GetComponent<CarMainScript>().carNumber) {
                return false;
            }
            else return true;
        }
        #endregion

        #region insertionsort
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            int valuePosition = carList[craneManagerScript.cranePosition].GetComponent<CarMainScript>().carNumber;
            int valueTarget = carList[craneManagerScript.targetPosition].GetComponent<CarMainScript>().carNumber;

            Debug.Log("Position : " + valuePosition);
            Debug.Log("Target : " + valueTarget);

            if (valueTarget >= valuePosition) {
                return true;
            }
            else return false;
        }
        #endregion

        #region shellsort
        if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {

            Debug.Log(carList[position].GetComponent<CarMainScript>().carNumber + " " + carList[position2].GetComponent<CarMainScript>().carNumber);
            if (carList[position].GetComponent<CarMainScript>().carNumber > carList[position2].GetComponent<CarMainScript>().carNumber) {
                return true;
            }
            else return false;
        }
        #endregion

        return false;
    }

    public void gameOver() {
        //GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().disableControls();

        switch (sortType) {
            case KeyDictionary.SORTTYPE.BUBBLESORT:
                {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("All objects are sorted.");
                    programMove.setGameOver();
                    break;
                }
            case KeyDictionary.SORTTYPE.SELECTIONSORT:
                {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("All objects are sorted.");
                    programMove.setGameOver();
                    break;
                }
            case KeyDictionary.SORTTYPE.INSERTIONSORT:
                {
                    if (checkIfSorted() == false) {
                        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Not all objects are sorted");
                    }
                    else if(checkIfSorted() == true) {
                        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("All objects are sorted.");
                        programMove.setGameOver();
                    }
                    break;
                }
        }
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("All objects are sorted.");
        //programMove.addLoop();

        //markSorted(0);
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().toggleGameOverControls();

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().stopTimer();
    }

    public void gameOverTimeout() {
        programMove.stopRunningProgram();
        programMove.destroyAllButton();
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Time is up");
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().toggleGameOverControls();
    }

    //
    public void markSorted(int cranePosition) {
        /*
        Transform[] children = carList[cranePosition].GetComponentsInChildren<Transform>();
        foreach (Transform child in children) {
            if(child.name == "Object Sprite") {
                child.GetComponent<SpriteRenderer>().material.color = Color.red;
            }
        }
        */
        //startNextLoop();
            //GetComponent<Renderer>().material.color = Color.red;
    }

    //after returning crane to original position
    //reassigns
    public void startNextLoop() {

        #region bubblesort

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            int highestNumber = 0;
            int tempLastHighestNumber = lastHighestNumber;

            for (int i = 0; i < lastCorrectCarNumberList.Count; i++) {
                int tempNumber = carList[i].GetComponent<CarMainScript>().carNumber;

                lastCorrectCarNumberList[i] = tempNumber;

                if (i < craneManagerScript.maxCranePosition + 1) {
                    if (tempNumber > highestNumber) {
                        if (tempNumber != lastHighestNumber) {
                            highestNumber = tempNumber;
                            tempLastHighestNumber = highestNumber;
                        }
                    }
                }
            }
            lastHighestNumber = tempLastHighestNumber;
        }
        #endregion

        #region selectionsort
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            int highestNumber = 0;
            int tempLastHighestNumber = lastHighestNumber;

            for (int i = 0; i < lastCorrectCarNumberList.Count; i++) {
                int tempNumber = carList[i].GetComponent<CarMainScript>().carNumber;

                lastCorrectCarNumberList[i] = tempNumber;

                if (i >= craneManagerScript.cranePosition) {
                    if (tempNumber > highestNumber) {
                        if (tempNumber != lastHighestNumber) {
                            highestNumber = tempNumber;
                            tempLastHighestNumber = highestNumber;
                        }
                    }
                }
            }
            lastHighestNumber = tempLastHighestNumber;
            markObject(-1);
        }
        #endregion

        #region insertionsort
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {

            for (int i = 0; i < lastCorrectCarNumberList.Count; i++) {
                int tempNumber = carList[i].GetComponent<CarMainScript>().carNumber;

                lastCorrectCarNumberList[i] = tempNumber;
            }
        }
        #endregion

        #region shellsort
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {

            for (int i = 0; i < lastCorrectCarNumberList.Count; i++) {
                int tempNumber = carList[i].GetComponent<CarMainScript>().carNumber;

                lastCorrectCarNumberList[i] = tempNumber;
            }
        }
        #endregion

        Debug.Log(lastHighestNumber);
    }

    //SELECTION SORT

    //mark the highest value in list of object
    public void markObject(int position) {

        #region selectionsort
        if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            //if (checkValue() == true) {

            int targetPosition = position;

            Debug.Log(targetPosition);

            if (craneManagerScript.markedPosition != -1) {
                var children = carList[craneManagerScript.markedPosition].GetComponentsInChildren<Transform>();
                foreach (var child in children) {
                    if (child.name == "Object Sprite") {
                        child.GetComponent<SpriteRenderer>().material.color = Color.white;
                    }
                }
            }
                
            if (targetPosition != -1) {
                var children = carList[targetPosition].GetComponentsInChildren<Transform>();
                foreach (var child in children) {
                    if (child.name == "Object Sprite") {
                        child.GetComponent<SpriteRenderer>().material.color = Color.red;
                    }
                }
                craneManagerScript.markedPosition = craneManagerScript.cranePosition2;
            }
            else {
                craneManagerScript.markedPosition = -1;
            }
            //}
            /*
            else {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("The selected value is lower than marked value.");
            }
            */
        }
        #endregion

        #region insertionsort
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {

            Debug.Log(craneManagerScript.targetPosition);

            var children = carList[craneManagerScript.targetPosition].GetComponentsInChildren<Transform>();
            foreach (var child in children) {
                if (child.name == "Object Sprite") {
                    child.GetComponent<SpriteRenderer>().material.color = Color.green;
                }
            }
        }
        #endregion
    }

    public void cleanObjects() {
        foreach(GameObject g in carList) {
            var children = g.GetComponentsInChildren<Transform>();
            foreach (var child in children) {
                if (child.name == "Object Sprite") {
                    child.GetComponent<SpriteRenderer>().material.color = Color.white;
                }
            }
        }

        #region insertionsort
        for(int i = 0;i < craneManagerScript.targetPosition;i++) {
            var children = carList[i].GetComponentsInChildren<Transform>();
            foreach (var child in children) {
                if (child.name == "Object Sprite") {
                    child.GetComponent<SpriteRenderer>().material.color = Color.red;
                }
            }
        }
        #endregion
    }

    public void markObjectQuick(GameObject g) {
        pivotPoint = g;
        var children = pivotPoint.GetComponentsInChildren<Transform>();
        foreach (var child in children) {
            if (child.name == "Object Sprite") {
                child.GetComponent<SpriteRenderer>().material.color = Color.green;
            }
        }
        chosenQuickObject = null;
    }

    public bool checkQuickSort() {

        bool isSorted = true;

        int i = 0;

        int pivotIdx = pivotPoint.GetComponent<CarMainScript>().triggerIdx;
        int pivotValue = pivotPoint.GetComponent<CarMainScript>().carNumber;

        for (i = 0;i < pivotIdx; i++) {
            
            if(carList[i].GetComponent<CarMainScript>().carNumber > pivotValue) {
                isSorted = false;
                Debug.Log(carList[i].GetComponent<CarMainScript>().carNumber);
            }    
        }

        Debug.Log("aw");

        for (i = carList.Count - 1; i > pivotIdx; i--) {
            if (carList[i].GetComponent<CarMainScript>().carNumber < pivotValue) {
                isSorted = false;
                Debug.Log(carList[i].GetComponent<CarMainScript>().carNumber);
            }
        }

        return isSorted;
        //Debug.Log(pivotPoint.GetComponent<CarMainScript>().triggerIdx);
    }

    public void putPivotQuick() {
        if (checkQuickSort() == true) {
            var children = pivotPoint.GetComponentsInChildren<Transform>();
            foreach (var child in children) {
                if (child.name == "Object Sprite") {
                    child.GetComponent<SpriteRenderer>().material.color = Color.red;
                }
            }
            pivotPoint.GetComponent<CarMainScript>().isPivot = true;
            triggerList[pivotPoint.GetComponent<CarMainScript>().triggerIdx].GetComponent<SwitchTriggerScript>().disableSelf();
            waitingForPivot = true;
            pivotPoint = null;

            bool isFinished = true;
            foreach(GameObject g in carList) {
                if(g.GetComponent<CarMainScript>().isPivot == false) {
                    isFinished = false;
                }
            }
            if(isFinished == true) {
                gameOver();
            }
        }
        else {
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Lower Values must be on the left and higher values must be on the right");
            Debug.Log("wroing");
        }
    }


}
