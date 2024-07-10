using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    public double price;
    public AudioClip audioClip;
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
            SoundManage.instance.PlaySound(audioClip);
            var scoreManger = collision.GetComponent<Score>();
            scoreManger.SetScore(price);
            gameObject.SetActive(false);
        }
    }
}
