using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MergeIdealSolver : MonoBehaviour {

    public MergeSortGameManager gameManager;

    public Coroutine cor;

    int maxLevelPos;

    public bool gameOver = false;
    public bool isWaitingForNextSolve = false;
    public bool isSolving = false;

	// Use this for initialization
	void Start () {
        gameManager = this.GetComponent<MergeSortGameManager>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(gameOver == true) {
            if(cor != null)
            StopCoroutine(cor);
        }
        else {
            if(isWaitingForNextSolve == true) {
                isWaitingForNextSolve = false;
                StartCoroutine(waitForNextSolve());
            }
        }
	}

    IEnumerator waitForNextSolve() {
        yield return new WaitForSeconds(2.0f);
        idealSolveMerge();
    }

    public void startIdealSolver() {
        if (isSolving == false) {
            isSolving = true;
            isWaitingForNextSolve = true;

            GameObject.Find("Timer").GetComponent<Timer>().stopTimer();

            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Auto-Solving...");
        }
    }

    void idealSolveMerge() {

        if (gameOver == false) {

            int lastStep = gameManager.lastStep;
            int levelPos = gameManager.levelPos;

            List<List<GameObject>> tempListOfMergeList = gameManager.tempListOfMergeList;

            int targetList = 0;

            if (lastStep == 0) targetList = 0;
            else {
                targetList = lastStep / levelPos;
            }

            if (targetList % 2 == 0) {

                int valueLeft;
                int valueRight;

                if (tempListOfMergeList[targetList].Count <= 0) {
                    valueLeft = 999999;
                }
                else valueLeft = tempListOfMergeList[targetList][0].GetComponent<CarMainScript>().carNumber;

                if (tempListOfMergeList[targetList + 1].Count <= 0) {
                    valueRight = 999999;
                }
                else valueRight = tempListOfMergeList[targetList + 1][0].GetComponent<CarMainScript>().carNumber;

                if (valueLeft < valueRight) {
                    tempListOfMergeList[targetList][0].GetComponent<BoxClickMerge>().addSelfToNextList();
                }
                else {
                    tempListOfMergeList[targetList + 1][0].GetComponent<BoxClickMerge>().addSelfToNextList();
                }
            }
            else if (targetList % 2 == 1) {

                int valueLeft;
                int valueRight;

                if (tempListOfMergeList[targetList - 1].Count <= 0) {
                    valueLeft = 999999;
                }
                else valueLeft = tempListOfMergeList[targetList - 1][0].GetComponent<CarMainScript>().carNumber;

                if (tempListOfMergeList[targetList].Count <= 0) {
                    valueRight = 999999;
                }
                else valueRight = tempListOfMergeList[targetList][0].GetComponent<CarMainScript>().carNumber;

                if (valueLeft < valueRight) {
                    tempListOfMergeList[targetList - 1][0].GetComponent<BoxClickMerge>().addSelfToNextList();
                }
                else {
                    tempListOfMergeList[targetList][0].GetComponent<BoxClickMerge>().addSelfToNextList();
                }
            }

            isWaitingForNextSolve = true;
        }
    }
}
