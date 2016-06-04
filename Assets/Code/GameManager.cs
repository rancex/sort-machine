using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //Selection sort
    public bool waitForSwitch = false;


    public int sortType;

    public CraneManager craneManagerScript;

    //The Prefab For Car
    public GameObject carObject;
    public List<GameObject> carList = new List<GameObject>();

    public int startXpos = -7;
    public int startYpos = 0;

	// Use this for initialization
	void Start () {

        //sortType = KeyDictionary.SORTTYPE.BUBBLESORT;
        sortType = KeyDictionary.SORTTYPE.SELECTIONSORT;

        List<int> numberList = Shuffler.generateRandomList();

        for (int i = 0;i < 5; i++) {
            GameObject car = Instantiate(carObject, new Vector3(startXpos + i * 3, startYpos, 0), Quaternion.identity) as GameObject;
            car.GetComponent<CarMainScript>().insertNumber(numberList[i]);
            car.GetComponent<CarMainScript>().insertIndex(i);
            carList.Add(car);
        }

        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            craneManagerScript.maxCranePosition = carList.Count - 2;
        }
        else if(sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            craneManagerScript.maxCranePosition = carList.Count - 1;
        }

        if(sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {

        }
        else if(sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            initSelection();
        }
	}

    public void initSelection() {
        //highestValue = carList[0].GetComponent<CarMainScript>().carNumber;

        /*
        int newValue = 0;

        for (int i = 1; i < carList.Count; i++) {
            newValue = carList[i].GetComponent<CarMainScript>().carNumber;
            if (newValue > highestValue) {

            }
        }*/
    }

    public void highlightCar(int carNumber) {
        if(sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {

        }
    }

    public void switchObject() {

        #region bubblesort
        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            int position = craneManagerScript.cranePosition;

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
            carList[position].GetComponent<CarMainScript>().insertNumber(carList[position + 1].GetComponent<CarMainScript>().carNumber);

            carList[position + 1].GetComponent<CarMainScript>().insertNumber(tempValue);
        }
        #endregion

        #region selectionsort
        if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {

            if (waitForSwitch == true) {
                int position1 = craneManagerScript.cranePosition;
                int position2 = craneManagerScript.markedPosition;

                int tempValue = carList[position1].GetComponent<CarMainScript>().carNumber;
                float tempXPos = carList[position1].transform.position.x;

                carList[position1].GetComponent<CarMainScript>().insertNumber(carList[position2].GetComponent<CarMainScript>().carNumber);

                carList[position2].GetComponent<CarMainScript>().insertNumber(tempValue);

                craneManagerScript.returnCrane();
                waitForSwitch = false;
                cleanObjects();
            }
        }
        #endregion
    }

    public void checkIfSorted() {

        bool sorted = true;

        int front;

        int back = carList[0].GetComponent<CarMainScript>().carNumber;

        for (int i = 1; i < carList.Count; i++) {
            front = carList[i].GetComponent<CarMainScript>().carNumber;
            if (front < back) sorted = false;
            back = front;
        }
        if (sorted == true) Debug.Log("Sorted");
        else Debug.Log("Not Sorted");
    }

    //check if the value of the left side is higher than the right side
    public bool checkValue() {
        int position = craneManagerScript.cranePosition;
        int position2 = craneManagerScript.cranePosition2;

        int markedPos = craneManagerScript.markedPosition;

        #region bubblesort
        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            if (carList[position].GetComponent<CarMainScript>().carNumber > carList[position + 1].GetComponent<CarMainScript>().carNumber) {
                return false;
            }
            else return true;
        }
        #endregion

        #region selectionsort
        else if(sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            Debug.Log(carList[markedPos].GetComponent<CarMainScript>().carNumber);
            Debug.Log(carList[position2].GetComponent<CarMainScript>().carNumber);
            if (carList[markedPos].GetComponent<CarMainScript>().carNumber >= carList[position2].GetComponent<CarMainScript>().carNumber) {
                return false;
            }
            else return true;
        }
        #endregion

        return false;
    }

    public void gameOver() {
        //GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().disableControls();
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("All objects are sorted.");
        markSorted(0);
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().toggleGameOverControls();
    }

    public void markSorted(int cranePosition) {
        
        var children = carList[cranePosition].GetComponentsInChildren<Transform>();
        foreach (var child in children) {
            if(child.name == "Object Sprite") {
                child.GetComponent<SpriteRenderer>().material.color = Color.red;
            }
        }
            //GetComponent<Renderer>().material.color = Color.red;
    }

    //SELECTION SORT

    //mark the highest value in list of object
    public void markObject() {
        if (checkValue() == true) {
            int markedPosition = craneManagerScript.cranePosition2;

            Debug.Log(markedPosition);

            var children = carList[craneManagerScript.markedPosition].GetComponentsInChildren<Transform>();
            foreach (var child in children) {
                if (child.name == "Object Sprite") {
                    child.GetComponent<SpriteRenderer>().material.color = Color.white;
                }
            }

            children = carList[markedPosition].GetComponentsInChildren<Transform>();
            foreach (var child in children) {
                if (child.name == "Object Sprite") {
                    child.GetComponent<SpriteRenderer>().material.color = Color.red;
                }
            }
            craneManagerScript.markedPosition = markedPosition;
        }
        else {
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("The selected value is lower than marked value.");
        }
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
    }
}
