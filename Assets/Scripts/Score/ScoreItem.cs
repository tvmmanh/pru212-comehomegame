using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public double price;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var scoreManger = collision.GetComponent<Score>();
            scoreManger.SetScore(price);
            gameObject.SetActive(false);
        }
    }
}
