using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {

    public CameraControls targetScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseOver() {
        targetScript.isInsideBox = true;
    }

    void OnMouseExit() {
        targetScript.isInsideBox = false;
    }


}
