using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MergeSortGameManager : MonoBehaviour {

    //The Prefab for boxes
    public GameObject boxObject;

    public List<GameObject> mergeList;

    private int objAmount = 8;

    private float startingXPos = -8.0f;
    private float startingYPos = -9.0f;

    public List<List<GameObject>> listOfMergeList;

    

    // Use this for initialization
    void Start () {
        generateMerge();

        highlightClickable();

        tempList = new List<GameObject>();

        startNextLevel();
    }
	
	// Update is called once per frame
	void Update () {
	
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

    private int levelPos = 0;
    private int horizontalPos = 0;

    private int lastStep = 0;

    private int maxCheck;

    void startNextLevel() {
        levelPos++;
        maxCheck = objAmount / levelPos;
        lastStep = 0;
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

        float maxSize = Mathf.Pow(2, levelPos);

        int listPosition;

        if (lastStep == 0)
            listPosition = 0;
        else {
            listPosition = lastStep / (int)maxSize;
        }

        

        if (tempList.Count == 0) {

            leftMostXPos = 0.75f * (maxSize / 2);
            float leftX = listOfMergeList[listPosition][listOfMergeList[listPosition].Count - 1].transform.position.x;
            float rightX = listOfMergeList[listPosition + 1][0].transform.position.x;

            middleXPos = leftX + ((rightX - leftX) / 2f);
        }

        if (tempList.Count < maxSize) {
          
            float xPos = 0;
            float yPos = 0;

            /*
            if (listPosition % 2 == 0) {
                leftX = listOfMergeList[listPosition][listOfMergeList[listPosition].Count - 1].transform.position.x;
                rightX = listOfMergeList[listPosition + 1][0].transform.position.x;
            }
            else {
                leftX = listOfMergeList[listPosition - 1][listOfMergeList[listPosition].Count - 1].transform.position.x;
                rightX = listOfMergeList[listPosition][0].transform.position.x;
            }
            */

            Debug.Log(leftMostXPos);
            Debug.Log(middleXPos);

            xPos = ((middleXPos - leftMostXPos) + tempList.Count * 1.5f);

            yPos = startingYPos - (2.0f * levelPos);

            g.GetComponent<BoxClickMerge>().moveToPosition(xPos, yPos);

            g.GetComponent<BoxClickMerge>().clickable = false;
            g.GetComponent<BoxClickMerge>().modifyOutline(2);

            tempList.Add(g);

            lastStep++;
        }

        Debug.Log(tempList.Count);
        Debug.Log(maxSize);

        if(tempList.Count == maxSize) {

            Debug.Log("full");

            listOfMergeList[listPosition].Clear();
            
            foreach(GameObject node in tempList) {
                listOfMergeList[listPosition].Add(node);
            }

            highlightClickable();

            tempList.Clear();
        }
    }
}
