using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Shuffler{

    /*
        List Shuffler. Uses Durstenfeld Shuffle. 
    */

	// Use this for initialization
        /*
        List<int> numberList = new List<int>();
        for (int i = 0; i < 5; i++){
            numberList.Add(Random.Range(1, 20));
        }
        foreach(int a in numberList) {
            Debug.Log(a);
        }
        shuffleList(numberList);
        foreach (int a in numberList) {
            Debug.Log(a);
        }
        */

    public static List<int> generateRandomList(int amount) {
        List<int> numberList = new List<int>();
        List<int> randomList = new List<int>();

        for (int i = 0; i < 10000; i++) {
            numberList.Add(i);
        }

        shuffleList(numberList);

        for (int j = 0; j < amount; j++) {
            randomList.Add(numberList[j]);
        }

        return randomList;
    }
    
    /// <summary>
    /// Shuffle A List Containing Numbers With Durstenfeld Shuffle
    /// </summary>
    public static List<int> shuffleList(List<int> numberList) {
        //1 Determine the length of the array (len).
        int len = numberList.Count;
        //2 Loop through each of the values between len and one, decrementing the loop control variable (lcv) for each iteration.
        for(int i = len - 1;i > 0; i--) {
            //3 Randomly select a value(n) between one and the current lcv.
            int randomIdx = Random.Range(0, i + 1);

            //4 Swap the contents of the array elements at indexes n and lcv.
            //This moves the randomly selected element beyond the indexes that will be considered in the next iteration.

            int tempValue = numberList[randomIdx];
            numberList[randomIdx] = numberList[i];
            numberList[i] = tempValue;

            //5 Continue the loop, restarting the randomisation process from step 3 with a lower lcv.
        }

        return numberList;
    }
}
