using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CraneManager : MonoBehaviour {

    public GameObject craneOne;
    public GameObject craneWireOne;
    public GameObject craneHookOne;

    public GameObject craneTwo;
    public GameObject craneWireTwo;
    public GameObject craneHookTwo;

    public GameObject gameManager;

    float originalWireWidthOne = 0.0f;
    float wireYPosOne = 0.0f;

    float originalWireWidthTwo = 0.0f;
    float wireYPosTwo = 0.0f;

    public float craneOriginalPos = -7f;

    //Position of crane 1
    public int cranePosition = 0;

    //Position of crane 2
    public int cranePosition2 = 1;

    //SELECTION SORT
    public int markedPosition = 0;

    //lower the more object is sorted
    public int maxCranePosition = 0;

    // Use this for initialization
    void Start () {

        returnCrane();

        originalWireWidthOne = (float)craneWireOne.GetComponent<SpriteRenderer>().bounds.size.y;
        wireYPosOne = craneWireOne.transform.position.y;

        originalWireWidthTwo = (float)craneWireTwo.GetComponent<SpriteRenderer>().bounds.size.y;
        wireYPosTwo = craneWireTwo.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        float tileWidthOne = wireYPosOne - (float)craneWireOne.GetComponent<SpriteRenderer>().bounds.size.y;
        float tileWidthTwo = wireYPosTwo - (float)craneWireTwo.GetComponent<SpriteRenderer>().bounds.size.y;

        craneHookOne.transform.position = new Vector3(craneHookOne.transform.position.x, tileWidthOne, craneHookOne.transform.position.z);
        craneHookTwo.transform.position = new Vector3(craneHookTwo.transform.position.x, tileWidthTwo, craneHookTwo.transform.position.z);
    }

    public void moveCrane() {
        #region bubblesort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            //make sure the value of the left side is lower than the right side
            if (gameManager.GetComponent<GameManager>().checkValue()) {

                cranePosition++;

                Debug.Log(maxCranePosition);

                if (cranePosition > maxCranePosition) {
                    returnCrane();
                    gameManager.GetComponent<GameManager>().markSorted(cranePosition);
                    cranePosition = 0;
                    maxCranePosition--;
                }
                else {
                    this.transform.Translate(3.0f, 0.0f, 0.0f);
                }

                if (maxCranePosition == -1) {
                    gameManager.GetComponent<GameManager>().gameOver();

                }
            }
            else {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Value on the left is higher than the right.");
            }
        }
        #endregion

        #region selectionsort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            if (gameManager.GetComponent<GameManager>().waitForSwitch == true) {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Do the switch between marked value and first crane value first!");
            }
            else {
                if (gameManager.GetComponent<GameManager>().checkValue() == false ) {    

                    if (cranePosition2 <= maxCranePosition) {
                        cranePosition2++;
                        craneTwo.transform.Translate(3.0f, 0.0f, 0.0f);
                        Debug.Log(cranePosition2);

                        if(cranePosition2 > maxCranePosition) {
                            if (markedPosition == cranePosition) {
                                returnCrane();
                            }
                            else gameManager.GetComponent<GameManager>().waitForSwitch = true;
                        }
                        /*
                        returnCrane();
                        //gameManager.GetComponent<GameManager>().markSorted(cranePosition);
                        cranePosition++;
                        cranePosition2 = cranePosition + 1;

                        craneOne.transform.Translate(3.0f, 0.0f, 0.0f);

                        markedPosition = cranePosition;
                        */
                    }
                }
                /*
                else if(cranePosition == maxCranePosition) {
                    if(gameManager.GetComponent<GameManager>().isSwitched == false) {
                        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Do the switch between marked value and first crane value first!");
                    }
                    else {
                        returnCrane();
                        //gameManager.GetComponent<GameManager>().markSorted(cranePosition);
                        cranePosition++;
                        cranePosition2 = cranePosition + 1;

                        craneOne.transform.Translate(3.0f, 0.0f, 0.0f);

                        markedPosition = cranePosition;
                    }
                }
                */
                else {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Value of selected object is higher than marked object");
                }
            }
        }
        
        #endregion
    }

    public void initBubblesort() {

    }
    /*
    public void initSelectionsort() {

        List<GameObject> carListRef = gameManager.GetComponent<GameManager>().carList;

        int highestValue = carListRef[0];
        for (int i = 1; i < carListRef.Count; i++) {
            
        }
    }
    */
    public void returnCrane() {
        #region bubblesort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            this.transform.position = new Vector3(craneOriginalPos, this.transform.position.y);
        }
        #endregion

        #region selectionsort
        if(gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {

            cranePosition++;
            cranePosition2 = cranePosition + 1;

            craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * 3.0f, this.transform.position.y);    

            craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * 3.0f, this.transform.position.y);

            markedPosition = cranePosition;

            if(cranePosition2 > maxCranePosition) {
                gameManager.GetComponent<GameManager>().gameOver();
            }
        }
        #endregion
    }
}
