using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private Vector3 dragOrigin;

    public bool animationFinish = true;

    public bool isChoosingSort = false;

    public GameObject targetObject;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (isChoosingSort == false) {
            if (Input.GetMouseButtonDown(0)) {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            if (Input.mousePosition.x - dragOrigin.x < 0) {
                isChoosingSort = true;
                StartCoroutine(smoothMoveToPosition(1.0f, -17.8f));
            }
        }
    }

    public void startBubbleSort() {
        if (animationFinish == true) {
            SceneChooser.startBubbleSort();
        }
    }

    public void startSelectionSort() {
        if (animationFinish == true) {
            SceneChooser.startSelectionSort();
        }
    }

    public void startInsertionSort() {
        if (animationFinish == true) {
            SceneChooser.startInsertionSort();
        }
    }

    public void startShellSort() {
        if (animationFinish == true) {
            SceneChooser.startShellSort();
        }
    }

    public void startHeapSort() {
        if (animationFinish == true) {
            SceneChooser.startHeapSort();
        }
    }

    public void startMergeSort() {
        if (animationFinish == true) {
            SceneChooser.startMergeSort();
        }
    }

    public void startQuickSort() {
        if (animationFinish == true) {
            SceneChooser.startQuickSort();
        }
    }

    IEnumerator smoothMoveToPosition(float time, float targetPos) {
        float elapsedTime = 0;

        animationFinish = false;

        while (elapsedTime < time) {
            targetObject.transform.position = new Vector3(Mathf.Lerp(targetObject.transform.position.x, targetPos, (elapsedTime / time)),
                                                  targetObject.transform.position.y,
                                                  0.0f);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        animationFinish = true;
    }
}
