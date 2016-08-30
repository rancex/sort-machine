using UnityEngine;
using System.Collections;

public class SwitchTriggerScript : MonoBehaviour {

    public int id = 0;

    public bool isPivot = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void insertIndex(int idx) {
        id = idx;
    }

    public void disableSelf() {
        isPivot = true;
        //this.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
