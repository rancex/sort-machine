using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArcadeStage : MonoBehaviour {

    public GameObject blackBar;
    public GameObject stagePrefab;

    public GameObject targetCanvas;
    public GameObject btnRollPrefab;

    public GameObject btnRollObject;
    public GameObject btnStart;

    int lastStage = 0;

    public GameObject chosenObject;

    private int resultStage;

	// Use this for initialization
	void Start () {
        lastStage = PlayerPrefs.GetInt("lastStage",0);

        int stageNum = PlayerPrefs.GetInt("stageNum", 3);

        for (int i = 0; i < stageNum; i++) {
            GameObject stageObject = Instantiate(stagePrefab, new Vector2((-7.5f + i * 2.5f), 0), Quaternion.identity) as GameObject;

            

            stageObject.GetComponent<ArcadeStageObject>().changeNumber(i);

            if (i < stageNum - 1) {
                Instantiate(blackBar, new Vector3((-6.7f + i * 2.5f), 0,2), Quaternion.identity);
            }

            if (i < lastStage) {
                stageObject.GetComponent<ArcadeStageObject>().changeSprite(2);
                string stageStringName = "Stage" + i.ToString();

                int stage = PlayerPrefs.GetInt(stageStringName, 0);
                stageObject.GetComponent<ArcadeStageObject>().changeName(stage);

                
            }
            if(i == lastStage) {
                stageObject.GetComponent<ArcadeStageObject>().changeSprite(3);

                chosenObject = stageObject;

                GameObject newButton = Instantiate(btnRollPrefab) as GameObject;

                btnRollObject = newButton;

                Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();

                //first you need the RectTransform component of your canvas
                RectTransform CanvasRect = targetCanvas.GetComponent<RectTransform>();

                //then you calculate the position of the UI element
                //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

                Vector2 ViewportPosition = camera.WorldToViewportPoint(new Vector3(stageObject.transform.position.x,
                                                                                   stageObject.transform.position.y - 1.5f,
                                                                                   0.0f));
                Vector2 WorldObject_ScreenPosition = new Vector2(
                ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

                //now you can set the position of the ui element
                newButton.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;

                //GameObject newButton = Instantiate(btnRoll,p,Quaternion.identity) as GameObject;
                newButton.transform.SetParent(targetCanvas.transform, false);
            }
            
            
        }

        

        /*
        for (int i = 0; i < lastStage; i++) {
            string lastStageStringName = "Stage" + i.ToString();

            int lastStageType = PlayerPrefs.GetInt(lastStageStringName, 0);

        }
        */
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void randomizeStage() {
        resultStage = Random.Range(1, 8);

        chosenObject.GetComponent<ArcadeStageObject>().changeName(resultStage);
        btnRollObject.SetActive(false);
        btnStart.SetActive(true);
    }

    public void startLevel() {

        lastStage = PlayerPrefs.GetInt("lastStage", 0);
        PlayerPrefs.SetInt("lastStage", lastStage+1);

        string stageStringName = "Stage" + lastStage.ToString();
        PlayerPrefs.SetInt(stageStringName, resultStage);

        switch (resultStage) {
            case KeyDictionary.SORTTYPE.BUBBLESORT:
                {
                    SceneChooser.startBubbleSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.SELECTIONSORT:
                {
                    SceneChooser.startSelectionSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.INSERTIONSORT:
                {
                    SceneChooser.startInsertionSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.SHELLSORT:
                {
                    SceneChooser.startShellSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.HEAPSORT:
                {
                    SceneChooser.startHeapSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.MERGESORT:
                {
                    SceneChooser.startMergeSort();
                    break;
                }
            case KeyDictionary.SORTTYPE.QUICKSORT:
                {
                    SceneChooser.startQuickSort();
                    break;
                }
        }
    }
}
