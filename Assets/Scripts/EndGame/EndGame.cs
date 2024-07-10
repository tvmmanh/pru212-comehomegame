using ComeHomeGame;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class EndGame : MonoBehaviour
{
    public GameObject player;
    public GameObject tableWin;
    public float yourScore { get; private set; }

    private DataManager dataManager;
    private PlayerController playerController;
    private Health healthController;
    private Score scoreManager;

    private const string apiUrl = "https://api-cominghome.outfit4rent.online";

    private void Awake()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned.");
        }
        else
        {
            playerController = player.GetComponent<PlayerController>();
            healthController = player.GetComponent<Health>();
            scoreManager = player.GetComponent<Score>();

            if (playerController == null) Debug.LogError("PlayerController component is not found.");
            if (healthController == null) Debug.LogError("Health component is not found.");
            if (scoreManager == null) Debug.LogError("Score component is not found.");
        }

        dataManager = DataManager.Instance;
        if (dataManager == null)
        {
            Debug.LogError("DataManager instance is not found.");
        }
    }

    public void SaveDataOnButtonPress()
    {
        if (player == null || playerController == null || healthController == null || scoreManager == null)
        {
            Debug.LogError("Required components are not assigned.");
            return;
        }

        User user = dataManager.user;
        if (user == null)
        {
            Debug.LogError("User data is not found.");
            return;
        }

        var playing = new Playing()
        {
            score = (float)scoreManager.GetScore(),
            userId = user.id,
            time = 0f // Ensure this is set correctly
        };

        Debug.Log("Creating Playing object with Score: " + playing.score + ", User ID: " + playing.userId + ", Time: " + playing.time);

        StartCoroutine(SavePlaying(playing));

        user.yPosition = 0;
        user.xPosition = 0;
        user.currentHealth = 0;
        user.currentJump = 0;
        user.currentScale = 0;
        user.indexScene = 0;
        user.currentSpeed = 0;
        user.currentDame = 1;
        user.totalScore = Mathf.Max((float)user.totalScore, (float)scoreManager.GetScore());

        StartCoroutine(SaveUserData(user));
    }

    IEnumerator SaveUserData(User data)
    {
        string jsonData = JsonUtility.ToJson(data);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest webRequest = new UnityWebRequest($"{apiUrl}/users", "PUT")
        {
            uploadHandler = new UploadHandlerRaw(postData),
            downloadHandler = new DownloadHandlerBuffer()
        };
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User data successfully saved.");
        }
        else
        {
            Debug.LogError("Error saving user data: " + webRequest.error);
        }
    }

    IEnumerator SavePlaying(Playing data)
    {
        // Log the properties of the Playing object
        Debug.Log("Playing object properties:");
        Debug.Log("Score: " + data.score);
        Debug.Log("Time: " + data.time);
        Debug.Log("User ID: " + data.userId);

        string jsonData = JsonUtility.ToJson(data);

        Debug.Log("Generated JSON: " + jsonData);

        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest webRequest = new UnityWebRequest($"{apiUrl}/playings", "POST")
        {
            uploadHandler = new UploadHandlerRaw(postData),
            downloadHandler = new DownloadHandlerBuffer()
        };
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Playing data successfully saved.");
        }
        else
        {
            Debug.LogError("Error saving playing data: " + webRequest.error);
            Debug.LogError("Response Code: " + webRequest.responseCode);
            Debug.LogError("Response: " + webRequest.downloadHandler.text);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with Player detected.");
            Time.timeScale = 0;
            SaveDataOnButtonPress();
            tableWin.GetComponent<TableWinGame>().Display(dataManager.user.email, 5, scoreManager.GetScore());
        }
    }
}
[System.Serializable]
public class Playing
{
    public float score;
    public float time;
    public int userId;
}
