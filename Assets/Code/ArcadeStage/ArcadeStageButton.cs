using UnityEngine;
using System.Collections;

public class ArcadeStageButton : MonoBehaviour {

    public GameObject arcadeStageManager;

	// Use this for initialization
	void Start () {
        arcadeStageManager = GameObject.Find("ArcadeStageManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onBtnRollClick() {
        arcadeStageManager.GetComponent<ArcadeStage>().randomizeStage();
    }
}
