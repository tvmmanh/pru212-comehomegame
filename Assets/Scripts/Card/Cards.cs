using ComeHomeGame;
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

    private PlayerController playerController;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[index].onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    public void DisplayCard()
    {
        gameObject.SetActive(true);
    }

    void OnChoiceSelected(int choiceIndex)
    {
        float scale = 0;
        switch (choiceIndex)
        {
            case 0:
                scale = 0.5f;
                break;
            case 1:
                scale = 1.0f;
                break;
            case 2: 
                scale = 1.5f;
                break;
            default:
                Debug.LogError("Invalid choice index!");
                break;
        }

        playerController.SetCharacterSize(scale);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
