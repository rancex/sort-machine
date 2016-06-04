using UnityEngine;
using System.Collections;

public class CarMainScript : MonoBehaviour {

    public TextMesh txtNumber;
    public GameObject carObject;

    public int carNumber;
    int carIdx;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void insertNumber(int number) {
        carNumber = number;
        txtNumber.text = carNumber.ToString();
    }

    public void insertIndex(int idx) {
        carIdx = idx;
    }
    public void onObjectClicked() {
        Debug.Log(carNumber);
    }
}
