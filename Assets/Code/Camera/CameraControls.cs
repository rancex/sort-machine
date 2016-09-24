using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

    public Camera targetCamera;

    public BoxCollider2D targetCollider;

    private float zoomSpeed = 0.1f;

    public bool isInsideBox = false;

    public bool isZoomingIn = false;
    public bool isZoomingOut = false;

	// Use this for initialization
	void Start () {
        targetCamera = this.GetComponent<Camera>();
	}

    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    void Update() {
        if (isInsideBox == true) {
            if (Input.GetMouseButtonDown(0)) {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = targetCamera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed * -1f, 0, pos.y * dragSpeed);

            transform.Translate(move, Space.World);

            if (targetCamera.transform.position.x < -5.8) targetCamera.transform.position = new Vector3(-5.8f, targetCamera.transform.position.y, targetCamera.transform.position.z);
            if (targetCamera.transform.position.x > 5.8) targetCamera.transform.position = new Vector3(5.8f, targetCamera.transform.position.y, targetCamera.transform.position.z);
        }

        if (isZoomingIn == true) {
            zoomCamera(-1);
        }
        if (isZoomingOut == true) {
            zoomCamera(1);
        }
    }

    public void moveCamera() {

    }

    public void zoomCamera(int zoomValue) {
        if (zoomValue > 0) {
            if (targetCamera.orthographicSize < 5) {
            }
            else zoomValue = 0;
        }
        else if (zoomValue < 0) {
            if (targetCamera.orthographicSize > 3) {

            }
            else zoomValue = 0;
        }

        targetCamera.orthographicSize += (zoomSpeed * zoomValue);
        targetCollider.transform.localScale = new Vector3(targetCollider.transform.localScale.x + (0.35f * zoomValue),
                                                          targetCollider.transform.localScale.y + (0.2f * zoomValue),
                                                          targetCollider.transform.localScale.z);
    }

    public void zoomIn() {
        isZoomingIn = true;
    }

    public void zoomOut() {
        isZoomingOut = true;
    }

    public void notZoom() {
        isZoomingIn = false;
        isZoomingOut = false;
    }
}
