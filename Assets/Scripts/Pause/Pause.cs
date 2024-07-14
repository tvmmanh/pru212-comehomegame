using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseFunction : MonoBehaviour
{
    public TMP_Text Notify;
    public GameObject PausePanel;
    private bool isPaused = false; 

    private void Start()
    {

    }
    private void Update()
    {
        NotifyManager notifyManager = NotifyManager.Instance;
        if (string.IsNullOrEmpty(notifyManager.GetString()))
        {
            Notify.text=notifyManager.GetString();
        }
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        NotifyManager notifyManager = NotifyManager.Instance;
        notifyManager.SetString("");
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        NotifyManager notifyManager= NotifyManager.Instance;
        notifyManager.SetString("");
        DataManager dataManager = DataManager.Instance;
        dataManager.Type = "";
        SceneManager.LoadScene(1);
    }
}
