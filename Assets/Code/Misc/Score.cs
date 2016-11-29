using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

    public int sortType;
    public string targetListName;

    public bool isSceneGame;

	// Use this for initialization
	void Start () {
        //setSortType();
        if (SceneManager.GetActiveScene().name == "mainmenu") {
            setSortType();
        }

    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setSortType() {
        sortType = PlayerPrefs.GetInt("sorttype");
        if (sortType == KeyDictionary.SORTTYPE.BUBBLESORT) {
            targetListName = "scoreListBubble";
        }
        else if (sortType == KeyDictionary.SORTTYPE.SELECTIONSORT) {
            targetListName = "scoreListSelection";
        }
        else if (sortType == KeyDictionary.SORTTYPE.INSERTIONSORT) {
            if (PlayerPrefs.GetInt("fromShell", 0) == 1) {
                PlayerPrefs.SetInt("fromShell", 0);
                targetListName = "scoreListShell";
            }
            else targetListName = "scoreListInsertion";
        }
        else if (sortType == KeyDictionary.SORTTYPE.SHELLSORT) {
            targetListName = "scoreListShell";
        }
        else if (sortType == KeyDictionary.SORTTYPE.HEAPSORT) {
            targetListName = "scoreListHeap";
        }
        else if (sortType == KeyDictionary.SORTTYPE.MERGESORT) {
            targetListName = "scoreListMerge";
        }
        else if (sortType == KeyDictionary.SORTTYPE.QUICKSORT) {
            targetListName = "scoreListQuick";
        }

        if (SceneManager.GetActiveScene().name == "mainmenu") {
            isSceneGame = false;
        }
        else {
            isSceneGame = true;
        }
    }

    public void showFinishTime(int timeElapsed) {
        
    }

    

    public int saveTimeToLeaderboard(int time) {
        int topScorePos = -1;

        int[] scoreArray = PlayerPrefsX.GetIntArray(targetListName, 99999, 5);

        for (int i = 0;i < scoreArray.Length; i++) {
            if(time < scoreArray[i] && topScorePos == -1) {
                for(int j = scoreArray.Length - 1; j > i; j--) {
                    scoreArray[j] = scoreArray[j - 1];
                }
                scoreArray[i] = time;

                topScorePos = i;
            }
        }

        PlayerPrefsX.SetIntArray(targetListName, scoreArray);

        return topScorePos;
    }

    public void showLeaderboard() {

       

        int[] scoreArray = PlayerPrefsX.GetIntArray(targetListName, 99999, 5);

        for (int i = 0; i < scoreArray.Length; i++) {
            string targetObject = "RankScore" + (i + 1);

            string numbers = "";

            if (scoreArray[i] == 99999) numbers = "---";
            else numbers = scoreArray[i].ToString();

            GameObject.Find(targetObject).GetComponent<Text>().text = numbers;
            /*
            if (topScorePos == i) {
                GameObject.Find(targetObject).GetComponent<Text>().fontStyle = FontStyle.Bold;
            }
            */
        }
    }

}
