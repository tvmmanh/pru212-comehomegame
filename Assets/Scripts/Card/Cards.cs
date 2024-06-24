using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public Button[] choiceButtons;
    public GameObject player;

    void Start()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[index].onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    void Update()
    {
        
    }
    public void DisplayCard()
    {
       
        gameObject.SetActive(true);
    }
    void OnChoiceSelected(int choiceIndex)
    {
        switch (choiceIndex)
        {
            case 0: 
                SetCharacterSize(0.5f); 
                break;
            case 1: 
                SetCharacterSize(1.0f); 
                break;
            case 2: 
                SetCharacterSize(1.5f); 
                break;
            default:
                Debug.LogError("Invalid choice index!");
                break;
        }

        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    void SetCharacterSize(float scale)
    {
        player.transform.localScale = new Vector3(scale, scale, scale);
    }
}
