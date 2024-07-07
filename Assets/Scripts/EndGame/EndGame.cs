using ComeHomeGame;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class EndGame : MonoBehaviour
{
    public GameObject player;
    public GameObject tableWin;
    public double yourScore { get; private set; }

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

        Score scoreManager = player.GetComponent<Score>();
        if (scoreManager == null)
        {
            Debug.LogError("Score component is not found.");
            return;
        }

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

        var playing = new Playing()
        {
            score = scoreManager.GetScore(),
            userId = user.id,
            time = 0
        };
        StartCoroutine(SavePlaying(playing));
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
            Debug.Log("User data successfully saved.");
        }
        else
        {
            Debug.LogError("Error saving user data: " + webRequest.error);
        }
    }

    IEnumerator SavePlaying(Playing data)
    {
        string jsonData = JsonUtility.ToJson(data);
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest webRequest = new UnityWebRequest("https://api-cominghome.outfit4rent.online/playings", "POST")
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            Score scoreManager = player.GetComponent<Score>();
            SaveDataOnButtonPress();
            tableWin.GetComponent<TableWinGame>().Display(DataManager.Instance.user.email, 5, scoreManager.GetScore());
        }
    }
}

public class Playing
{
    public int id { get; set; }
    public double score { get; set; }
    public double time { get; set; }
    public int userId { get; set; }
}
