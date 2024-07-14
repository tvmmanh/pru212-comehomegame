using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    public TMP_InputField EmailInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField ConfirmPasswordInputField;
    public TMP_Text ErrorMessage;

    public async void RegisterUser()
    {
        string email = EmailInputField.text;
        string password = PasswordInputField.text;
        string confirmPassword = ConfirmPasswordInputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            ErrorMessage.text = "Information is missing.";
            return;
        }

        if (password != confirmPassword)
        {
            ErrorMessage.text = "Confirm Password is not correct.";
            return;
        }

        try
        {
            string url = $"https://api-cominghome.outfit4rent.online/users/{email}/{password}";
            User user = await FetchAsync(url);

            if (user != null)
            {
                DataManager dataManager = DataManager.Instance;
                dataManager.user = user;
                ErrorMessage.text = "Registration successful!";
                SceneManager.LoadScene(1);
            }
            else
            {
                ErrorMessage.text = "Registration failed.";
            }
        }
        catch (Exception e)
        {
            ErrorMessage.text = "Error Internets ";
        }
    }

    public void Cancel()
    {
        ErrorMessage.text = "";
        SceneManager.LoadScene(0);
    }

    public async Task<User> FetchAsync(string url)
    {
        try
        {
            using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, ""))
            {
                webRequest.SetRequestHeader("accept", "text/plain");

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
}
