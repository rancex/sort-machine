using UnityEngine;
using System.Collections;

public class BoxClickMerge : MonoBehaviour {

    public SpriteRenderer outlineRenderer;

    public bool clickable = false;

    public Coroutine cor;

    public Vector3 bigScale = new Vector3(0.9f, 0.9f, 0.9f);

    public Vector3 originalScale = new Vector3(0.7f, 0.7f, 0.7f);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void modifyOutline(int color) {

        if (color == 0) {
            this.transform.localScale = originalScale;
            outlineRenderer.enabled = false;
        }
        else if (color == 1) {
            this.transform.localScale = bigScale;
            outlineRenderer.enabled = true;
            outlineRenderer.color = new Color(0.0f, 255.0f, 0.0f);
        }
        else if (color == 2) {
            this.transform.localScale = originalScale;
            outlineRenderer.enabled = true;
            outlineRenderer.color = new Color(255.0f, 255.0f, 0.0f);
        }
        else if (color == 3) {
            this.transform.localScale = originalScale;
            outlineRenderer.enabled = true;
            outlineRenderer.color = new Color(255.0f, 0.0f, 0.0f);
        }
    }

    public void onTouched() {
        if (clickable == true) {
            if ( GameObject.Find("GameManager").GetComponent<MergeIdealSolver>().isSolving == false) 
            addSelfToNextList();
        }
    }



    public void addSelfToNextList() {
       
        GameObject.Find("GameManager").GetComponent<MergeSortGameManager>().addToNextList(this.gameObject);
    }

    public void moveToPosition(float xPos,float Ypos) {
        cor = StartCoroutine(smoothMoveToPosition(1.0f, xPos,Ypos));
    }

    IEnumerator smoothMoveToPosition(float time, float targetPosX, float targetPosY) {
        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosX, (elapsedTime / time)),
                                                  Mathf.Lerp(transform.position.y, targetPosY, (elapsedTime / time)),
                                                  0.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        GameObject.Find("GameManager").GetComponent<MergeSortGameManager>().finishedMovingObject(this.gameObject);
    }

    public Vector3 objOriginalPos;

    public void saveOriginalPosition() {

        if (this.GetComponent<CarMainScript>().carIdx == 3) Debug.Log(this.transform.position);
        objOriginalPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    public void returnToOriginalPos() {
        StopCoroutine(cor);
        this.transform.position = objOriginalPos;
    }
}
