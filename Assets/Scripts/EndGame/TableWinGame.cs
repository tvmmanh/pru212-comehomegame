using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class TableWinGame : MonoBehaviour
{
    public TMP_Text[] Rank;
    public TMP_Text[] Email;
    public TMP_Text[] Score;
    public TMP_Text YourScore;
    public TMP_Text YourRank;

    public void Display(string email, int rank, double score)
    {
        gameObject.SetActive(true);
        StartCoroutine(CallAPI(email, score, rank));
        
    }

    IEnumerator CallAPI(string email, double score, int rank)
    {
        string url = $"https://api-cominghome.outfit4rent.online/users/rank-tops/{email}/{score}/{rank}";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            RankModel response = JsonUtility.FromJson<RankModel>(jsonResponse);

            UpdateUI(response);
        }
    }

    void UpdateUI(RankModel response)
    {
        if (response.rankTops != null)
        {
            for (int i = 0; i < response.rankTops.Count && i < Rank.Length; i++)
            {
                Rank[i].text = response.rankTops[i].rank.ToString();
                Email[i].text = response.rankTops[i].email;
                Score[i].text = response.rankTops[i].score.ToString();
            }
        }

        YourScore.text = response.score.ToString();
        YourRank.text = response.rank.ToString();
        
    }
    public void BackToMenu()
    {
        DataManager dataManager = DataManager.Instance;
        dataManager.Type = "";
        SceneManager.LoadScene(1);
    }
}

[Serializable]
public class RankModel
{
    public int rank;
    public string email;
    public double score;
    public List<RankModel> rankTops;
}
