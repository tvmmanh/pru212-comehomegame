using Cainos.PixelArtPlatformer_VillageProps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public TMP_Text notifyText;
    public Button loginButton;
    public TMP_InputField email;
    public TMP_InputField password;

    private void Start()
    {
        loginButton.onClick.AddListener(() => LoginEmail(email.text, password.text));
    }

    public async void LoginEmail(string emailText, string passwordText)
    {
        if (string.IsNullOrEmpty(emailText) || string.IsNullOrEmpty(passwordText))
        {
            notifyText.text = "Email or password cannot be empty.";
            return;
        }

        if (!IsValidEmail(emailText) || passwordText.Length < 4)
        {
            notifyText.text = "Invalid email or password.";
            return;
        }

        string url = $"https://api-cominghome.outfit4rent.online/users/{emailText}/{passwordText}";
        var user = await FetchAsync(url);

        if (user != null && user.email == emailText && user.password == passwordText)
        {

            notifyText.text = "Login successful!";
            DataManager dataManager = DataManager.Instance;
            dataManager.SetUser(user);
            Debug.Log(user.indexScene);
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            notifyText.text = "Login failed. Incorrect email or password.";
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public async Task<User> FetchAsync(string url)
    {
        try
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                var asyncOperation = webRequest.SendWebRequest();
                while (!asyncOperation.isDone)
                {
                    await Task.Yield();
                }

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error fetching data: {webRequest.error}");
                    return null;
                }

                User data = JsonUtility.FromJson<User>(webRequest.downloadHandler.text);
                return data;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception during fetch: {e.Message}");
            return null;
        }
    }
    public void Register()
    {
        SceneManager.LoadScene(6);
    }
}

[System.Serializable]
public class User
{
    public int id;
    public string email;
    public string password;
    public bool isAnyCompleted;
    public double totalScore;
    public int numberCheckpoint;
    public float xPosition;
    public int indexScene;
    public float yPosition;
    public double currentScore;
    public double currentTime;
    public double currentScale;
    public double currentSpeed;
    public double currentJump;
    public double currentDame;
    public double currentHealth;
    public object playings;
}

