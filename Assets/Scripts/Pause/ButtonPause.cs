using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPause : MonoBehaviour
{
    public Button PauseButton;
    private bool isPaused = false; // To track the pause state

    private void Start()
    {
        if (PauseButton != null)
        {
            PauseButton.onClick.AddListener(TogglePause);
        }
        else
        {
            Debug.LogError("PauseButton is not assigned in the Inspector.");
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        DisplayPauseTable();
    }

    private void DisplayPauseTable()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
