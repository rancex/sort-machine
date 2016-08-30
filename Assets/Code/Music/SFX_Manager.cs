using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFX_Manager : MonoBehaviour {

	public AudioSource sfxClickSource;
    public AudioSource craneSource;

    public AudioClip clickSound;
    public AudioClip gameOverSound;

    // Use this for initialization
    void Start () {
        changeVolume();
	}

    public void changeVolume() {
        sfxClickSource.volume = PlayerPrefs.GetInt("SFX", 1);
        craneSource.volume = PlayerPrefs.GetInt("SFX", 1);
    }

    /*
	public void playSFX(int SFXnum){
		SFXSource.clip = SFX[SFXnum];
		SFXSource.Play ();
	}
    */

    public void playButtonClick() {
        sfxClickSource.clip = clickSound;
        sfxClickSource.Play();
    }

}
