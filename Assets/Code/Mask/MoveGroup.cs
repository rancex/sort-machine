using UnityEngine;
using System.Collections;

public class MoveGroup : MonoBehaviour {

    public Coroutine cor;

    public bool isMovingLeft = false;
    public bool isMovingRight = false;

    public GameObject minObjectX;
    public GameObject maxObjectX;

    public ProgrammableMove programMove;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(isMovingLeft == true) {
            if (minObjectX.transform.position.x < -6.5f) {
                Debug.Log(minObjectX.transform.position.x);
                this.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f));
            }
            else {
                Debug.Log(this.transform.position.x);
            }
        }
        if (isMovingRight == true) {
            if (maxObjectX.transform.position.x > 1.5f) {
                Debug.Log(maxObjectX.transform.position.x);

                this.transform.Translate(new Vector3(-0.1f, 0.0f, 0.0f));
            }
        }
    }

    public void returnToOriginalPos() {
        this.transform.position = new Vector3(-0.78f, this.transform.position.y, this.transform.position.z);
    }

    public void setMinObject(GameObject g) {
        minObjectX = g;
    }

    public void setMaxObject(GameObject g) {
        maxObjectX = g;
    }

    public void moveLeft() {
        if (programMove.isDoingProgram == false) {
            isMovingLeft = true;
        }
    }

    public void moveRight() {
        if (programMove.isDoingProgram == false) {
            isMovingRight = true;
        }
    }

    public void stopMoving() {
        isMovingLeft = false;
        isMovingRight = false;
    }

    IEnumerator moveToDirection(float dir) {
        while (true) {
            //yield return new WaitForSeconds()
            this.transform.Translate(new Vector3(dir, 0.0f, 0.0f));
        }
    }
    /*
    IEnumerator smoothMoveToPosition(float time, float targetPos) {
        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos, (elapsedTime / time)),
                                                  transform.position.y,
                                                  0.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    */
}
