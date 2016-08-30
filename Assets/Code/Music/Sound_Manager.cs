using UnityEngine;
using System.Collections;

public class Sound_Manager : MonoBehaviour {

	private static Sound_Manager instance = null;
	private static Sound_Manager Instance{
		get{return instance;}
	}

	private GameObject mainCam;

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} 
		else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);

		//mainCam = GameObject.Find ("Main Camera");
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(mainCam == null)mainCam = GameObject.Find ("Main Camera");
		goToMainCamera ();
	}

	void goToMainCamera(){
		Vector3 camLocation = mainCam.transform.position;
		this.transform.position = camLocation;
	}
}