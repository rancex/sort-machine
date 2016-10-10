using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HeapSortGameManager : MonoBehaviour {

    public HeapIdealSolve heapIdealSolve;

    public float sortedInitialXPos = -7.5f;
    public float sortedInitialYPos = -4f;
    public float sortedInitialZPos = -2f;

    //The Prefab for Documents
    public GameObject docObject;

    public GameObject sortedArea;
    private Vector3 targetSortedSize = new Vector3(0.35f, 0.35f, 0.35f);

    public List<GameObject> heapList;
    public GameObject chosenHeapObject = null;
    public GameObject clickedHeapObject = null;

    //list to hold original position on every loop
    public List<int> originalHeapNumbers = new List<int>();

    public bool chosenSwitched = false;
    public bool clickedSwitched = false;

    public bool isSwitching = false;
    public bool isRestarting = false;
    public bool isHeapifying = false;
    public bool isMovingToSortedArea = false;

    private int objAmount = 7;

    // Use this for initialization
    void Start () {

        heapIdealSolve = this.GetComponent<HeapIdealSolve>();

        generateHeap();

        chosenHeapObject = heapList[0];

        highlightClickable(chosenHeapObject);
       
        printList();

        saveNumberOriginalPosition();

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Welcome to Heap Sort");
    }
	
    void gameOver() {
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("All objects are sorted");
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().toggleGameOverControls();

        GameObject.Find("Timer").GetComponent<Timer>().stopTimer();
    }

    public void startNextHeap() {
        //heapList[heapList.Count - 1]
        
        if(isSwitching == false && isMovingToSortedArea == false && isHeapifying == false) {

            if (heapList.Count > 1) {
                if (checkHeap() == true) {

                    isHeapifying = true;
                    
                    Vector3 topHeapPosition = new Vector3(heapList[0].transform.position.x,
                                                          heapList[0].transform.position.y,
                                                          heapList[0].transform.position.z);

                    Vector3 topHeapSize = new Vector3(heapList[0].transform.localScale.x,
                                                      heapList[0].transform.localScale.y,
                                                      heapList[0].transform.localScale.z);

                    dehighlightClickable(chosenHeapObject);

                    chosenHeapObject = heapList[heapList.Count - 1];

                    switchObject(heapList[0]);

                }
                else {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("One of more objects are wrong");
                }
            }
            else{
                isMovingToSortedArea = true;

                float xPos = sortedInitialXPos + (2.5f * (objAmount - heapList.Count));

                Vector3 sortedAreaPos = new Vector3(xPos,
                                                    sortedInitialYPos,
                                                    sortedInitialZPos);

                chosenHeapObject = heapList[0];

                dehighlightClickable(heapList[0]);

                heapList.RemoveAt(0);

                StartCoroutine(smoothMoveDocumentPosition(chosenHeapObject, sortedAreaPos, targetSortedSize, 1.0f));

                gameOver();
            }
        }
        
    }

    void saveNumberOriginalPosition() {
        originalHeapNumbers.Clear();
        for (int i = 0; i < heapList.Count; i++) {
            originalHeapNumbers.Add(heapList[i].GetComponent<DocumentScript>().docNumber);
        }
    }

    public void restartLoop() {
        if (isHeapifying == true) Debug.Log("wew");
        if (isSwitching == false && isMovingToSortedArea == false && isHeapifying == false) {
            for(int i = 0;i < heapList.Count; i++) {
                heapList[i].GetComponent<DocumentScript>().insertNumber(originalHeapNumbers[i]);
                dehighlightClickable(heapList[i]);
            }

            chosenHeapObject = heapList[0];

            highlightClickable(chosenHeapObject);

            
        }
    }

    void highlightClickable(GameObject targetObject) {
        targetObject.GetComponent<DocumentScript>().modifyOutline(2);
        targetObject.GetComponent<DocumentScript>().GetComponent<DocumentScript>().clickable = false;

        if (targetObject.GetComponent<DocumentScript>().leftChild != null) {
            targetObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().modifyOutline(1);
            targetObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().clickable = true;
        }

        if (targetObject.GetComponent<DocumentScript>().rightChild != null) {
            targetObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().modifyOutline(1);
            targetObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().clickable = true;
        }
    }

    void dehighlightClickable(GameObject targetObject) {

        targetObject.GetComponent<DocumentScript>().modifyOutline(0);

        if (targetObject.GetComponent<DocumentScript>().leftChild != null) {
            targetObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().modifyOutline(0);
            targetObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().clickable = false;
        }

        if (targetObject.GetComponent<DocumentScript>().rightChild != null) {
            targetObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().modifyOutline(0);
            targetObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().clickable = false;
        }
    }

    void highLightIncorrect(GameObject targetObject) {
        targetObject.GetComponent<DocumentScript>().modifyOutline(3);
        targetObject.GetComponent<DocumentScript>().GetComponent<DocumentScript>().clickable = false;

        if (targetObject.GetComponent<DocumentScript>().leftChild != null) {
            targetObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().modifyOutline(3);
            targetObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().clickable = false;
        }

        if (targetObject.GetComponent<DocumentScript>().rightChild != null) {
            targetObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().modifyOutline(3);
            targetObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().clickable = false;
        }
    }

    bool checkHeap() {

        bool heapIsCorrect = true;

        foreach (GameObject g in heapList) {
            dehighlightClickable(g);
        }

        foreach (GameObject g in heapList) {
            int parentNum = 0;

            parentNum = g.GetComponent<DocumentScript>().docNumber;
            if(g.GetComponent<DocumentScript>().leftChild != null) {
                if(parentNum > g.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().docNumber) {
                    highLightIncorrect(g);
                    heapIsCorrect = false;
                }
            }
            if (g.GetComponent<DocumentScript>().rightChild != null) {
                if (parentNum > g.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().docNumber) {
                    highLightIncorrect(g);
                    heapIsCorrect = false;
                }
            }
        }

        return heapIsCorrect;
    }

    public void printList() {

        string listNumbers = "";

        for(int i = 0;i < heapList.Count; i++) {
            listNumbers += heapList[i].GetComponent<DocumentScript>().docNumber;
            listNumbers += " ";
        }

        Debug.Log(listNumbers);
    }

    void generateHeap() {

        int treeIdx;

        GameObject lastParent = null;

        List<int> numberList = Shuffler.generateRandomList(objAmount);

        for (treeIdx = 0; treeIdx < objAmount; treeIdx++) {

            GameObject node = Instantiate(docObject, new Vector3(1, 0, 0), Quaternion.identity) as GameObject;

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
                else {
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
            
            /*
            GameObject trigger = Instantiate(heapSwitchTrigger, node.transform.position, Quaternion.identity) as GameObject;
            trigger.transform.localScale = new Vector2(node.transform.localScale.x * 4, node.transform.localScale.y * 3);
            heapTriggerList.Add(trigger);
            */
        }
    } 

    void afterSwitch(GameObject g) {

        if (g == chosenHeapObject) chosenSwitched = true;
        else if (g == clickedHeapObject) clickedSwitched = true;

        if (chosenSwitched == true && clickedSwitched == true) {

            chosenSwitched = false;
            clickedSwitched = false;

            GameObject temp = chosenHeapObject;

            int chosenIdx = chosenHeapObject.GetComponent<DocumentScript>().id;
            int targetIdx = clickedHeapObject.GetComponent<DocumentScript>().id;

            heapList[chosenIdx].GetComponent<DocumentScript>().id = targetIdx;
            heapList[targetIdx].GetComponent<DocumentScript>().id = chosenIdx;

            heapList[chosenIdx] = heapList[targetIdx];
            heapList[targetIdx] = temp;

            GameObject clickedLeftChild = clickedHeapObject.GetComponent<DocumentScript>().leftChild;
            GameObject clickedRightChild = clickedHeapObject.GetComponent<DocumentScript>().rightChild;

            GameObject chosenLeftChild = chosenHeapObject.GetComponent<DocumentScript>().leftChild;
            GameObject chosenRightChild = chosenHeapObject.GetComponent<DocumentScript>().rightChild;

            bool clickedIsChosenParent = false;

            if (isHeapifying == true) {
                if (chosenHeapObject.GetComponent<DocumentScript>().parent == clickedHeapObject) {

                    if (clickedHeapObject.GetComponent<DocumentScript>().leftChild == chosenHeapObject) {
                        chosenHeapObject.GetComponent<DocumentScript>().leftChild = null;
                        chosenHeapObject.GetComponent<DocumentScript>().rightChild = clickedRightChild;
                    }
                    if (clickedHeapObject.GetComponent<DocumentScript>().rightChild == chosenHeapObject) {
                        chosenHeapObject.GetComponent<DocumentScript>().leftChild = clickedLeftChild;
                        chosenHeapObject.GetComponent<DocumentScript>().rightChild = null;
                    }

                    chosenHeapObject.GetComponent<DocumentScript>().parent = null;

                    clickedIsChosenParent = true;
                }
            }

            //Clicked----------------------
            if (clickedHeapObject == chosenLeftChild) {
                clickedHeapObject.GetComponent<DocumentScript>().leftChild = chosenHeapObject;
                clickedHeapObject.GetComponent<DocumentScript>().rightChild = chosenRightChild;

                if (clickedHeapObject.GetComponent<DocumentScript>().rightChild != null)
                    clickedHeapObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().parent = clickedHeapObject;
            }
            else if(clickedHeapObject == chosenRightChild) {
                clickedHeapObject.GetComponent<DocumentScript>().leftChild = chosenLeftChild;
                clickedHeapObject.GetComponent<DocumentScript>().rightChild = chosenHeapObject;

                if (clickedHeapObject.GetComponent<DocumentScript>().leftChild != null)
                    clickedHeapObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().parent = clickedHeapObject;
            }

            if (chosenHeapObject.GetComponent<DocumentScript>().parent != null) {
                clickedHeapObject.GetComponent<DocumentScript>().parent = chosenHeapObject.GetComponent<DocumentScript>().parent;

                if(chosenHeapObject.GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().leftChild == chosenHeapObject) {
                    chosenHeapObject.GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().leftChild = clickedHeapObject;
                }
                if (chosenHeapObject.GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().rightChild == chosenHeapObject) {
                    chosenHeapObject.GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().rightChild = clickedHeapObject;
                }
            }
            else {
                clickedHeapObject.GetComponent<DocumentScript>().parent = null;
            }
            //---------------------

            //chosen---------------


            if (clickedIsChosenParent == false) {
                chosenHeapObject.GetComponent<DocumentScript>().leftChild = clickedLeftChild;
                chosenHeapObject.GetComponent<DocumentScript>().rightChild = clickedRightChild;

                chosenHeapObject.GetComponent<DocumentScript>().parent = clickedHeapObject;
            }

            if (chosenHeapObject.GetComponent<DocumentScript>().leftChild != null)
                chosenHeapObject.GetComponent<DocumentScript>().leftChild.GetComponent<DocumentScript>().parent = chosenHeapObject;
            if (chosenHeapObject.GetComponent<DocumentScript>().rightChild != null)
                chosenHeapObject.GetComponent<DocumentScript>().rightChild.GetComponent<DocumentScript>().parent = chosenHeapObject;     

            //---------------------
            //if (chosenObjectParent != null) clickedHeapObject.GetComponent<DocumentScript>().parent = chosenObjectParent;

            if (isRestarting == false && isHeapifying == false) {
                highlightClickable(chosenHeapObject);
            }

            printList();

            isSwitching = false;

            if(isHeapifying == true) {

                isMovingToSortedArea = true;

                float xPos = sortedInitialXPos + (2.5f * (objAmount - heapList.Count));

                Vector3 sortedAreaPos = new Vector3(xPos,
                                            sortedInitialYPos,
                                            sortedInitialZPos);

                StartCoroutine(smoothMoveDocumentPosition(heapList[heapList.Count - 1], sortedAreaPos, targetSortedSize, 1.0f));

                if(heapList[heapList.Count - 1].GetComponent<DocumentScript>().parent != null) {
                    if(heapList[heapList.Count - 1].GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().leftChild == heapList[heapList.Count - 1]){
                        heapList[heapList.Count - 1].GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().leftChild = null;
                    }
                    if (heapList[heapList.Count - 1].GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().rightChild == heapList[heapList.Count - 1]) {
                        heapList[heapList.Count - 1].GetComponent<DocumentScript>().parent.GetComponent<DocumentScript>().rightChild = null;
                    }
                }

                heapList.RemoveAt(heapList.Count - 1);

                saveNumberOriginalPosition();
            }
            else {
                if (heapIdealSolve.isSolving == true) {
                    heapIdealSolve.isWaitingForNextSolve = true;
                }
            }
        }
    }

    public void switchObject(GameObject clickedObject) {

        isSwitching = true;

        clickedHeapObject = clickedObject;

        Vector3 chosenHeapPosition = new Vector3(chosenHeapObject.transform.position.x,
                                                 chosenHeapObject.transform.position.y,
                                                 chosenHeapObject.transform.position.z);

        Vector3 chosenHeapSize = new Vector3(chosenHeapObject.transform.localScale.x,
                                             chosenHeapObject.transform.localScale.y,
                                             chosenHeapObject.transform.localScale.z);

        Vector3 clickedHeapPosition = new Vector3(clickedObject.transform.position.x,
                                                  clickedObject.transform.position.y,
                                                  clickedObject.transform.position.z);

        Vector3 clickedHeapSize = new Vector3(clickedObject.transform.localScale.x,
                                             clickedObject.transform.localScale.y,
                                             clickedObject.transform.localScale.z);

        Debug.Log(chosenHeapPosition);
        Debug.Log(clickedHeapPosition);

        dehighlightClickable(chosenHeapObject);

        StartCoroutine(smoothMoveDocumentPosition(chosenHeapObject, clickedHeapPosition, clickedHeapSize, 1.0f));
        StartCoroutine(smoothMoveDocumentPosition(clickedObject, chosenHeapPosition, chosenHeapSize, 1.0f));

        /*
        
        */
        //highlightClickable(chosenHeapObject.GetComponent<DocumentScript>().id);


    }

    IEnumerator smoothMoveDocumentPosition(GameObject targetObject, Vector3 targetPos, Vector3 targetSize, float time) {

        float elapsedTime = 0;

        while (elapsedTime < time) {
            targetObject.transform.position = new Vector3(Mathf.Lerp(targetObject.transform.position.x, targetPos.x, (elapsedTime / time)),
                                                          Mathf.Lerp(targetObject.transform.position.y, targetPos.y, (elapsedTime / time)),
                                                          targetPos.z
                                                          );

            targetObject.transform.localScale = new Vector3(Mathf.Lerp(targetObject.transform.localScale.x, targetSize.x, (elapsedTime / time)),
                                                            Mathf.Lerp(targetObject.transform.localScale.y, targetSize.y, (elapsedTime / time)),
                                                            1.0f
                                                            );

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (isSwitching == true) {
            afterSwitch(targetObject);
        }
        if(isMovingToSortedArea == true) {
            isMovingToSortedArea = false;
            isHeapifying = false;

            if (heapList.Count > 0) {
                highlightClickable(chosenHeapObject);

                if(heapIdealSolve.isSolving == true) {
                    heapIdealSolve.isWaitingForNextSolve = true;
                }
            }
        }
    }
}
