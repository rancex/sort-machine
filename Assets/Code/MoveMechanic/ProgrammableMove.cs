using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgrammableMove : MonoBehaviour {

    public List<int> movesList = new List<int>();

    public GameObject addButtonPrefab;

    public GameObject gameManager;
    public GameObject craneManager;

    public GameObject buttonObject;
    public List<GameObject> buttonList;

    public GameObject tickSymbol;
    public List<GameObject> tickSymbolList = new List<GameObject>();

    public int numberOfMistake;

    public int numberOfSteps;

    //number of loops done
    public int loopDone = 0;

    private int numberOfLoop;

    private bool coroutineRunning;

    private Coroutine programCor;

    //is loop finished?
    public bool loopFinished;

    private float moveButtonXPos = 4.5f;
    private float moveButtonYPos = 3.6f;

    // Use this for initialization
    void Start () {

        numberOfSteps = 0;

        numberOfLoop = gameManager.GetComponent<GameManager>().objectAmount;
        for(int i = 0;i < numberOfLoop; i++) {
            GameObject tick = Instantiate(tickSymbol, new Vector3((4.5f + 0.5f * i), 4.5f, 0.0f), Quaternion.identity) as GameObject;
            tickSymbolList.Add(tick);
        }

        loopFinished = false;
	}

    public void setAvailableMoves() {

        List<int> availableMoveList = new List<int>();

        switch (gameManager.GetComponent<GameManager>().sortType) {
            case KeyDictionary.SORTTYPE.BUBBLESORT:
                {
                    availableMoveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                    availableMoveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                    break;
                }
            case KeyDictionary.SORTTYPE.INSERTIONSORT:
                {
                    availableMoveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                    availableMoveList.Add(KeyDictionary.MOVETYPES.MOVECRANEREDLEFT);
                    availableMoveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                    break;
                }
            case KeyDictionary.SORTTYPE.SELECTIONSORT:
                {
                    availableMoveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                    availableMoveList.Add(KeyDictionary.MOVETYPES.MOVECRANERED);
                    availableMoveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                    
                    break;
                }
        }

        for (int i = 0; i < availableMoveList.Count; i++) {

            float posX = 490f + ((i % 3 * 150f));

            float posY = 230f - ((i / 3 * 170));

            GameObject addButtonObj = Instantiate(addButtonPrefab, new Vector3(posX, posY, 0.0f), Quaternion.identity) as GameObject;

            addButtonObj.GetComponent<AddButtonScript>().updateMovetype(availableMoveList[i]);

            //Debug.Log(move);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void runProgram() {

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("");

        if (coroutineRunning == true) {
            StopCoroutine(programCor);
            coroutineRunning = false;
        }

        if (loopFinished == false) {
            restartProgram();
            if (movesList.Count > 0) {
                Debug.Log(movesList.Count);
                programCor = StartCoroutine(runProgramWithSpeed(1.0f));
            }
            else GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("No Command Inputted");
        }
        else {
            addLoop();
            /*
            if (craneManager.GetComponent<CraneManager>().maxCranePosition == -1) {
                if (gameManager.GetComponent<GameManager>().checkIfEndHighest() == true) {
                    gameManager.GetComponent<GameManager>().gameOver();
                }
            }
            */
        }
        //}
    }

    public void addMove(int moveType,int movePos) {
        if (movePos == -1) {

            if (coroutineRunning == false) {
                float posX = moveButtonXPos + ((movesList.Count % 4 * 1));

                float posY = moveButtonYPos - ((movesList.Count / 4 * 1));

                movesList.Add(moveType);

                GameObject buttonObj = Instantiate(buttonObject, new Vector3(posX, posY, -1f), Quaternion.identity) as GameObject;
                buttonObj.GetComponent<ProgramButtonScript>().setMoveType(moveType);
                buttonObj.GetComponent<ProgramButtonScript>().setMoveIndex(movesList.Count - 1);

                buttonList.Add(buttonObj);
            }
        }
        else {
            movesList.Insert(movePos, moveType);
        }
    }

    public void removeMove(int movePos) {
        movesList.RemoveAt(movePos);

        Destroy(buttonList[movePos]);
        buttonList.RemoveAt(movePos);

        refreshButtonPosition();
    }

    public void addMoveGreenButton() {
        addMove(KeyDictionary.MOVETYPES.MOVECRANEGREEN,-1);
    }

    public void addMoveRedButton() {
        addMove(KeyDictionary.MOVETYPES.MOVECRANERED, -1);
    }

    public void addSwitchButton() {
        addMove(KeyDictionary.MOVETYPES.SWITCHOBJECT, -1);
    }

    public void addMarkButton() {
        addMove(KeyDictionary.MOVETYPES.MARK, -1);
    }

    public void refreshButtonPosition() {
        int stepNumber = 0;
        for (stepNumber = 0; stepNumber < buttonList.Count; stepNumber++) {

            float posX = moveButtonXPos + ((stepNumber % 4 * 1));
            float posY = moveButtonYPos - ((stepNumber / 4 * 1));

            buttonList[stepNumber].GetComponent<Transform>().position = new Vector3(posX, posY, -1);
            buttonList[stepNumber].GetComponent<ProgramButtonScript>().setMoveIndex(stepNumber);
        }
    }

    public void restartProgram() {
        gameManager.GetComponent<GameManager>().restartCarPositions();

        if (coroutineRunning == true) {
            StopCoroutine(programCor);
            coroutineRunning = false;
        }

        foreach (GameObject button in buttonList) {
            button.GetComponent<ProgramButtonScript>().dehighlightSprite();
        }

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("");
    }

    public void destroyAllButton() {
        movesList.Clear();
        foreach(GameObject button in buttonList) {
            Destroy(button.gameObject);
        }
        buttonList.Clear();
    }

    IEnumerator runProgramWithSpeed(float speed) {

        coroutineRunning = true;

        //check if program should continue looping
        bool runProgram = true;

        int stepNumber = 0;

        yield return new WaitForSeconds(1.0f);

        for (stepNumber = 0; stepNumber < movesList.Count; stepNumber++) {

            //to mark if the program works correctly

            

            int move = movesList[stepNumber];

            switch (move) {
                case KeyDictionary.MOVETYPES.MOVECRANEGREEN:
                    {
                        bool canMove = craneManager.GetComponent<CraneManager>().moveCrane();
                        if (canMove == false) {
                            if(gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT)
                                runProgram = false;
                            else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) { }
                        }

                        else {
                            buttonList[stepNumber].GetComponent<ProgramButtonScript>().highlightSprite(canMove);

                            /*
                            if (stepNumber > 1) {
                                buttonList[stepNumber].GetComponent<ProgramButtonScript>().dehighlightSprite();
                            }
                            */
                        }
                        break;
                    }
                case KeyDictionary.MOVETYPES.MOVECRANERED:
                    {
                        Debug.Log(runProgram);
                        bool canMove = craneManager.GetComponent<CraneManager>().moveCraneRed();
                        if (canMove == false) {
                            runProgram = false;
                        }

                        else {
                            buttonList[stepNumber].GetComponent<ProgramButtonScript>().highlightSprite(canMove);

                            if (stepNumber > 1) {
                                buttonList[stepNumber].GetComponent<ProgramButtonScript>().dehighlightSprite();
                            }
                        }
                        break;
                    }
                case KeyDictionary.MOVETYPES.SWITCHOBJECT:
                    {
                        buttonList[stepNumber].GetComponent<ProgramButtonScript>().highlightSprite(true);
                        gameManager.GetComponent<GameManager>().switchObject();
                        break;
                    }
                case KeyDictionary.MOVETYPES.MARK:
                    {
                        buttonList[stepNumber].GetComponent<ProgramButtonScript>().highlightSprite(true);
                        gameManager.GetComponent<GameManager>().markObject(craneManager.GetComponent<CraneManager>().cranePosition2);
                        break;
                    }
            }

            //stops iterating if the player makes a wrong move
            if (runProgram == false) break;

            numberOfSteps++;

            yield return new WaitForSeconds(1.0f);
        }

        coroutineRunning = false;
    }

    public void prepareNextLoop() {
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Completed Highest Number. /n Press Run To Continue");
        loopFinished = true;
        if (coroutineRunning == true) {
            StopCoroutine(programCor);
            coroutineRunning = false;
        }
    }

    public void addLoop() {

        //gameManager.GetComponent<GameManager>().markSorted(craneManager.GetComponent<CraneManager>().cranePosition);
        gameManager.GetComponent<GameManager>().startNextLoop();
        craneManager.GetComponent<CraneManager>().returnCrane(false);

        if(gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT)
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Completed Highest Number. /n Press Run To Continue");

        loopDone++;
        loopFinished = false;
        manageLoopNumber();
        if (coroutineRunning == true) {
            StopCoroutine(programCor);
            coroutineRunning = false;
        }
        destroyAllButton();
    }

    //check how many loops are done and how many are left
    public void manageLoopNumber() {
        for(int i = 0;i < loopDone; i++) {
            tickSymbolList[i].GetComponent<TickSymbolScript>().switchColor();
        }
    }
}
