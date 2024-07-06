using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreEnemy : MonoBehaviour
{
    public double price;
    public GameObject player;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void AddScore()
    {
        player.GetComponent<Score>().SetScore(price);
    }
}
