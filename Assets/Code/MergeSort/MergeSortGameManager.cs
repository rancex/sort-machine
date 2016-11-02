using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MergeSortGameManager : MonoBehaviour {

    //The Prefab for boxes
    public GameObject boxObject;

    public List<GameObject> mergeList;

    private int objAmount = 4;

    private float startingXPos = -6.0f;
    private float startingYPos = -9.0f;

    public List<List<GameObject>> listOfMergeList;

    public List<List<GameObject>> tempListOfMergeList;

    public GameObject lastMovedObject;

    // Use this for initialization
    void Start () {
        generateMerge();

        highlightClickable();

        tempList = new List<GameObject>();

        startNextLevel();

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome To Merge Sort");
    }

    // 0 = nothing
    // 1 = isWaitingForMovement
    // 2 = isWaitingForNextLevel
    // 3 = startNextLevel
    private int steps = 0;

	// Update is called once per frame
	void Update () {
	    if(steps == 3) {
            startNextLevel();
        }
	}

    void gameOver() {
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().toggleGameOverControls();
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Merge Sort Completed");
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().showScoreInterface(true);

        this.GetComponent<MergeIdealSolver>().gameOver = true;

        GameObject.Find("Timer").GetComponent<Timer>().stopTimer();
    }

    public void finishedMovingObject(GameObject g) {

        if (steps == 1) {
            steps = 0;
        }
        else if (steps == 2) {
            if (g == lastMovedObject) {
                steps = 3;
            }
        }

    }

    void generateMerge() {

        listOfMergeList = new List<List<GameObject>>();

        List<int> numberList = Shuffler.generateRandomList(objAmount);

        for(int i = 0;i < objAmount; i++) {
            GameObject node = Instantiate(boxObject, new Vector3(startingXPos + (3.0f * i), startingYPos, 0), Quaternion.identity) as GameObject;

            node.GetComponent<CarMainScript>().insertNumber(numberList[i]);
            node.GetComponent<CarMainScript>().insertIndex(i);

            List<GameObject> newList = new List<GameObject>();
            newList.Add(node);
            listOfMergeList.Add(newList);
        }

       
    }

    public int levelPos = 0;
    private int horizontalPos = 0;

    public int lastStep = 0;

    private int maxCheck;

    void startNextLevel() {

        steps = 0;

        float maxSize = Mathf.Pow(2, levelPos);

        if (maxSize >= objAmount) {
            gameOver();
        }
        else {
            
            levelPos++;
            maxCheck = objAmount / levelPos;
            lastStep = 0;

            tempListOfMergeList = new List<List<GameObject>>(listOfMergeList);

            highlightClickable();

            foreach(List<GameObject> list in listOfMergeList) {
                foreach(GameObject g in list) {
                    g.GetComponent<BoxClickMerge>().saveOriginalPosition();
                }
            }
        }
    }

    void highlightClickable() {

        int targetList = 0;

        if (lastStep < objAmount) {

            if (lastStep == 0) targetList = 0;
            else {
                targetList = lastStep / levelPos;
            }

            foreach (List<GameObject> go in listOfMergeList) {
                foreach (GameObject g in go) {
                    g.GetComponent<BoxClickMerge>().modifyOutline(0);
                    g.GetComponent<BoxClickMerge>().clickable = false;
                }
            }
            
            listOfMergeList[targetList][0].GetComponent<BoxClickMerge>().modifyOutline(1);
            listOfMergeList[targetList][0].GetComponent<BoxClickMerge>().clickable = true;

            listOfMergeList[targetList + 1][0].GetComponent<BoxClickMerge>().modifyOutline(1);
            listOfMergeList[targetList + 1][0].GetComponent<BoxClickMerge>().clickable = true;

            /*
            foreach (GameObject g in listOfMergeList[targetList]) {
                g.GetComponent<BoxClickMerge>().modifyOutline(1);
            }

            foreach (GameObject g in listOfMergeList[targetList + 1]) {
                g.GetComponent<BoxClickMerge>().modifyOutline(1);
            }
            */
        }
    }

    private List<GameObject> tempList;

    private float leftMostXPos;
    private float middleXPos;


    public void addToNextList(GameObject g) {

        if (steps == 0) {

            float maxSize = Mathf.Pow(2, levelPos);

            int listPosition;
            int ownListIndex;

            if (lastStep == 0) listPosition = 0;
            else listPosition = lastStep / (int)maxSize;                

            if (g.GetComponent<CarMainScript>().carIdx == 0) ownListIndex = 0;
            else ownListIndex = g.GetComponent<CarMainScript>().carIdx / levelPos;
            
            if (tempList.Count == 0) {

                float leftX;
                float rightX;

                leftMostXPos = 0.75f * (maxSize / 2);

                if (ownListIndex % 2 == 0) {
                    leftX = listOfMergeList[ownListIndex][listOfMergeList[ownListIndex].Count - 1].transform.position.x;
                    rightX = listOfMergeList[ownListIndex + 1][0].transform.position.x;
                }
                else {
                    leftX = listOfMergeList[ownListIndex - 1][listOfMergeList[ownListIndex].Count - 1].transform.position.x;
                    rightX = listOfMergeList[ownListIndex][0].transform.position.x;
                }

                middleXPos = leftX + ((rightX - leftX) / 2f);
            }

            if (tempList.Count < maxSize) {

                float xPos = 0;
                float yPos = 0;

                xPos = ((middleXPos - leftMostXPos) + tempList.Count * 1.5f);
                yPos = startingYPos - (2.7f * levelPos);

                g.GetComponent<BoxClickMerge>().moveToPosition(xPos, yPos);
                g.GetComponent<BoxClickMerge>().clickable = false;
                g.GetComponent<BoxClickMerge>().modifyOutline(2);

                tempListOfMergeList[ownListIndex].RemoveAt(0);

                if (tempListOfMergeList[ownListIndex].Count > 0) {
                    tempListOfMergeList[ownListIndex][0].GetComponent<BoxClickMerge>().modifyOutline(1);
                    tempListOfMergeList[ownListIndex][0].GetComponent<BoxClickMerge>().clickable = true;
                }

                steps = 1;

                tempList.Add(g);
                lastStep++;
            }

            if (tempList.Count == maxSize) {

                if (checkSorted() == true) {
                    listOfMergeList[listPosition].Clear();

                    foreach (GameObject node in tempList) {
                        listOfMergeList[listPosition].Add(node);
                    }

                    tempList.Clear();

                    if (lastStep >= objAmount) {
                        steps = 2;
                        lastMovedObject = g;
                    }
                    else {
                        highlightClickable();
                    }

                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Correct!");
                }

                else {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Objects Are Not Sorted");

                }
            }
        }
    }

    public bool checkSorted() {
        int tempValue = -1;

        bool sorted = true;

        tempValue = tempList[0].GetComponent<CarMainScript>().carNumber;

        foreach (GameObject g in tempList) {
            if (sorted == true) {
                int targetNumber = g.GetComponent<CarMainScript>().carNumber;

                if (targetNumber >= tempValue) tempValue = targetNumber;
                else {
                    sorted = false;
                }
            }
        }

        return sorted;
    }
    
    public void resetPosition() {
        if (this.GetComponent<MergeIdealSolver>().isSolving == false) {
            if (steps == 0) {

                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("");

                for (int i = tempList.Count - 1; i >= 0; i--) {
                    int ownListIndex;

                    if (tempList[i].GetComponent<CarMainScript>().carIdx == 0)
                        ownListIndex = 0;
                    else {
                        ownListIndex = tempList[i].GetComponent<CarMainScript>().carIdx / levelPos;
                    }

                    tempList[i].GetComponent<BoxClickMerge>().returnToOriginalPos();

                    tempListOfMergeList[ownListIndex].Insert(0, tempList[i]);
                }

                lastStep = lastStep - tempList.Count;

                highlightClickable();

                tempList.Clear();
            }
        }
    }
}
