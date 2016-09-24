using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdealSolutionQuicksort : MonoBehaviour {

    public GameManager gameManager;
    public List<int> pivotedPosition = new List<int>(); 

	// Use this for initialization
	void Start () {
        
	}

    int pivotPosition = 0;
    bool pivotFound = false;
    GameObject pivotObject;
    int lastStep = 0;

    public List<GameObject> originalRefList = new List<GameObject>();
    public List<GameObject> leftPartition = new List<GameObject>();
    public List<GameObject> rightPartition = new List<GameObject>();

    bool isSolving = false;
    bool isComplete = false;

    bool isInit = false;

    public void initList() {
        int numberOfObject = gameManager.carList.Count;

        for (int i = 0; i < numberOfObject; i++) {
            pivotedPosition.Add(i);
        }
        isInit = true;

        originalRefList = new List<GameObject>(gameManager.carList);

        gameManager.isAutoSolvingQuick = true;
    }

    public void solveProblem() {

        if (isComplete == false) {

            if (isInit == false) initList();

            int randPosition = Random.Range(0, pivotedPosition.Count);

            pivotPosition = pivotedPosition[randPosition];

            pivotedPosition.RemoveAt(randPosition);

            pivotObject = originalRefList[pivotPosition];

            pivotFound = false;

            Debug.Log("K" + " " + pivotObject.GetComponent<CarMainScript>().carNumber + " " + pivotedPosition.Count);

            lastStep = 0;

            
            gameManager.markObjectQuick(pivotObject);

            bool pivotBorderFound = false;

            leftPartition.Clear();
            rightPartition.Clear();

            for (int i = pivotObject.GetComponent<CarMainScript>().carIdx; i >= 0; i--) {
                if (i != pivotObject.GetComponent<CarMainScript>().carIdx) {
                    if (gameManager.carList[i].GetComponent<CarMainScript>().isPivot == true) pivotBorderFound = true;

                    if (pivotBorderFound == false) {
                        leftPartition.Add(gameManager.carList[i]);
                    }
                }
            }
            leftPartition.Reverse();

            pivotBorderFound = false;
            for (int j = pivotObject.GetComponent<CarMainScript>().carIdx; j < gameManager.carList.Count; j++) {
                if (j != pivotObject.GetComponent<CarMainScript>().carIdx) {
                    if (gameManager.carList[j].GetComponent<CarMainScript>().isPivot == true) pivotBorderFound = true;

                    if (pivotBorderFound == false) {
                        rightPartition.Add(gameManager.carList[j]);
                    }
                }
            }

            partitionDirection = 1;

            Debug.Log("left" + leftPartition.Count);
            Debug.Log("right" + rightPartition.Count);

            runNextSolveProblem();

            //StartCoroutine(waitForNextProblem());
        }
    }

    //1 = left
    //2 = right
    public int partitionDirection = 0;

    public void runNextSolveProblem() {

        bool changePartition = false;

        bool moveBox = false;

        //Debug.Log(partitionDirection);

        if (partitionDirection == 1) {
            if (lastStep >= leftPartition.Count) {
                partitionDirection = 2;
                lastStep = 0;
                pivotFound = false;
                Debug.Log("wwww");
            }
        }
        if(partitionDirection == 2) {
            if (lastStep >= rightPartition.Count) {
                partitionDirection = 0;
                lastStep = 0;
                pivotFound = false;
                Debug.Log("wkwkw");
            }
        }
        if(partitionDirection == 0) {

            changePartition = true;

            Debug.Log("jm");

            lastStep = 0;
            pivotFound = false;

            Debug.Log("mark");
            gameManager.putPivotQuick();

            if (pivotedPosition.Count > 0) {
                StartCoroutine(waitForSolveProblem());
            }
            else {
                Debug.Log("complete");

                isComplete = true;
            }
        }

        if (changePartition == false) {

            /*
            if(partitionDirection == 1) {
                int carPivotIdx = pivotObject.GetComponent<CarMainScript>().carIdx;
                int carTargetIdx = gameManager.carList[leftPartition[lastStep]].GetComponent<CarMainScript>().carIdx;

                if (carTargetIdx > carPivotIdx) pivotFound = true;
            }
            else if(partitionDirection == 2) {
                int carPivotIdx = pivotObject.GetComponent<CarMainScript>().carIdx;
                int carTargetIdx = gameManager.carList[rightPartition[lastStep]].GetComponent<CarMainScript>().carIdx;

                //if (carTargetIdx > carPivotIdx) pivotFound = true;
            }
            */

            //if (pivotFound == false) {

            int carPivotNumber = pivotObject.GetComponent<CarMainScript>().carNumber;

            if (partitionDirection == 1) {
                int carTargetNumber = leftPartition[lastStep].GetComponent<CarMainScript>().carNumber;

               

                if (carTargetNumber > carPivotNumber) {

                    gameManager.chosenQuickObject = leftPartition[lastStep];

                    leftPartition[lastStep].GetComponent<CarMainScript>().autoMoveToTriggerNum(pivotObject.GetComponent<CarMainScript>().carIdx);
                    moveBox = true;
                }
            }

            else if (partitionDirection == 2) {
                int carTargetNumber = rightPartition[lastStep].GetComponent<CarMainScript>().carNumber;

                if (carTargetNumber < carPivotNumber) {

                    gameManager.chosenQuickObject = rightPartition[lastStep];


                    Debug.Log("gdi" + lastStep);
                    rightPartition[lastStep].GetComponent<CarMainScript>().autoMoveToTriggerNum(pivotObject.GetComponent<CarMainScript>().carIdx);
                    moveBox = true;
                }
            }
            //}

            lastStep++;
            if(moveBox == false) {
                runNextSolveProblem();
                //StartCoroutine(waitForNextProblem());
            }
        }
    }

    IEnumerator waitForSolveProblem() {
        yield return new WaitForSeconds(1.0f);
        solveProblem();
    }

    IEnumerator waitForNextProblem() {
        yield return new WaitForSeconds(1.0f);
        runNextSolveProblem();
    }
}
