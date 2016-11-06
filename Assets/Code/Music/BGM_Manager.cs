using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGM_Manager : MonoBehaviour {
	
	private AudioSource BGMSource;

	public List<AudioClip> BGM = new List<AudioClip>();

	// Use this for initialization
	void Start () {
		BGMSource = this.GetComponent<AudioSource> ();
        changeVolume();
	}

    public void changeVolume() {
        BGMSource.volume = PlayerPrefs.GetInt ("BGM", 1);
    }

	public void playBGM(int BGMnum){
        if (BGMnum != 0) {
            if (BGMSource == null) BGMSource = this.GetComponent<AudioSource>();

            if (BGMSource.clip != null) {
                if (BGMSource.clip != BGM[BGMnum]) {
                    BGMSource.clip = BGM[BGMnum];
                    BGMSource.Play();
                }
            }
            else {
                BGMSource.clip = BGM[BGMnum];
                BGMSource.Play();
            }
        }
	}

    public void stopBGM() {
        if (BGMSource == null) BGMSource = this.GetComponent<AudioSource>();

        BGMSource.Stop();
    }
}
