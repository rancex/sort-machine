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
    public GameObject programMove;

    float originalWireWidthOne = 0.0f;
    float wireYPosOne = 0.0f;

    float originalWireWidthTwo = 0.0f;
    float wireYPosTwo = 0.0f;

    public float craneOriginalPos = -6.5f;

    //Position of crane 1
    //The green one in bubble sort
    public int cranePosition = 0;

    //Position of crane 2
    //The green one in insertion and selection
    public int cranePosition2 = 1;

    //SELECTION SORT ONLY-------------------------
    //The last marked position in the object list, 
    //that serves as the last highest value
    public int markedPosition = 0;
    //--------------------------------------------

    //INSERTION SORT ONLY-------------------------
    //INSERTION SORT ONLY NEEDS 1 CRANE

    //The compare target
    //Initially after every insertion this is the
    //nearest value to the "border" between
    //inserted and not, (the not one)
    public int targetPosition = 0;
    //--------------------------------------------

    //lower the more object is sorted
    public int maxCranePosition = 0;

    // Use this for initialization
    void Start () {

        //returnCrane();

        /*
        originalWireWidthOne = (float)craneWireOne.GetComponent<SpriteRenderer>().bounds.size.y;
        wireYPosOne = craneWireOne.transform.position.y;

        originalWireWidthTwo = (float)craneWireTwo.GetComponent<SpriteRenderer>().bounds.size.y;
        wireYPosTwo = craneWireTwo.transform.position.y;
        */
        //gameManager.GetComponent<GameManager>().markObject();
    }
	
	// Update is called once per frame
	void Update () {
        /*
        float tileWidthOne = wireYPosOne - (float)craneWireOne.GetComponent<SpriteRenderer>().bounds.size.y;
        float tileWidthTwo = wireYPosTwo - (float)craneWireTwo.GetComponent<SpriteRenderer>().bounds.size.y;

        craneHookOne.transform.position = new Vector3(craneHookOne.transform.position.x, tileWidthOne, craneHookOne.transform.position.z);
        craneHookTwo.transform.position = new Vector3(craneHookTwo.transform.position.x, tileWidthTwo, craneHookTwo.transform.position.z);
        */
    }

    public void highlightCrane(int craneNum,int craneColor) {
        if(craneNum == 1) {
            craneOne.GetComponent<ClawScript>().highlightClaw(craneColor);
        }
        else if(craneNum == 2) {
            craneTwo.GetComponent<ClawScript>().highlightClaw(craneColor);
        }
    }

    public bool moveCrane() {

        bool result = true;

        #region bubblesort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            //make sure the value of the left side is lower than the right side
            //if (gameManager.GetComponent<GameManager>().checkValue()) {

            cranePosition++;

            Debug.Log(maxCranePosition);

            if (cranePosition > maxCranePosition) {
                if (gameManager.GetComponent<GameManager>().checkIfEndHighest() == true) {
                    result = false;
                    programMove.GetComponent<ProgrammableMove>().prepareNextLoop();
                    /*
                    gameManager.GetComponent<GameManager>().markSorted(cranePosition);
                    returnCrane(false);
                    */
                }
                else {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Objects are not sorted");
                    return false;
                }
            }
            else {
                craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * gameManager.GetComponent<GameManager>().objectGap, craneOne.transform.position.y);
                craneTwo.transform.position = new Vector3(craneOne.transform.position.x + gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);
            }
            if(maxCranePosition == -1) {
                gameManager.GetComponent<GameManager>().gameOver();
            }
            //}
            /*
            else {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Value on the left is higher than the right.");
                return false;
            }
            */
        }
        #endregion

        #region selectionsort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {

            if (cranePosition2 < maxCranePosition) {
                cranePosition2++;

                craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);

                //craneTwo.transform.Translate(gameManager.GetComponent<GameManager>().objectGap, 0.0f, 0.0f);
                Debug.Log(cranePosition2);

                if (cranePosition2 > maxCranePosition) {
                    if (markedPosition == cranePosition) {
                        returnCrane(false);
                    }
                    else gameManager.GetComponent<GameManager>().waitForSwitch = true;
                }
            }
            else {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Green crane can't move anymore.");
            }
            //}

            /*
            else if(cranePosition == maxCranePosition) {
                /*
                if (gameManager.GetComponent<GameManager>().isSwitched == false) {
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
            }*/
        }

        #endregion

        #region insertionsort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {           
            if (gameManager.GetComponent<GameManager>().checkIfEndHighest()) {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Highest Value on sorted list is bigger than value on crane!");
                /*
                if (gameManager.GetComponent<GameManager>().biggerInsert == true) {
                        
                    gameManager.GetComponent<GameManager>().waitForInsert = true;
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Insert the object to the position first!");
                }
                */
            }
            else {

                targetPosition++;
                Debug.Log(targetPosition);
                cranePosition = targetPosition;
                cranePosition2 = targetPosition + 1;

                if (cranePosition2 > maxCranePosition) {
                    gameManager.GetComponent<GameManager>().gameOver();
                }
                else {
                    craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);

                    programMove.GetComponent<ProgrammableMove>().prepareNextLoop();
                }
            }
            /*
            if (gameManager.GetComponent<GameManager>().waitForInsert == false) {
                cranePosition--;
                craneOne.transform.Translate(-(gameManager.GetComponent<GameManager>().objectGap), 0.0f, 0.0f);
                if (cranePosition < 0 && gameManager.GetComponent<GameManager>().biggerInsert == false) {
                    returnCrane(false);
                }
                else if(cranePosition < 0 && gameManager.GetComponent<GameManager>().biggerInsert == true) {
                    gameManager.GetComponent<GameManager>().waitForInsert = true;
                    //cranePosition = 0;
                    //craneOne.transform.Translate(3.0f, 0.0f, 0.0f);
                }
                /*
                if (cranePosition == 0) {
                    returnCrane(false);
                }
                
            }
            */
        }
        #endregion

        #region shellsort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            if (gameManager.GetComponent<GameManager>().checkValue()) {
                cranePosition++;
                cranePosition2++;

                if(cranePosition2 > maxCranePosition) {
                    returnCrane(false);
                }
                else {
                    craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * gameManager.GetComponent<GameManager>().objectGap, craneOne.transform.position.y);
                    craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);
                }
            }
            else {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Value on the left is higher than the right.");
            }
        }
        #endregion

        return result;
    }

    //Insertion and Selection only
    public bool moveCraneRed() {

        bool result = true;

        #region selectionsort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            if (cranePosition2 < maxCranePosition) {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Finish The Loop First!");
            }
            else {
                if (gameManager.GetComponent<GameManager>().checkIfEndHighest() == true) {
                    cranePosition++;
                    craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);
                    programMove.GetComponent<ProgrammableMove>().prepareNextLoop();

                    if (cranePosition2 > maxCranePosition) {
                        gameManager.GetComponent<GameManager>().gameOver();
                    }

                    //programMove.GetComponent<ProgrammableMove>().addLoop();
                }
                else {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("The number at red crane position is not the highest");
                    return false;
                }
            }
        }
        
        #endregion

        #region insertionsort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            if(cranePosition > 0) {
                cranePosition--;
                craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);
            }
            else {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Red Crane Cannot Move Anymore");
            }
        }
        #endregion
        return result;
    }

    public void returnCrane(bool restart) {
        #region bubblesort
        if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {   

            cranePosition = 0;
            if (restart == false) {
                maxCranePosition--;
            }

            craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * gameManager.GetComponent<GameManager>().objectGap, craneOne.transform.position.y);
            craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);

        }
        #endregion

        #region selectionsort
        else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {

            cranePosition2 = cranePosition + 1;

            if (cranePosition2 > maxCranePosition) {
                gameManager.GetComponent<GameManager>().gameOver();
            }

            else {
                craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * gameManager.GetComponent<GameManager>().objectGap, craneOne.transform.position.y);
            }
        }
        #endregion

        #region insertionsort
        else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {

            if (restart == true) {
                cranePosition = targetPosition;
                cranePosition2 = targetPosition + 1;
            }

            if (cranePosition2 > maxCranePosition) {
                gameManager.GetComponent<GameManager>().gameOver();
            }

            else {
                craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * gameManager.GetComponent<GameManager>().objectGap, craneOne.transform.position.y, craneOne.transform.position.z);
                craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y, craneTwo.transform.position.z);
            }
            //gameManager.GetComponent<GameManager>().cleanObjects();

            /*
            else {
                
                gameManager.GetComponent<GameManager>().markObject(-1);
            }
            */
        }
        #endregion

        #region shellsort
        else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SHELLSORT) {

            int gapLength = gameManager.GetComponent<GameManager>().gapLength;

            if (gapLength > 1) {

                cranePosition = 0;
                cranePosition2 = gapLength;

                craneOne.transform.position = new Vector3(craneOriginalPos + cranePosition * gameManager.GetComponent<GameManager>().objectGap, craneOne.transform.position.y);
                craneTwo.transform.position = new Vector3(craneOriginalPos + cranePosition2 * gameManager.GetComponent<GameManager>().objectGap, craneTwo.transform.position.y);

                gapLength = gapLength / 2;
                gameManager.GetComponent<GameManager>().gapLength = gapLength;
            }
            else {
                Debug.Log("start insertion sort");
                cranePosition = 0;
                
                Destroy(craneTwo);

                gameManager.GetComponent<GameManager>().sortType = KeyDictionary.SORTTYPE.INSERTIONSORT;

                returnCrane(false);

                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Start Insertion Sort.");
            }
        }
        #endregion
    }
}
