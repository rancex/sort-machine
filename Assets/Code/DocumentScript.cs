using UnityEngine;
using System.Collections;

public class DocumentScript : MonoBehaviour {

    public GameObject parent = null;

    public GameObject leftChild = null;
    public GameObject rightChild = null;

    public Vector3 originalScale;
    public Vector3 targetScale;

    public int id;
    public int docNumber;

    public int childDirection = 0;

    public TextMesh txtNumber;

    public Coroutine cor;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void insertId(int newId) {
        id = newId;
    }

    public void insertPositionInTree(int direction) {
        childDirection = direction;
    }

    public void insertNumber(int number) {
        docNumber = number;
        txtNumber.text = docNumber.ToString();
    }

    public void changeNumber(int number) {
        docNumber = number;
    }

    public void decidePosition() {
        if (childDirection != 0) {
            if (childDirection == KeyDictionary.TREECHILDDIRECTION.LEFT) {
                
                this.transform.localScale = new Vector3(parent.transform.localScale.x * 0.75f,
                                                        parent.transform.localScale.y * 0.75f,
                                                        parent.transform.localScale.z);
                this.transform.position = new Vector3(parent.transform.position.x - (8f * this.transform.localScale.x) + (2.5f * (1f - this.transform.localScale.x)),
                                                      parent.transform.position.y - (5f * this.transform.localScale.y),
                                                      parent.transform.position.z - 1);
            }
            else if (childDirection == KeyDictionary.TREECHILDDIRECTION.RIGHT) {
                

                this.transform.localScale = new Vector3(parent.transform.localScale.x * 0.75f,
                                                        parent.transform.localScale.y * 0.75f,
                                                        parent.transform.localScale.z);
                this.transform.position = new Vector3(parent.transform.position.x + (8f * this.transform.localScale.x) - (2.5f * (1f - this.transform.localScale.x)),
                                                      parent.transform.position.y - (5f * this.transform.localScale.y),
                                                      parent.transform.position.z - 1);
            }
        }
        else if(childDirection == 0) {
            this.transform.position = new Vector3(0f,
                                                  3f,
                                                  0f);
        }
    }

    public void heapifySelf() {
        if (parent != null) {
            if (this.docNumber < parent.GetComponent<DocumentScript>().docNumber) {
                int tempNumber = parent.GetComponent<DocumentScript>().docNumber;

                parent.GetComponent<DocumentScript>().insertNumber(this.docNumber);
                this.insertNumber(tempNumber);

                parent.GetComponent<DocumentScript>().heapifySelf();
            }
        }
    }

    public void heapifyChild() {
        bool isMoved = false;

        if(leftChild != null) {
            leftChild.GetComponent<DocumentScript>().heapifySelf();
            isMoved = true;
        }
        
        if(isMoved == false) {
            if (rightChild != null) {
                rightChild.GetComponent<DocumentScript>().heapifySelf();
                isMoved = true;
            }
        }
        
    }

    void OnMouseDown() {
        Debug.Log("Clicked");
        Debug.Log(id);
        GameObject.Find("GameManager").GetComponent<GameManager>().chosenHeapObject = this.gameObject;
    }

    public void moveToTriggerPosition(float time) {
        if (cor == null) {
            cor = StartCoroutine(smoothMoveToTrigger(time));
        }
        else {
            StopCoroutine(cor);
            cor = StartCoroutine(smoothMoveToTrigger(time));
        }
    }

    public void stopCoroutineImmediately() {
        if (cor != null) {
            StopCoroutine(cor);
            cor = null;
        }
    }

    IEnumerator smoothMoveToTrigger(float time) {

        Transform targetPos = GameObject.Find("GameManager").GetComponent<GameManager>().triggerList[id].transform;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.position.x, (elapsedTime / time)),
                                                  transform.position.y,
                                                  0.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    /*
    void OnTriggerStay2D(Collider2D col) {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().chosenQuickObject == this.gameObject) {
            if (col.tag == "SwitchTrigger") {
                SwitchTriggerScript swg = col.GetComponent<SwitchTriggerScript>();
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
            }
        }
    }
*/
}
