using UnityEngine;
using System.Collections;

public class DocumentSwitchTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D col) {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().chosenQuickObject == this.gameObject) {
            if (col.tag == "SwitchTrigger") {
                SwitchTriggerScript swg = col.GetComponent<SwitchTriggerScript>();
                /*
                if (swg.isPivot == true) {

                    if (swg.id == this.GetComponent<CarMainScript>().triggerIdx) {
                    }
                    else {

                        Vector2 mousePosition = Input.mousePosition;
                        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                        if (this.gameObject.GetComponent<CarMainScript>().triggerIdx < swg.id) {
                            if (mousePosition.x > swg.transform.position.x) this.GetComponent<CarMainScript>().canMove = false;
                            if (mousePosition.x < swg.transform.position.x) {
                                this.GetComponent<CarMainScript>().canMove = true;
                            }
                        }
                        else {
                            if (mousePosition.x < swg.transform.position.x) this.GetComponent<CarMainScript>().canMove = false;
                            if (mousePosition.x < swg.transform.position.x) {
                                this.GetComponent<CarMainScript>().canMove = true;
                            }
                        }
                    }
                }
                */
            }
        }
    }
}
