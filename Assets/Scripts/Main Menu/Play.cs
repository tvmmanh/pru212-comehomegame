using ComeHomeGame;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Play : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Load()
    {
        DataManager dataManager = DataManager.Instance;
        dataManager.Type = "Continue";

        SceneManager.LoadSceneAsync(dataManager.user.indexScene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
