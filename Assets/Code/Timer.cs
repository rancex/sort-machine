using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    private int timeLeft;
    public Text timerText;

	// Use this for initialization
	void Start () {
        timeLeft = 180;
        StartCoroutine(updateTimerEverySecond());
	}
	
	// Update is called once per frame
	void Update () { 
	}

    IEnumerator updateTimerEverySecond() {

        while(timeLeft > 0) {
            timeLeft--;

            int minutes = timeLeft / 60;
            int seconds = timeLeft - (minutes * 60);

            string clockTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            timerText.text = clockTime;

            yield return new WaitForSeconds(1.0f);

            if(timeLeft == 0) {
                //SceneManager.LoadScene(KeyDictionary.SCENES.ARCADE);
            }


        }
    }
}
