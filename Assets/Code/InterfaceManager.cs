using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour {

    public GameObject optionsPanel;
    public GameObject scorePanel;
    public GameObject tutorialPanel;

    public GameObject controllerGeneral;
    //public GameObject controllerSelection;
    public GameObject gameOverController;

    public Text infoText;
    public Text loopText;

    public Timer timer;

    public Score scoreManager;

    public Text rankText;
    public Text victoryText;
    public Text timeElapsedText;

    // Use this for initialization
    void Start () {
        scoreManager = this.GetComponent<Score>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PlayerPrefs.SetInt("returnFromGame", 1);
            SceneChooser.returnMainMenu();
        }
    }

    public void changeInfoText(string info) {
        infoText.text = info;
    }

    public void disableControls() {
        if(controllerGeneral != null)
        controllerGeneral.SetActive(false);
        //controllerSelection.SetActive(false);
    }

    public void toggleGameOverControls() {
        disableControls();
        gameOverController.SetActive(true);
    }

    public void toggleInfoPanel() {
        if (optionsPanel.activeSelf == true) {
            optionsPanel.SetActive(false);
        }
        else {
            optionsPanel.SetActive(true);
        }
    }

    public void retryGame() {
        SceneChooser.replayStage();
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene(KeyDictionary.SCENES.MAINMENU);
    }

    public void returnToArcade() {
        SceneManager.LoadScene(KeyDictionary.SCENES.ARCADE);
    }

    public void changeLoopTextNum(int num) {
        loopText.text = "Loop " + num;
    }

    public void showScoreInterface(bool win) {

        scorePanel.SetActive(true);

        if (win == true) {
            int topScorePos = scoreManager.saveTimeToLeaderboard(timer.getTime());
            victoryText.text = "YOU COMPLETED THE SORT";
            timeElapsedText.text = "Time : " + timer.getTime();

            if (topScorePos != -1) {
                rankText.text = "You ranked #" + (topScorePos + 1);
                string targetObject = "RankScore" + (topScorePos + 1);
                GameObject.Find(targetObject).GetComponent<Text>().fontStyle = FontStyle.Bold;
                Debug.Log(targetObject);
            }
        }

        scoreManager.showLeaderboard();
    }
    
    public void hideScoreInterface() {
        scorePanel.SetActive(false);
    }

    public void stopTimer() {
        timer.stopTimer();
    }

    public void startTimer() {
        timer.startTimer();
    }

    public void showTutorialPanel() {
        toggleInfoPanel();
        tutorialPanel.SetActive(true);
        tutorialPanel.GetComponent<InfoTutorial>().showInitialPage();
    }

    public void hideTutorialPanel() {
        toggleInfoPanel();
        tutorialPanel.SetActive(false);
    }

    public void buttonExitPress() {
        PlayerPrefs.SetInt("returnFromGame", 1);
        SceneChooser.returnMainMenu();
    }
}
