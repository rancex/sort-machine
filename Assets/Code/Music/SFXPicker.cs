using UnityEngine;
using System.Collections;

public class SFXPicker : MonoBehaviour {

	private int SFXnum;
	
	// Use this for initialization
	void Start () {
		//GameObject.Find ("SFXManager").SendMessage ("playSFX",SFXnum);
	}

	/*
	public void sendToSFXPlayer(int SFXnum){
		GameObject.Find ("SFXManager").SendMessage ("playSFX",SFXnum);
	}
	*/

	public void playMouseHoverSFX(){
		SFXnum = 0;
		GameObject.Find ("SFXManager").SendMessage ("playSFX", SFXnum);
	}

	public void playMouseClickSFX(){
		SFXnum = 1;
		GameObject.Find ("SFXManager").SendMessage ("playSFX", SFXnum);
	}
}
