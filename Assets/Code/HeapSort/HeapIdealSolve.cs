using UnityEngine;
using System.Collections;

public class HeapIdealSolve : MonoBehaviour {

    public HeapSortGameManager gameManager;

    public bool gameOver = false;

    public Coroutine cor;

    public bool isWaitingForNextSolve = false;
    public bool isSolving = false;

	// Use this for initialization
	void Start () {
        gameManager = this.GetComponent<HeapSortGameManager>();
	}

    // Update is called once per frame
    void Update() {
        if (gameOver == true) {
            if (cor != null)
                StopCoroutine(cor);
        }
        else {
            if (isWaitingForNextSolve == true) {
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

            GameObject chosenHeapObject = gameManager.chosenHeapObject;

            Debug.Log(chosenHeapObject.GetComponent<DocumentScript>().docNumber);

            if (chosenHeapObject.GetComponent<DocumentScript>().leftChild == null &&
                chosenHeapObject.GetComponent<DocumentScript>().rightChild == null) {
                gameManager.startNextHeap();
            }

            else {
                int parentValue = chosenHeapObject.GetComponent<DocumentScript>().docNumber;

                int valueLeft = 0;
                int valueRight = 0;

                if (chosenHeapObject.GetComponent<DocumentScript>().leftChild == null) {
                    valueLeft = parentValue + 1;
                }
                else valueLeft = chosenHeapObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().docNumber;

                if (chosenHeapObject.GetComponent<DocumentScript>().rightChild == null) {
                    valueRight = parentValue + 2;
                }
                else valueRight = chosenHeapObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().docNumber;

                //find lowest number
                if (parentValue < valueLeft && parentValue < valueRight) {
                    gameManager.startNextHeap();
                }

                else if (valueLeft < parentValue && valueLeft < valueRight) {
                    chosenHeapObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().switchObjectWithChosen();
                }

                else if (valueRight < parentValue && valueRight < valueLeft) {
                    chosenHeapObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().switchObjectWithChosen();
                }
            }
        }
    }

}
