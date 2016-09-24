using UnityEngine;
using System.Collections;

public class BoxClick : MonoBehaviour {

    //Boxes are click-and-drag able on quicksort

    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

#if UNITY_ANDROID
        /*
        if (Input.touchCount >= 1) {
            foreach (Touch touch in Input.touches) {
                Ray ray = Camera.main.ScreenPointToRay(Input.touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100)) {
                    //Might need to set X and Z depending on how your game is set up as touch.position is a 2D Vector
                    this.transform.position = touch.position;
                }
            }
        }
        */
#endif

    }

    void OnMouseDown() {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().waitingForPivot) {
            GameObject.Find("GameManager").GetComponent<GameManager>().markObjectQuick(this.gameObject);

            GameObject.Find("GameManager").GetComponent<GameManager>().waitingForPivot = false;


        }
        else {

            if (this.GetComponent<CarMainScript>().isPivot == true) {
                GameObject.Find("InterfaceManager").GetComponent<InterfaceManager>().changeInfoText("Pivot Point cannot be moved!");
            }
            else {
                Debug.Log("Clicked");
                Debug.Log(this.GetComponent<CarMainScript>().triggerIdx);
                GameObject.Find("GameManager").GetComponent<GameManager>().chosenQuickObject = this.gameObject;
            }
        }
    }

    
    void OnTriggerStay2D(Collider2D col) {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().chosenQuickObject == this.gameObject) {
            if (col.tag == "SwitchTrigger") {
                SwitchTriggerScript swg = col.GetComponent<SwitchTriggerScript>();
                if(swg.isPivot == true) {
                   
                    if (swg.id == this.GetComponent<CarMainScript>().triggerIdx) {
                    }
                    else {

                        Vector2 mousePosition = Input.mousePosition;
                        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                        if (this.gameObject.GetComponent<CarMainScript>().triggerIdx < swg.id) {
                            if (mousePosition.x > swg.transform.position.x) this.GetComponent<CarMainScript>().canMove = false;
                            else if(mousePosition.x < swg.transform.position.x){
                                this.GetComponent<CarMainScript>().canMove = true;
                            }
                        }
                        else {
                            if (mousePosition.x < swg.transform.position.x) this.GetComponent<CarMainScript>().canMove = false;
                            else if(mousePosition.x > swg.transform.position.x) {
                                this.GetComponent<CarMainScript>().canMove = true;
                            }
                        }
                    }
                }
            }
        }
    }
    

    void OnTriggerEnter2D(Collider2D col) {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            if (GameObject.Find("GameManager").GetComponent<GameManager>().chosenQuickObject == this.gameObject) {
                if (col.tag == "SwitchTrigger") {
                    SwitchTriggerScript swg = col.GetComponent<SwitchTriggerScript>();

                    if (swg.isPivot == false) {
                        if (swg.id == this.GetComponent<CarMainScript>().triggerIdx) {
                        }
                        else {
                            int targetId = GameObject.Find("GameManager").GetComponent<GameManager>().switchObjectQuick(this.gameObject.GetComponent<CarMainScript>().triggerIdx, swg.id);

                            GameObject.Find("GameManager").GetComponent<GameManager>().carList[targetId].GetComponent<CarMainScript>().moveToTriggerPosition(1.0f);
                            //GameObject.Find("GameManager").GetComponent<GameManager>().carList[this.gameObject.GetComponent<CarMainScript>().triggerIdx].GetComponent<CarMainScript>().moveToTriggerPosition(1.0f);
                            //col.gameObject.GetComponent<CarMainScript>().moveToTriggerPosition(1.0f);
                        }
                    }
                    /*
                    else {
                        Debug.Log("www");
                        if (swg.id == this.GetComponent<CarMainScript>().triggerIdx) {
                        }
                        else {

                            Vector2 mousePosition = Input.mousePosition;
                            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                            Debug.Log(mousePosition.x);
                            Debug.Log(swg.transform.position.x);

                            if (this.gameObject.GetComponent<CarMainScript>().triggerIdx < swg.id) {

                                if (mousePosition.x > swg.transform.position.x)  this.transform.position = swg.transform.position;
                            }
                            else {
                                if (mousePosition.x < swg.transform.position.x) this.transform.position = swg.transform.position;
                            }
                        }
                    }
                    */
                }
            }
        }
        else if(GameObject.Find("GameManager").GetComponent<GameManager>().sortType == KeyDictionary.SORTTYPE.HEAPSORT) {

        }
    }
}
