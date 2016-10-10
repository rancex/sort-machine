using UnityEngine;
using System.Collections;

public class CarMainScript : MonoBehaviour {

    public TextMesh txtNumber;

    public GameObject carObject;

    public GameObject sortedLightObject;

    public int carNumber;
    public int carIdx;

    public int triggerIdx;

    public Coroutine cor = null;

    public bool isPivot = false;

    public bool canMove = true;

    public int sorttype = 0;

    private string SortingLayerName = "Default";
    private int SortingOrder = 2;

    // Use this for initialization
    void Start () {
        canMove = true;

        txtNumber.GetComponent<MeshRenderer>().sortingLayerName = SortingLayerName;
        txtNumber.GetComponent<MeshRenderer>().sortingOrder = SortingOrder;

        setSortType();
    }

    public void setSortType() {
        sorttype = GameObject.Find("GameManager").GetComponent<GameManager>().sortType;
    }

    void Awake() {
        
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

    public void moveToPosition(float xPos) {
        StartCoroutine(smoothMoveToPosition(2.0f,xPos));
    }

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

        StopCoroutine(cor);
    }

    IEnumerator smoothMoveToTriggerWithNum(float time, int trigIdx) {
        Transform targetPos = GameObject.Find("GameManager").GetComponent<GameManager>().triggerList[trigIdx].transform;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.position.x, (elapsedTime / time)),
                                                  transform.position.y,
                                                  -1.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameObject.Find("IdealSolver").GetComponent<IdealSolutionQuicksort>().runNextSolveProblem();
    }

    public void autoMoveToTriggerNum(int triggerNum) {
        StartCoroutine(smoothMoveToTriggerWithNum(1.0f,triggerNum));
    }

    public void markSortedLight() {
        sortedLightObject.SetActive(true);
    }
}
