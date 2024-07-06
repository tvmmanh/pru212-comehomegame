using ComeHomeGame;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SaveDataa : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {

    }

    public void SaveDataOnButtonPress()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned.");
            return;
        }

        DataManager dataManager = DataManager.Instance;
        if (dataManager == null)
        {
            Debug.LogError("DataManager instance is not found.");
            return;
        }

        User user = dataManager.user;
        if (user == null)
        {
            Debug.LogError("User data is not found.");
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component is not found.");
            return;
        }

        Health healthController = player.GetComponent<Health>();
        if (healthController == null)
        {
            Debug.LogError("Health component is not found.");
            return;
        }

        Vector3 currentPlayerPosition = playerController.GetPosition();
        user.yPosition = currentPlayerPosition.y;
        user.xPosition = currentPlayerPosition.x;
        user.currentHealth = healthController.currentHealth;
        user.currentJump = playerController.GetJump();
        user.currentScale = playerController.GetScaleCharacter().x;
        user.indexScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        user.currentSpeed = playerController.GetSpeed();
        user.currentDame = 1;

        StartCoroutine(SaveUserData(user));
    }

    IEnumerator SaveUserData(User data)
    {
        string jsonData = JsonUtility.ToJson(data);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest webRequest = new UnityWebRequest("https://api-cominghome.outfit4rent.online/users", "PUT")
        {
            uploadHandler = new UploadHandlerRaw(postData),
            downloadHandler = new DownloadHandlerBuffer()
        };
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data successfully saved.");
        }
        else
        {
            Debug.LogError("Error saving data: " + webRequest.error);
        }
    }
}
