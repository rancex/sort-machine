using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(blingEverySecond());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator blingEverySecond() {
        while (true) {
            yield return new WaitForSeconds(1.0f);

            if(this.GetComponent<Text>().text != "") this.GetComponent<Text>().text = "";
            else {
                this.GetComponent<Text>().text = "Touch to Continue...";
            }
        }
    }
}
