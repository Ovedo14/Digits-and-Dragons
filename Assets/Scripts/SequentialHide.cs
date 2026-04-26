using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SequentialHide : MonoBehaviour
{
    [SerializeField] public GameObject[] elements;

    private int currentIndex = 0;

    public void GoNext()
    {
        if (currentIndex < elements.Length - 1)
        {
            Debug.Log("pressed");
            elements[currentIndex].SetActive(false);
            currentIndex++;
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}