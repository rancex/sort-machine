using UnityEngine;
using System.Collections;

public class BoxClickMerge : MonoBehaviour {

    public SpriteRenderer outlineRenderer;

    public bool clickable = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void modifyOutline(int color) {

        if (color == 0) {
            outlineRenderer.enabled = false;
        }
        else if (color == 1) {
            outlineRenderer.enabled = true;
            outlineRenderer.color = new Color(0.0f, 255.0f, 0.0f);
        }
        else if (color == 2) {
            outlineRenderer.enabled = true;
            outlineRenderer.color = new Color(255.0f, 255.0f, 0.0f);
        }
        else if (color == 3) {
            outlineRenderer.enabled = true;
            outlineRenderer.color = new Color(255.0f, 0.0f, 0.0f);
        }
    }

    void OnMouseDown() {
        if (clickable == true) {
            GameObject.Find("GameManager").GetComponent<MergeSortGameManager>().addToNextList(this.gameObject);
        }
    }

    public void moveToPosition(float xPos,float Ypos) {
        StartCoroutine(smoothMoveToPosition(2.0f, xPos,Ypos));
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
    }
}
