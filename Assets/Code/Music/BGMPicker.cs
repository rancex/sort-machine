using UnityEngine;
using System.Collections;

public class BGMPicker : MonoBehaviour {

	public int BGMnum;

	// Use this for initialization
	void Start () {
		if (BGMnum != 0) {
			//int songNum = PlayerPrefs.GetInt ("char", 0);
			GameObject.Find ("BGMManager").SendMessage ("playBGM", BGMnum);
		}
        else {
            GameObject.Find("BGMManager").SendMessage("stopBGM");
        }
	}
}
