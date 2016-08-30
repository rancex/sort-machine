using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class InterfaceManager : MonoBehaviour {

    public GameObject optionsPanel;
    public GameObject controllerGeneral;
    public GameObject controllerSelection;
    public GameObject gameOverController;
    public Text infoText;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeInfoText(string info) {
        infoText.text = info;
    }

    public void disableControls() {
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
        SceneManager.LoadScene(KeyDictionary.SCENES.BUBBLESORT);
    }

    public void returnToMainMenu() {
        SceneManager.LoadScene(KeyDictionary.SCENES.MAINMENU);
    }

    public void returnToArcade() {
        SceneManager.LoadScene(KeyDictionary.SCENES.ARCADE);
    }
}
