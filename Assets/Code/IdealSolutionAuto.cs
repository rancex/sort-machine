using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdealSolutionAuto : MonoBehaviour {

    public List<List<int>> moveListList= new List<List<int>>();
    public List<int> moveList = new List<int>();

    public List<int> numberList;

    public int stepsNeeded;

    public int sorttype;

	// Use this for initialization
	void Start () {
        //sorttype = KeyDictionary.SORTTYPE.BUBBLESORT;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void findIdealSolution() {

        int sorttype = GameObject.Find("GameManager").GetComponent<GameManager>().sortType;

        populateNumberList();

        #region bubblesort
        if (sorttype == KeyDictionary.SORTTYPE.BUBBLESORT) {

            Debug.Log("wew");

            stepsNeeded = 0;

            int finishedLoop = 0;

            Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);

            for (int i = 0;i < numberList.Count; i++) {
                int border = numberList.Count - finishedLoop - 1;
                for (int j = 0;j < border; j++) {
                    if (numberList[j] > numberList[j + 1]) {
                        moveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                        stepsNeeded++;
                        moveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                        stepsNeeded++;

                        int tempNumber = numberList[j + 1];
                        numberList[j + 1] = numberList[j];
                        numberList[j] = tempNumber; 
                    }
                    else {
                        moveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                        stepsNeeded++;
                    }

                    Debug.Log(j);

                    //Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
                    //Debug.Log("Steps Done" + stepsNeeded);
                }
                finishedLoop++;

                moveListList.Add(new List<int>(moveList));
                moveList.Clear();
            }

            Debug.Log("Numbers Sorted :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
            Debug.Log("Steps Done" + stepsNeeded);
        }
        #endregion

        #region selectionsort
        if(sorttype == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            Debug.Log("wew");

            stepsNeeded = 0;

            int finishedLoop = 0;

            Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);

            for (int i = 0; i < numberList.Count; i++) {

                int highestNumber = numberList[i];
                int markedNumber = -1;

                for (int j = i + 1; j < numberList.Count; j++) {

                    if (numberList[j] > highestNumber) {
                        markedNumber = j;
                        highestNumber = numberList[j];
                        moveList.Add(KeyDictionary.MOVETYPES.MARK);
                        stepsNeeded++;

                        if (j < numberList.Count - 1) {
                            moveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                            stepsNeeded++;
                        }
                    }
                    else {
                        if (j < numberList.Count - 1) {
                            moveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                            stepsNeeded++;
                        }
                    }

                    //Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
                    //Debug.Log("Steps Done" + stepsNeeded);
                }

                if (markedNumber != -1) {
                    int tempNumber = numberList[i];
                    numberList[i] = numberList[markedNumber];
                    numberList[markedNumber] = tempNumber;
                    moveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                }

                if (finishedLoop == numberList.Count - 1) {

                }
                else moveList.Add(KeyDictionary.MOVETYPES.MOVECRANERED);

                finishedLoop++;
                moveListList.Add(new List<int>(moveList));
                moveList.Clear();
            }

            Debug.Log("Numbers Sorted :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
            Debug.Log("Steps Done" + stepsNeeded);
        }
        #endregion

        #region insertionsort
        if(sorttype == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            Debug.Log("wew");

            stepsNeeded = 0;

            int finishedLoop = 0;
            

            Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);

            for (int i = 1; i < numberList.Count; i++) {

                bool needInsertion = false;
                int moveNeeded = 0;

                //int border = numberList.Count - finishedLoop - 1;
                for (int j = i; j >= 0 ; j--) {

                    if (numberList[i] < numberList[j]) {

                        needInsertion = true;

                        moveNeeded++;

                        if (j != i - 1) {
                            moveList.Add(KeyDictionary.MOVETYPES.MOVECRANERED);
                            stepsNeeded++;
                        }

                        /*
                        int tempNumber = numberList[j + 1];
                        numberList[j + 1] = numberList[j];
                        numberList[j] = tempNumber;
                        */
                    }

                    //Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
                    //Debug.Log("Steps Done" + stepsNeeded);
                }
                finishedLoop++;

                if(needInsertion == true) {
                    moveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                    stepsNeeded++;
                    
                    int tempValue = numberList[i];

                    for (int k = i; k > i - moveNeeded; k--) {
                        numberList[i] = numberList[i - 1];
                    }

                    numberList[i - 1] = tempValue;
                }

                moveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                stepsNeeded++;
                moveListList.Add(new List<int>(moveList));
                moveList.Clear();
            }

            Debug.Log("Numbers Sorted :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
            Debug.Log("Steps Done" + stepsNeeded);
        }
        #endregion

        #region shellsort
        if(sorttype == KeyDictionary.SORTTYPE.SHELLSORT) {

            stepsNeeded = 0;

            int finishedLoop = 0;

            Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);

            int gapLength = numberList.Count - 1;

            for (int i = 1; gapLength > 1; i++) {
          
                for (int j = 0; j < numberList.Count - gapLength; j++) {
                    if (numberList[j] < numberList[j + gapLength]) {
                        moveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                        stepsNeeded++;
                        moveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                        stepsNeeded++;

                        int tempNumber = numberList[j + gapLength];
                        numberList[j + gapLength] = numberList[j];
                        numberList[j] = tempNumber;
                    }
                    else {
                        moveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                        stepsNeeded++;
                    }

                    //Debug.Log("Numbers :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
                    //Debug.Log("Steps Done" + stepsNeeded);
                }
                finishedLoop++;

                moveListList.Add(new List<int>(moveList));
                moveList.Clear();

                gapLength = numberList.Count / (i * 2);
            }

            Debug.Log("Numbers Sorted :" + numberList[0] + " " + numberList[1] + " " + numberList[2] + " " + numberList[3]);
            Debug.Log("Steps Done" + stepsNeeded);
        }
        #endregion
    }

    public void populateNumberList() {
        numberList = new List<int>();

        List<GameObject> carListClone = GameObject.Find("GameManager").GetComponent<GameManager>().carList;

        foreach(GameObject g in carListClone) {
            numberList.Add(g.GetComponent<CarMainScript>().carNumber);
        }
    }
}
