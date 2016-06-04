using UnityEngine;
using System.Collections;

public class CarObjectScript : MonoBehaviour {

    Coroutine cor;

    bool isHovered = false;

    Vector3 rotation;
	// Use this for initialization

    // Update is called once per frame
    void Update () {
	    if(isHovered == true) {
            if (transform.eulerAngles.z < 90)
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90 * Time.deltaTime);
            else {
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else {
            if (transform.eulerAngles.z > 0 && transform.eulerAngles.z  < 91)
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90 * 3 * Time.deltaTime);
            else {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
	}

    void OnMouseOver() {
        isHovered = true;
    }

    
    void OnMouseExit() {
        isHovered = false;
    }
    
    void OnMouseDown() {
        this.transform.parent.GetComponent<CarMainScript>().onObjectClicked();
    }
}