using UnityEngine;
using System.Collections;

public class CarMainScript : MonoBehaviour {

    public TextMesh txtNumber;
    public GameObject carObject;

    public int carNumber;
    public int carIdx;

    public int triggerIdx;

    public Coroutine cor = null;

    public bool isPivot = false;

    public bool canMove = true;

    // Use this for initialization
    void Start () {
        canMove = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void insertNumber(int number) {
        carNumber = number;
        txtNumber.text = carNumber.ToString();
    }

    public void changeNumber(int number) {
        carNumber = number;
        txtNumber.text = carNumber.ToString();
    }
    public void insertIndex(int idx) {
        carIdx = idx;
        triggerIdx = idx;
    }
    public void onObjectClicked() {
        Debug.Log(carNumber);
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

        Transform targetPos = GameObject.Find("GameManager").GetComponent<GameManager>().triggerList[triggerIdx].transform;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.position.x, (elapsedTime / time)),
                                                  transform.position.y,
                                                  0.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
