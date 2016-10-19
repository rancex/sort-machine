using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public int timeLimit = 180;
    private int timeLeft;

    public int timeCounter = 0;

    public Text timerText;
    public Coroutine timeCor;

	// Use this for initialization
	void Start () {
        timeLeft = timeLimit;

        timeCounter = 0;

        timeCor = StartCoroutine(updateTimerEverySecond());
	}

    IEnumerator updateTimerEverySecond() {

        //while(timeLeft > 0) {
        //timeLeft--;

        while (true) {

            timeCounter++;

            int minutes = timeCounter / 60;
            int seconds = timeCounter - (minutes * 60);

            string clockTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            timerText.text = clockTime;

            yield return new WaitForSeconds(1.0f);
        }      
        /*
            if(timeLeft <= 0) {
                GameObject.Find("GameManager").GetComponent<GameManager>().gameOverTimeout();
                break;
            }
            */
            //}
            //stopTimer();
    }

    public void stopTimer() {
        StopCoroutine(timeCor);
    }

    public void startTimer() {
        timeCor = StartCoroutine(updateTimerEverySecond());
    }

    public int getTime() {
        return (timeCounter);
    }
}
