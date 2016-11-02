using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgrammableMove : MonoBehaviour {

    private bool gameOver = false;

    public GameObject programButtonPanel;
    public GameObject addProgramPanel;

    public List<int> movesList = new List<int>();

    public GameObject addButtonPrefab;

    public GameObject gameManager;
    public GameObject craneManager;

    public GameObject buttonObject;
    public List<GameObject> buttonList;

    public List<GameObject> programButtonList;

    public GameObject tickSymbol;
    public List<GameObject> tickSymbolList = new List<GameObject>();

    public int numberOfMistake;

    public int numberOfSteps;

    //number of loops done
    public int loopDone = 0;

    private int numberOfLoop;

    //private bool coroutineRunning;

    private Coroutine programCor;

    public bool isRunningIdealSolution = false;

    //is loop finished?
    public bool loopFinished;

    private float moveButtonXPos = 4.5f;
    private float moveButtonYPos = 3.6f;

    public GameObject craneOne = null;
    public GameObject craneTwo = null;

    private float moveExecuteXPos = -181f;
    private float moveExecuteYPos = 202f;

    private float moveExecuteGap = 103f;


    // Use this for initialization
    void Start () {

        numberOfSteps = 0;

        numberOfLoop = gameManager.GetComponent<GameManager>().objectAmount - 1;

        if (GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT ||
            GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.INSERTIONSORT ||
            GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT ||
            GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            for (int i = 0; i < numberOfLoop; i++) {
                GameObject tick = Instantiate(tickSymbol, new Vector3((4.6f + 0.6f * i), 4.6f, 0.0f), Quaternion.identity) as GameObject;
                tickSymbolList.Add(tick);
            }
        }

        loopFinished = false;
	}

    public void setGameOver() {
        gameOver = true;

        if (GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT ||
            GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            addLoop();
        }

        animationIsRunning = false;
        isDoingProgram = false;
    }

    public bool animationIsRunning = false;
    public bool isDoingProgram = false;

    void Update() {
        if (gameOver == false) {
            if (isDoingProgram == true) {
                if (animationIsRunning) {
                }
                else {
                    animationIsRunning = true;
                    runNextCommand();
                }
            }
        }
    }

    bool craneOneFinished;
    bool craneTwoFinished;

    public void afterMovement(GameObject craneObj) {

        if (craneOne == null) craneOne = GameObject.Find("CraneObject").GetComponent<CraneManager>().craneOne;
        if (craneTwo == null) craneTwo = GameObject.Find("CraneObject").GetComponent<CraneManager>().craneTwo;

        if (craneOne == craneObj) {
            craneOneFinished = true;
        }
        else if (craneTwo == craneObj) {
            craneTwoFinished = true;
        }

        if (craneOneFinished == true && craneTwoFinished == true) {
            craneOneFinished = false;
            craneTwoFinished = false;
            animationIsRunning = false;

            if(GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {

                int cranePosition2 = GameObject.Find("CraneObject").GetComponent<CraneManager>().cranePosition2;
                float craneXPosition = GameObject.Find("GameManager").GetComponent<GameManager>().carList[cranePosition2].transform.position.x;

                craneTwo.transform.position = new Vector3(craneXPosition, craneTwo.transform.position.y, craneTwo.transform.position.z);
            }
        }
        
    }

    public void afterSingleMovement() {
        animationIsRunning = false;
    }

    public void checkAnimation() {
        animationIsRunning = false;
    }

    public void setAvailableMoves() {

        List<int> availableMoveList = new List<int>();

        programButtonList = new List<GameObject>();

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
                    availableMoveList.Add(KeyDictionary.MOVETYPES.MARK);
                    break;
                }
            case KeyDictionary.SORTTYPE.SHELLSORT:
                {
                    availableMoveList.Add(KeyDictionary.MOVETYPES.MOVECRANEGREEN);
                    availableMoveList.Add(KeyDictionary.MOVETYPES.SWITCHOBJECT);
                    break;
                }
        }

        for (int i = 0; i < availableMoveList.Count; i++) {

            float posX = 490f + ((i % 2 * 190f));

            float posY = 250f - ((i / 2 * 190));

            GameObject addButtonObj = Instantiate(addButtonPrefab, new Vector3(posX, posY, 0.0f), Quaternion.identity) as GameObject;

            addButtonObj.GetComponent<AddButtonScript>().updateMovetype(availableMoveList[i]);

            programButtonList.Add(addButtonObj);

            //Debug.Log(move);
        }
    }

    public void runProgram() {

        if (gameOver == false) {

            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("");

            //coroutineRunning = true;

            isDoingProgram = true;
            animationIsRunning = true;
            stopRunningProgram();

           

            if (loopFinished == false) {

                lastStepNumber = -1;
                restartProgram();
                if (movesList.Count > 0) {
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().stopTimer();
                    runNextCommand();
                }
                else {
                    isDoingProgram = false;
                    animationIsRunning = false;
                    GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("No Command Inputted");
                }
            }
            else {
                isDoingProgram = false;
                animationIsRunning = false;

                addLoop();

                if (isRunningIdealSolution) {
                    runIdealSolution();
                }

                /*
                if (craneManager.GetComponent<CraneManager>().maxCranePosition == -1) {
                    if (gameManager.GetComponent<GameManager>().checkIfEndHighest() == true) {
                        gameManager.GetComponent<GameManager>().gameOver();
                    }
                }
                */
            }
        }
        //}
    }

    public void addMove(int moveType,int movePos) {
        if (movePos == -1) {

            if (isDoingProgram == false) {

                float posX = moveExecuteXPos + ((movesList.Count % 4 * moveExecuteGap));

                float posY = moveExecuteYPos - ((movesList.Count / 4 * moveExecuteGap));

                movesList.Add(moveType);

                GameObject buttonObj = Instantiate(buttonObject, new Vector3(posX, posY, 0f), Quaternion.identity) as GameObject;
                buttonObj.GetComponent<ProgramButtonScript>().setMoveType(moveType);
                buttonObj.GetComponent<ProgramButtonScript>().setMoveIndex(movesList.Count - 1);

                buttonObj.transform.SetParent(programButtonPanel.transform,false);

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

    public void addMoveRedReverseButton() {
        addMove(KeyDictionary.MOVETYPES.MOVECRANEREDLEFT, -1);
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

            float posX = moveExecuteXPos + ((stepNumber % 4 * moveExecuteGap));

            float posY = moveExecuteYPos - ((stepNumber / 4 * moveExecuteGap));
            

            buttonList[stepNumber].GetComponent<Transform>().localPosition = new Vector3(posX, posY, 0f);
            buttonList[stepNumber].GetComponent<ProgramButtonScript>().setMoveIndex(stepNumber);
        }
    }

    public void restartProgram() {

        loopFinished = false;

        gameManager.GetComponent<GameManager>().restartCarPositions();

        stopRunningProgram();

        foreach (GameObject button in buttonList) {
            button.GetComponent<ProgramButtonScript>().dehighlightSprite();
        }

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("");
    }

    public void destroyAllButton() {
        if (isDoingProgram == false) {
            movesList.Clear();
            foreach (GameObject button in buttonList) {
                Destroy(button.gameObject);
            }
            buttonList.Clear();
        }
    }

    public void destroyAllProgramButtons() {
        if(isDoingProgram == false) {
            foreach(GameObject button in programButtonList) {
                Destroy(button.gameObject);
            }
            programButtonList.Clear();
        }
    }

    int lastStepNumber = -1;

    public bool runNextCommand() {

        bool runProgram = true;

        lastStepNumber++;

        if (lastStepNumber >= movesList.Count) {
            isDoingProgram = false;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().startTimer();
        }

        else {
            //to mark if the program works correctly

            int move = movesList[lastStepNumber];

            switch (move) {
                case KeyDictionary.MOVETYPES.MOVECRANEGREEN:
                    {
                        bool canMove = craneManager.GetComponent<CraneManager>().moveCrane();
                        if (canMove == false) {
                            if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT)
                                runProgram = false;
                            else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
                                runProgram = false;
                            }
                            else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
                                runProgram = false;
                            }
                        }

                        else {
                            buttonList[lastStepNumber].GetComponent<ProgramButtonScript>().highlightSprite(canMove);
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
                            buttonList[lastStepNumber].GetComponent<ProgramButtonScript>().highlightSprite(canMove);

                            if (lastStepNumber > 1) {
                                buttonList[lastStepNumber].GetComponent<ProgramButtonScript>().dehighlightSprite();
                            }
                        }
                        break;
                    }
                case KeyDictionary.MOVETYPES.SWITCHOBJECT:
                    {
                        buttonList[lastStepNumber].GetComponent<ProgramButtonScript>().highlightSprite(true);
                        bool canSwitch = gameManager.GetComponent<GameManager>().switchObject();

                        if(canSwitch == true) {

                        }
                        else {
                            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Mark A Box First");
                            runProgram = false;
                        }
                        break;
                    }
                case KeyDictionary.MOVETYPES.MARK:
                    {
                        buttonList[lastStepNumber].GetComponent<ProgramButtonScript>().highlightSprite(true);
                        gameManager.GetComponent<GameManager>().markObject(craneManager.GetComponent<CraneManager>().cranePosition2);
                        runProgram = false;
                        break;
                    }
            }

            numberOfSteps++;
        }
        if(runProgram == false && gameOver == false) {
            runNextCommand();
        }
        return runProgram;
    }

    IEnumerator runProgramWithSpeed(float speed) {

        isDoingProgram = true;

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
                        animationIsRunning = true;
                        bool canMove = craneManager.GetComponent<CraneManager>().moveCrane();
                        if (canMove == false) {
                            if(gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT)
                                runProgram = false;
                            else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
                                runProgram = false;
                            }
                            else if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
                                runProgram = false;
                            }
                        }

                        else {
                            buttonList[stepNumber].GetComponent<ProgramButtonScript>().highlightSprite(canMove);
                            if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
                                //isDoingProgram = false;
                            }
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

        isDoingProgram = false;
    }

    public void prepareNextLoop() {
        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Completed Highest Number. /n Press Run To Continue");
        loopFinished = true;
        stopRunningProgram();

        if (isRunningIdealSolution == true && gameOver == false) {
            StartCoroutine(waitBeforeContinuing(1.0f));
        }
    }

    public void addLoop() {

        int sorttype = gameManager.GetComponent<GameManager>().sortType;

        int objectPos = 0;

        if (sorttype == KeyDictionary.SORTTYPE.BUBBLESORT) {
            objectPos = gameManager.GetComponent<GameManager>().carList.Count - loopDone - 1;
        }
        if (sorttype == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            objectPos = loopDone;
        }
        if(sorttype == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            objectPos = loopDone;
        }

        gameManager.GetComponent<GameManager>().carList[objectPos].GetComponent<CarMainScript>().markSortedLight();

        //gameManager.GetComponent<GameManager>().markSorted(craneManager.GetComponent<CraneManager>().cranePosition);
        if (gameOver == false) {
            gameManager.GetComponent<GameManager>().startNextLoop();
            craneManager.GetComponent<CraneManager>().returnCrane(false);

            if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.BUBBLESORT)
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Completed Highest Number. /n Press Run To Continue");

            stopRunningProgram();
            
        }
        loopDone++;
        loopFinished = false;
        manageLoopNumber();
        destroyAllButton();

        
    }

    //check how many loops are done and how many are left
    public void manageLoopNumber() {
        for(int i = 0;i < loopDone; i++) {
            tickSymbolList[i].GetComponent<TickSymbolScript>().switchColor();
        }

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeLoopTextNum(loopDone + 1);
    }

    public void stopRunningProgram() {
        /*
        if (coroutineRunning == true) {
            StopCoroutine(programCor);
            coroutineRunning = false;
        }
        */
    }


    public void runIdealSolution() {

        GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().disableControls();

        if (movesList.Count > 0) {
            destroyAllButton();
        }

        isRunningIdealSolution = true;

        List<List<int>> moveListList = GameObject.Find("IdealSolver").GetComponent<IdealSolutionAuto>().moveListList;

        if (loopDone < moveListList.Count) {
            foreach (int move in moveListList[loopDone]) {
                addMove(move, -1);
            }

            runProgram();
        }
        else {
            isRunningIdealSolution = false;
            GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Ideal Solution Simulation Finished");

            if (gameManager.GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
                moveListList.Clear();
                loopDone = 0;
                manageLoopNumber();
            }
        }
    }

    IEnumerator waitBeforeContinuing(float time) {
        yield return new WaitForSeconds(time);
        runProgram();
    }
}
