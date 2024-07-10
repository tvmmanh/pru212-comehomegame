using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private double score;
    public TMP_Text TextMeshPro;
    void Start()
    {
        DataManager dataManager = DataManager.Instance;
        if (dataManager!=null)
        {
            User user = dataManager.user;
            Debug.Log(user.currentScore);
            if (dataManager.Type=="Continue")
            {
                score = user.currentScore;
            }
            else if(dataManager.Type=="Normal")
            {
                score = dataManager.GetScore();
                
            }
            TextMeshPro.text = this.score.ToString();

        }
    }
    void Update()
    {
        
    }
    public void SetScore(double score)
    {
        this.score+= score;
        TextMeshPro.text = this.score.ToString();
    }
    public double GetScore()
    {
        return score;
    }
   
}