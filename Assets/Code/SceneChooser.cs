using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class SceneChooser{

    public static void startBubbleSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.BUBBLESORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.BUBBLESORT);
    }

    public static void startSelectionSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SELECTIONSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.BUBBLESORT);
    }

    public static void startInsertionSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.INSERTIONSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.BUBBLESORT);
    }

    public static void startShellSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.SHELLSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.BUBBLESORT);
    }

    public static void startHeapSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.HEAPSORT);
        Debug.Log("UNDER CONSTRUCTION");
    }

    public static void startMergeSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.MERGESORT);
        Debug.Log("UNDER CONSTRUCTION");
    }

    public static void startQuickSort() {
        PlayerPrefs.SetInt("sorttype", KeyDictionary.SORTTYPE.QUICKSORT);
        SceneManager.LoadScene(KeyDictionary.SCENES.QUICKSORT);
    }
}
