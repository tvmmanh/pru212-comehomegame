
using Cainos.LucidEditor;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public Button[] choiceButtons;
    private string correctAnswer;
    public GameObject cardPanel;
    private void Start()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int index = i;
            choiceButtons[index].onClick.AddListener(() => OnChoiceSelected(index));
        }
    }

    public void DisplayQuestion(string question, List<string> choices, string correctAnswer)
    {
        questionText.text = question;
        this.correctAnswer = correctAnswer;
        for (int i = 0; i < choices.Count; i++)
        {
            choiceButtons[i].GetComponentInChildren<TMP_Text>().text = choices[i]; 
        }
        gameObject.SetActive(true); 
    }

    void OnChoiceSelected(int choiceIndex)
    {
        string selectedAnswer = choiceButtons[choiceIndex].GetComponentInChildren<TMP_Text>().text;

        if (selectedAnswer == correctAnswer)
        {

            cardPanel.GetComponent<Cards>().DisplayCard();
        }
        else
        {

            cardPanel.GetComponent<Cards>().DisplayCard();
        }

        
        gameObject.SetActive(false);
    }
}